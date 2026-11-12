namespace uChat_Desktop.MainApp.Grid {
    partial class ChatDisplay {
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
            panelTopBar = new AntdUI.Panel();
            lbName = new Label();
            panel1 = new AntdUI.Panel();
            tbMessage = new AntdUI.Input();
            btnSendMsg = new AntdUI.Button();
            panelMessages = new Panel();
            panelTopBar.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelTopBar
            // 
            panelTopBar.Back = Color.FromArgb(60, 60, 60);
            panelTopBar.Controls.Add(lbName);
            panelTopBar.Dock = DockStyle.Top;
            panelTopBar.Location = new Point(0, 0);
            panelTopBar.Margin = new Padding(10);
            panelTopBar.Name = "panelTopBar";
            panelTopBar.Shadow = 5;
            panelTopBar.ShadowColor = Color.Black;
            panelTopBar.ShadowOpacity = 0.3F;
            panelTopBar.Size = new Size(600, 45);
            panelTopBar.TabIndex = 0;
            panelTopBar.Text = "panel1";
            // 
            // lbName
            // 
            lbName.BackColor = Color.FromArgb(60, 60, 60);
            lbName.Dock = DockStyle.Left;
            lbName.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lbName.Location = new Point(5, 5);
            lbName.Name = "lbName";
            lbName.Padding = new Padding(4, 0, 0, 0);
            lbName.Size = new Size(315, 35);
            lbName.TabIndex = 0;
            lbName.Text = "Username";
            lbName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Back = Color.FromArgb(60, 60, 60);
            panel1.Controls.Add(tbMessage);
            panel1.Controls.Add(btnSendMsg);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 440);
            panel1.Margin = new Padding(10);
            panel1.Name = "panel1";
            panel1.Shadow = 5;
            panel1.ShadowColor = Color.Black;
            panel1.ShadowOpacity = 0.3F;
            panel1.Size = new Size(600, 60);
            panel1.TabIndex = 1;
            panel1.Text = "panel1";
            // 
            // tbMessage
            // 
            tbMessage.BackColor = Color.FromArgb(60, 60, 60);
            tbMessage.BorderWidth = 0F;
            tbMessage.Dock = DockStyle.Fill;
            tbMessage.ForeColor = Color.White;
            tbMessage.Location = new Point(5, 5);
            tbMessage.Name = "tbMessage";
            tbMessage.PlaceholderColor = Color.Gray;
            tbMessage.PlaceholderText = "Send some messages!";
            tbMessage.Size = new Size(540, 50);
            tbMessage.TabIndex = 0;
            // 
            // btnSendMsg
            // 
            btnSendMsg.DefaultBack = Color.RoyalBlue;
            btnSendMsg.Dock = DockStyle.Right;
            btnSendMsg.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnSendMsg.ForeColor = Color.White;
            btnSendMsg.IconSvg = "SendOutlined";
            btnSendMsg.Location = new Point(545, 5);
            btnSendMsg.Margin = new Padding(0);
            btnSendMsg.Name = "btnSendMsg";
            btnSendMsg.Size = new Size(50, 50);
            btnSendMsg.TabIndex = 1;
            // 
            // panelMessages
            // 
            panelMessages.Dock = DockStyle.Fill;
            panelMessages.Location = new Point(0, 45);
            panelMessages.Name = "panelMessages";
            panelMessages.Padding = new Padding(5, 0, 5, 0);
            panelMessages.Size = new Size(600, 395);
            panelMessages.TabIndex = 2;
            // 
            // ChatDisplay
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(panelMessages);
            Controls.Add(panel1);
            Controls.Add(panelTopBar);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Margin = new Padding(4);
            Name = "ChatDisplay";
            Size = new Size(600, 500);
            Load += ChatDisplay_Load;
            panelTopBar.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Panel panelTopBar;
        private AntdUI.Panel panel1;
        private AntdUI.Input tbMessage;
        private AntdUI.Button btnSendMsg;
        private Label lbName;
        private Panel panelMessages;
    }
}
