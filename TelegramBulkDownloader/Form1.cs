using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TL;
using TL.Layer23;
using WTelegram;


namespace TelegramBulkDownloader
{
    public partial class Form1 : Form
    {

        string credsFile = "credentials.txt";
        string downloadFolder = "downloads";
        static Client client;

        public Form1()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory(downloadFolder);
            TryLoadCredentials();
        }

        string Config(string what)
        {
            switch (what)
            {
                case "api_id": return apiIdTextBox.Text; // Replace with your API ID
                case "api_hash": return apiHashTextBox.Text; // Replace with your API Hash
                case "phone_number": return phoneNumberTextBox.Text; // Replace with your phone number
                case "verification_code": return Prompt.ShowDialog("Verification code:", "Code");
                case "session_pathname": return "TelegramSession.ss";

                default: return null; // Default behavior for unhandled cases
            }
        }

        private void SaveCredentials()
        {
            var id = Config("api_id");
            var hash = Config("api_hash");
            var number = Config("phone_number");

            var resultText = id + "\n" + hash + "\n" + number;

            File.WriteAllText(credsFile, resultText);
        }

        private void TryLoadCredentials()
        {
            if (File.Exists(credsFile))
            {
                try
                {
                    var creds = File.ReadAllText(credsFile).Split('\n');

                    apiIdTextBox.Text = creds[0];
                    apiHashTextBox.Text = creds[1];
                    phoneNumberTextBox.Text = creds[2];
                }
                catch { }
            }
        }

        private async void requestCodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                labelStatus.Text = "Requesting auth code...";

                client = new WTelegram.Client(Config);

                var myself = await client.LoginUserIfNeeded();

                labelStatus.Text = "Authenticated as: " + myself;

                SaveCredentials();

                Console.WriteLine($"We are logged in as {myself} (id {myself.id})");

                var chats = await client.Messages_GetAllChats();
                Console.WriteLine("This user has joined the following active chats:");

                foreach (var chat in chats.chats)
                {
                    listBoxChannels.Items.Add(new ListItem { Text = chat.Value.Title + " -- " + chat.Value.ID, Value = chat.Value });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            try
            {

                if (listBoxChannels.SelectedItem is ListItem selectedChat)
                {
                    InputPeer chat = null; // Initialize a variable to hold the InputPeer

                    // Determine the type of selectedChat.Value and create the corresponding InputPeer
                    switch (selectedChat.Value)
                    {
                        case Chat chatt:
                            chat = selectedChat.Value as Chat;
                            break;
                        case Channel channel:
                            chat = selectedChat.Value as Channel;
                            break;
                        default:
                            MessageBox.Show("Selected item is not a valid chat or channel.");
                            return; // Exit the method if the selected item is neither
                    }



                    for (int offset_id = 0; ;)
                    {
                        var messages = await client.Messages_GetHistory(chat, offset_id);
                        if (messages.Messages.Length == 0) break;
                        foreach (var msgBase in messages.Messages)
                        {
                            var from = messages.UserOrChat(msgBase.From ?? msgBase.Peer); // from can be User/Chat/Channel

                            if (msgBase is TL.Message msg)
                            {


                                if (msg.media is MessageMediaDocument)
                                {
                                    var document = (msg.media as MessageMediaDocument).document as Document; // Cast to MessageMediaDocument
                                    if (document != null)
                                    {
                                        var filename = document.Filename; // Use document's original filename, or build a name from document ID & MIME type:


                                        if (string.IsNullOrEmpty(filename))
                                        {
                                            var mimeTypeParts = document.mime_type.Split('/');
                                            filename = $"{document.id}.{mimeTypeParts[1]}"; // Build filename using ID and MIME type
                                        }

                                        labelStatus.Text = "Downloading " + filename;



                                        while (File.Exists(downloadFolder + "/" + filename))
                                        {
                                            filename = "Copy_" + filename;
                                        }


                                        using (var fileStream = File.Create(downloadFolder + "/" + filename))
                                        {
                                            await client.DownloadFileAsync(document, fileStream);
                                        }
                                        Console.WriteLine("Download finished");
                                    }
                                }

                                else
                                if (msg.media is MessageMediaPhoto)
                                {
                                    var photo = ((MessageMediaPhoto)msg.media).photo as Photo;


                                    var filename = $"{photo.id}TEMP.jpg";
                                    labelStatus.Text = "Downloading " + filename;



                                    var finalPath = downloadFolder + "/" + filename;



                                    using (var fileStream = File.Create(finalPath))
                                    {
                                        var type = await client.DownloadFileAsync(photo, fileStream);

                                        fileStream.Close(); // necessary for the renaming
                                        Console.WriteLine("Download finished");
                                        if (!(type is Storage_FileType.unknown) && !(type is Storage_FileType.partial))
                                        {
                                            var newFilename = $"{photo.id}.{type}";

                                            while (File.Exists(downloadFolder + "/" + newFilename))
                                            {
                                                newFilename = "Copy_" + newFilename;
                                            }

                                            newFilename = downloadFolder + "/" + newFilename;


                                            File.Move(finalPath, newFilename); // rename extension
                                        }
                                    }
                                }
                            }


                        }
                        offset_id = messages.Messages[messages.Messages.Count() - 1].ID;

                    }



                    MessageBox.Show("Download completed!");
                }
                else
                {
                    MessageBox.Show("Please select a chat to download media.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\n---------\n" + ex.Message);
            }
        }

        //private async Task DownloadMedia(FileLocation fileLocation)
        //{
        //    if (fileLocation == null) return;

        //    // Get the file path and URL for download
        //    var file = await client.DownloadFileAsync(fileLocation, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TelegramDownloads", ""));

        //    // Ensure the directory exists
        //    Directory.CreateDirectory(Path.GetDirectoryName(file));

        //    using (var client = new WebClient())
        //    {
        //        client.DownloadFileCompleted += (s, e) =>
        //        {
        //            Console.WriteLine($"Downloaded: {fileLocation.FileName}");
        //        };

        //        // Start the download
        //        await client.DownloadFileTaskAsync(new Uri(fileLocation.url), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TelegramDownloads", fileLocation.FileName));
        //    }
        //}
    }
    public class ListItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedToolWindow
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox inputBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 150, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.ShowDialog();
            return inputBox.Text;
        }
    }
}