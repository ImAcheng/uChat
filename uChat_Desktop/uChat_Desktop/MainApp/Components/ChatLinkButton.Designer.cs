namespace uChat_Desktop.MainApp.Components {
    partial class ChatLinkButton {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatLinkButton));
            panel1 = new AntdUI.Panel();
            panelProflePicBorder = new Panel();
            imgProfilePicture = new PictureBox();
            lbChatTitle = new AntdUI.Label();
            panel1.SuspendLayout();
            panelProflePicBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imgProfilePicture).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Back = Color.FromArgb(64, 64, 64);
            panel1.Controls.Add(panelProflePicBorder);
            panel1.Controls.Add(lbChatTitle);
            panel1.Dock = DockStyle.Fill;
            panel1.ForeColor = Color.White;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 50);
            panel1.TabIndex = 0;
            panel1.Text = "panel1";
            // 
            // panelProflePicBorder
            // 
            panelProflePicBorder.BackColor = Color.DimGray;
            panelProflePicBorder.Controls.Add(imgProfilePicture);
            panelProflePicBorder.Location = new Point(3, 3);
            panelProflePicBorder.Name = "panelProflePicBorder";
            panelProflePicBorder.Padding = new Padding(2);
            panelProflePicBorder.Size = new Size(44, 44);
            panelProflePicBorder.TabIndex = 2;
            // 
            // imgProfilePicture
            // 
            imgProfilePicture.BackColor = Color.FromArgb(64, 64, 64);
            imgProfilePicture.Dock = DockStyle.Fill;
            imgProfilePicture.Image = (Image)resources.GetObject("imgProfilePicture.Image");
            imgProfilePicture.Location = new Point(2, 2);
            imgProfilePicture.Name = "imgProfilePicture";
            imgProfilePicture.Size = new Size(40, 40);
            imgProfilePicture.SizeMode = PictureBoxSizeMode.Zoom;
            imgProfilePicture.TabIndex = 0;
            imgProfilePicture.TabStop = false;
            // 
            // lbChatTitle
            // 
            lbChatTitle.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbChatTitle.BackColor = Color.FromArgb(64, 64, 64);
            lbChatTitle.ForeColor = Color.White;
            lbChatTitle.Location = new Point(53, 14);
            lbChatTitle.Name = "lbChatTitle";
            lbChatTitle.Size = new Size(144, 23);
            lbChatTitle.TabIndex = 1;
            lbChatTitle.Text = "Chat";
            // 
            // ChatLinkButton
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(panel1);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "ChatLinkButton";
            Size = new Size(200, 50);
            Load += ChatLinkButton_Load;
            panel1.ResumeLayout(false);
            panelProflePicBorder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)imgProfilePicture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Panel panel1;
        private PictureBox imgProfilePicture;
        private AntdUI.Label lbChatTitle;
        private Panel panelProflePicBorder;
    }
}
