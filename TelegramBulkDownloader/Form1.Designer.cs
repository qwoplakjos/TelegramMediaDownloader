namespace TelegramBulkDownloader
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.apiIdTextBox = new System.Windows.Forms.TextBox();
            this.apiHashTextBox = new System.Windows.Forms.TextBox();
            this.phoneNumberTextBox = new System.Windows.Forms.TextBox();
            this.requestCodeButton = new System.Windows.Forms.Button();
            this.downloadButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.listBoxChannels = new System.Windows.Forms.ListBox();
            this.skipCheckbox = new System.Windows.Forms.CheckBox();
            this.searchTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // apiIdTextBox
            // 
            this.apiIdTextBox.Location = new System.Drawing.Point(93, 9);
            this.apiIdTextBox.Name = "apiIdTextBox";
            this.apiIdTextBox.Size = new System.Drawing.Size(163, 20);
            this.apiIdTextBox.TabIndex = 0;
            // 
            // apiHashTextBox
            // 
            this.apiHashTextBox.Location = new System.Drawing.Point(93, 32);
            this.apiHashTextBox.Name = "apiHashTextBox";
            this.apiHashTextBox.Size = new System.Drawing.Size(163, 20);
            this.apiHashTextBox.TabIndex = 1;
            // 
            // phoneNumberTextBox
            // 
            this.phoneNumberTextBox.Location = new System.Drawing.Point(93, 55);
            this.phoneNumberTextBox.Name = "phoneNumberTextBox";
            this.phoneNumberTextBox.Size = new System.Drawing.Size(163, 20);
            this.phoneNumberTextBox.TabIndex = 2;
            // 
            // requestCodeButton
            // 
            this.requestCodeButton.Location = new System.Drawing.Point(11, 82);
            this.requestCodeButton.Name = "requestCodeButton";
            this.requestCodeButton.Size = new System.Drawing.Size(245, 25);
            this.requestCodeButton.TabIndex = 4;
            this.requestCodeButton.Text = "Request Code";
            this.requestCodeButton.Click += new System.EventHandler(this.requestCodeButton_Click);
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(11, 112);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(244, 25);
            this.downloadButton.TabIndex = 5;
            this.downloadButton.Text = "Download";
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "API ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "API Hash:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Phone number:";
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(12, 171);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(244, 13);
            this.labelStatus.TabIndex = 9;
            this.labelStatus.Text = "Status:";
            // 
            // listBoxChannels
            // 
            this.listBoxChannels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxChannels.FormattingEnabled = true;
            this.listBoxChannels.IntegralHeight = false;
            this.listBoxChannels.Location = new System.Drawing.Point(261, 32);
            this.listBoxChannels.Name = "listBoxChannels";
            this.listBoxChannels.Size = new System.Drawing.Size(288, 152);
            this.listBoxChannels.TabIndex = 10;
            // 
            // skipCheckbox
            // 
            this.skipCheckbox.AutoSize = true;
            this.skipCheckbox.Location = new System.Drawing.Point(15, 147);
            this.skipCheckbox.Name = "skipCheckbox";
            this.skipCheckbox.Size = new System.Drawing.Size(166, 17);
            this.skipCheckbox.TabIndex = 11;
            this.skipCheckbox.Text = "Skip already downloaded files";
            this.skipCheckbox.UseVisualStyleBackColor = true;
            // 
            // searchTextbox
            // 
            this.searchTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextbox.Location = new System.Drawing.Point(309, 6);
            this.searchTextbox.Name = "searchTextbox";
            this.searchTextbox.Size = new System.Drawing.Size(240, 20);
            this.searchTextbox.TabIndex = 12;
            this.searchTextbox.TextChanged += new System.EventHandler(this.searchTextbox_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(262, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Search:";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(555, 189);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.searchTextbox);
            this.Controls.Add(this.skipCheckbox);
            this.Controls.Add(this.listBoxChannels);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.apiIdTextBox);
            this.Controls.Add(this.apiHashTextBox);
            this.Controls.Add(this.phoneNumberTextBox);
            this.Controls.Add(this.requestCodeButton);
            this.Controls.Add(this.downloadButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Telegram Media Downloader";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox apiIdTextBox;
        private System.Windows.Forms.TextBox apiHashTextBox;
        private System.Windows.Forms.TextBox phoneNumberTextBox;
        private System.Windows.Forms.Button requestCodeButton;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ListBox listBoxChannels;
        private System.Windows.Forms.CheckBox skipCheckbox;
        private System.Windows.Forms.TextBox searchTextbox;
        private System.Windows.Forms.Label label4;
    }
}

