using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TL;
using WTelegram;


namespace TelegramBulkDownloader
{
    public partial class Form1 : Form
    {

        string credsFile = "credentials.txt";
        string downloadFolder = "downloads";
        static Client client;
        List<ListItem> chats;


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
                case "api_id": return apiIdTextBox.Text;
                case "api_hash": return apiHashTextBox.Text;
                case "phone_number": return phoneNumberTextBox.Text;
                case "verification_code": return Prompt.ShowDialog("Verification code:", "Code");
                case "session_pathname": return "TelegramSession.ss";

                default: return null;
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
                if (listBoxChannels.Items.Count > 0) return;

                labelStatus.Text = "Requesting auth code...";

                client = new WTelegram.Client(Config);

                var myself = await client.LoginUserIfNeeded();

                labelStatus.Text = "Authenticated as: " + myself;

                SaveCredentials();


                var chats = await client.Messages_GetAllChats();

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
                    InputPeer chat = null;

                    if (selectedChat.Value is Chat chatt)
                        chat = chatt;
                    else if (selectedChat.Value is Channel channel)
                        chat = channel;
                    else
                    {
                        MessageBox.Show("Selected item is not a valid chat or channel.");
                        return;
                    }

                    for (int offset_id = 0; ;)
                    {
                        var messages = await client.Messages_GetHistory(chat, offset_id);
                        if (messages.Messages.Length == 0) break;

                        foreach (var msgBase in messages.Messages)
                        {
                            if (msgBase is TL.Message msg)
                            {
                                string filename = null;
                                string filePath = null;

                                if (msg.media is MessageMediaDocument)
                                {
                                    var document = (msg.media as MessageMediaDocument).document as Document;
                                    if (document != null)
                                    {
                                        filename = string.IsNullOrEmpty(document.Filename)
                                            ? $"{document.id}.{document.mime_type.Split('/')[1]}"
                                            : document.Filename;



                                        filePath = Path.Combine(downloadFolder, filename);

                                        if (skipCheckbox.Checked && File.Exists(filePath))
                                        {
                                            labelStatus.Text = "Skipping " + filename;
                                            continue;
                                        }

                                        filePath = GetUniqueFilePath(filePath);
                                        labelStatus.Text = "Downloading " + filename;

                                        using (var fileStream = File.Create(filePath))
                                        {
                                            await client.DownloadFileAsync(document, fileStream);
                                        }
                                        labelStatus.Text = "Download finished for " + filename;
                                    }
                                }
                                else if (msg.media is MessageMediaPhoto mmp)
                                {
                                    var photo = mmp.photo as Photo;
                                    filename = $"{photo.id}";

                                    string tempFilePath = Path.Combine(downloadFolder, filename);


                                    if (skipCheckbox.Checked)
                                    {

                                        var types = new string[] { ".jpeg", ".jpg", ".gif", ".pdf", ".png", ".mp4" };

                                        bool exists = false;

                                        foreach (var t in types)
                                        {
                                            if (File.Exists(tempFilePath + t))
                                            {
                                                exists = true;
                                                break;
                                            }
                                        }

                                        if (exists)
                                        {
                                            labelStatus.Text = "Skipping " + filename;
                                            continue;
                                        }
                                    }

                                    var photoType = Storage_FileType.jpeg;

                                    using (var fileStream = File.Create(tempFilePath))
                                    {
                                        photoType = await client.DownloadFileAsync(photo, fileStream);
                                        await fileStream.FlushAsync();
                                        fileStream.Close();
                                    }

                                    if (photoType == Storage_FileType.partial) photoType = Storage_FileType.jpeg;

                                    string extension = photoType.ToString().ToLower();
                                    filename = $"{photo.id}.{extension}";
                                    filePath = Path.Combine(downloadFolder, filename);


                                    if (skipCheckbox.Checked && File.Exists(filePath))
                                    {
                                        labelStatus.Text = "Skipping " + filename;
                                        File.Delete(tempFilePath);
                                        continue;
                                    }

                                    filePath = GetUniqueFilePath(filePath);
                                    File.Move(tempFilePath, filePath);
                                    labelStatus.Text = "Download finished for " + filename;

                                }
                            }
                        }

                        offset_id = messages.Messages.Last().ID;
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

        // Helper method to generate unique file path if file already exists
        private string GetUniqueFilePath(string filePath)
        {
            string uniqueFilePath = filePath;
            int copyIndex = 1;
            while (File.Exists(uniqueFilePath))
            {
                string fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);
                uniqueFilePath = Path.Combine(downloadFolder, $"{fileNameOnly}_Copy{copyIndex++}{extension}");
            }
            return uniqueFilePath;
        }


        private void searchTextbox_TextChanged(object sender, EventArgs e)
        {
            if (chats is null) chats = listBoxChannels.Items.Cast<ListItem>().ToList();

            listBoxChannels.Items.Clear();

            if (string.IsNullOrEmpty(searchTextbox.Text))
            {
                listBoxChannels.Items.AddRange(chats.ToArray());
                return;
            }

            var filtered = chats.Where(channel => channel.Text.ToLower().Contains(searchTextbox.Text.ToLower())).ToArray();
            listBoxChannels.Items.AddRange(filtered);
        }
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
                Width = 230,
                Height = 100,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label textLabel = new Label()
            {
                Left = 10,
                Top = 2,
                Width = 90,
                Text = text,
            };

            TextBox inputBox = new TextBox()
            {
                Left = 100,
                Top = 0,
                Width = 100,
            };

            Button confirmation = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Left = 10,
                Width = 190,
                Top = inputBox.Bottom + 10,
            };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : string.Empty;
        }

    }
}