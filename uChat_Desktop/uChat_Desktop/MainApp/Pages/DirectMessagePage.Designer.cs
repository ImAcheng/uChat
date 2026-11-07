namespace uChat_Desktop.MainApp.Pages {
    partial class DirectMessagePage {
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
            splitter1 = new AntdUI.Splitter();
            panelChatHeadsContainer = new AntdUI.Panel();
            panelChatHeads = new AntdUI.Panel();
            panel4 = new Panel();
            label1 = new Label();
            panel3 = new Panel();
            panel2 = new AntdUI.Panel();
            btnNotifications = new AntdUI.Button();
            btnFriends = new AntdUI.Button();
            panel1 = new AntdUI.Panel();
            panelMainContainer = new AntdUI.Panel();
            panelFrdOrSrch = new Panel();
            panel5 = new Panel();
            input1 = new AntdUI.Input();
            btnFindUser = new AntdUI.Button();
            ((System.ComponentModel.ISupportInitialize)splitter1).BeginInit();
            splitter1.Panel1.SuspendLayout();
            splitter1.Panel2.SuspendLayout();
            splitter1.SuspendLayout();
            panelChatHeadsContainer.SuspendLayout();
            panelChatHeads.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            panelMainContainer.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // splitter1
            // 
            splitter1.BackColor = Color.FromArgb(68, 68, 68);
            splitter1.Dock = DockStyle.Fill;
            splitter1.Location = new Point(0, 0);
            splitter1.Name = "splitter1";
            // 
            // splitter1.Panel1
            // 
            splitter1.Panel1.Controls.Add(panelChatHeadsContainer);
            splitter1.Panel1.Padding = new Padding(5, 0, 0, 5);
            splitter1.Panel1MinSize = 100;
            // 
            // splitter1.Panel2
            // 
            splitter1.Panel2.Controls.Add(panel1);
            splitter1.Panel2.Padding = new Padding(0, 0, 5, 5);
            splitter1.Panel2MinSize = 640;
            splitter1.Size = new Size(850, 515);
            splitter1.SplitterDistance = 205;
            splitter1.SplitterSize = 10;
            splitter1.TabIndex = 0;
            // 
            // panelChatHeadsContainer
            // 
            panelChatHeadsContainer.Back = Color.FromArgb(64, 64, 64);
            panelChatHeadsContainer.BorderColor = Color.Gray;
            panelChatHeadsContainer.BorderWidth = 1F;
            panelChatHeadsContainer.Controls.Add(panelChatHeads);
            panelChatHeadsContainer.Dock = DockStyle.Fill;
            panelChatHeadsContainer.Location = new Point(5, 0);
            panelChatHeadsContainer.Name = "panelChatHeadsContainer";
            panelChatHeadsContainer.RadiusAlign = AntdUI.TAlignRound.Left;
            panelChatHeadsContainer.Size = new Size(200, 510);
            panelChatHeadsContainer.TabIndex = 0;
            panelChatHeadsContainer.Text = "panel1";
            // 
            // panelChatHeads
            // 
            panelChatHeads.Back = Color.FromArgb(64, 64, 64);
            panelChatHeads.BackColor = Color.Transparent;
            panelChatHeads.Controls.Add(panel4);
            panelChatHeads.Controls.Add(panel3);
            panelChatHeads.Controls.Add(panel2);
            panelChatHeads.Dock = DockStyle.Fill;
            panelChatHeads.Location = new Point(1, 1);
            panelChatHeads.Name = "panelChatHeads";
            panelChatHeads.RadiusAlign = AntdUI.TAlignRound.Left;
            panelChatHeads.Size = new Size(198, 508);
            panelChatHeads.TabIndex = 0;
            panelChatHeads.Text = "panel2";
            // 
            // panel4
            // 
            panel4.Controls.Add(label1);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 86);
            panel4.Name = "panel4";
            panel4.Size = new Size(198, 20);
            panel4.TabIndex = 2;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Microsoft JhengHei UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(198, 20);
            label1.TabIndex = 0;
            label1.Text = "Direct Message";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 85);
            panel3.Name = "panel3";
            panel3.Size = new Size(198, 1);
            panel3.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Back = Color.FromArgb(64, 64, 64);
            panel2.Controls.Add(btnNotifications);
            panel2.Controls.Add(btnFriends);
            panel2.Dock = DockStyle.Top;
            panel2.InnerPadding = new Padding(5);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(198, 85);
            panel2.TabIndex = 0;
            panel2.Text = "panel2";
            // 
            // btnNotifications
            // 
            btnNotifications.DefaultBack = Color.FromArgb(64, 64, 64);
            btnNotifications.Dock = DockStyle.Top;
            btnNotifications.ForeColor = Color.White;
            btnNotifications.Location = new Point(0, 40);
            btnNotifications.Name = "btnNotifications";
            btnNotifications.Size = new Size(198, 40);
            btnNotifications.TabIndex = 1;
            btnNotifications.Text = "Notifications";
            // 
            // btnFriends
            // 
            btnFriends.DefaultBack = Color.FromArgb(64, 64, 64);
            btnFriends.Dock = DockStyle.Top;
            btnFriends.ForeColor = Color.White;
            btnFriends.Location = new Point(0, 0);
            btnFriends.Name = "btnFriends";
            btnFriends.Size = new Size(198, 40);
            btnFriends.TabIndex = 0;
            btnFriends.Text = "Friends";
            // 
            // panel1
            // 
            panel1.Back = Color.FromArgb(60, 60, 60);
            panel1.BorderColor = Color.Gray;
            panel1.BorderWidth = 1F;
            panel1.Controls.Add(panelMainContainer);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(2);
            panel1.RadiusAlign = AntdUI.TAlignRound.Right;
            panel1.Size = new Size(636, 510);
            panel1.TabIndex = 1;
            panel1.Text = "panel1";
            // 
            // panelMainContainer
            // 
            panelMainContainer.Back = Color.FromArgb(60, 60, 60);
            panelMainContainer.BackColor = Color.Transparent;
            panelMainContainer.Controls.Add(panelFrdOrSrch);
            panelMainContainer.Controls.Add(panel5);
            panelMainContainer.Dock = DockStyle.Fill;
            panelMainContainer.Location = new Point(3, 3);
            panelMainContainer.Name = "panelMainContainer";
            panelMainContainer.RadiusAlign = AntdUI.TAlignRound.Right;
            panelMainContainer.Size = new Size(630, 504);
            panelMainContainer.TabIndex = 1;
            panelMainContainer.Text = "panel2";
            // 
            // panelFrdOrSrch
            // 
            panelFrdOrSrch.BackColor = Color.FromArgb(60, 60, 60);
            panelFrdOrSrch.Dock = DockStyle.Fill;
            panelFrdOrSrch.Location = new Point(0, 52);
            panelFrdOrSrch.Name = "panelFrdOrSrch";
            panelFrdOrSrch.Size = new Size(630, 452);
            panelFrdOrSrch.TabIndex = 7;
            // 
            // panel5
            // 
            panel5.Controls.Add(input1);
            panel5.Controls.Add(btnFindUser);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(630, 52);
            panel5.TabIndex = 0;
            // 
            // input1
            // 
            input1.BackColor = Color.FromArgb(72, 72, 72);
            input1.Dock = DockStyle.Fill;
            input1.ForeColor = Color.White;
            input1.Location = new Point(0, 0);
            input1.Name = "input1";
            input1.PlaceholderColor = Color.Gray;
            input1.PlaceholderText = "Find people with their username!";
            input1.Size = new Size(578, 52);
            input1.TabIndex = 7;
            // 
            // btnFindUser
            // 
            btnFindUser.BackColor = Color.FromArgb(72, 72, 72);
            btnFindUser.DefaultBack = Color.FromArgb(72, 72, 72);
            btnFindUser.Dock = DockStyle.Right;
            btnFindUser.ForeColor = Color.White;
            btnFindUser.IconSvg = "SearchOutlined";
            btnFindUser.Location = new Point(578, 0);
            btnFindUser.Name = "btnFindUser";
            btnFindUser.Size = new Size(52, 52);
            btnFindUser.TabIndex = 8;
            btnFindUser.Click += btnFindUser_Click;
            // 
            // DirectMessagePage
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(splitter1);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Margin = new Padding(4);
            Name = "DirectMessagePage";
            Size = new Size(850, 515);
            splitter1.Panel1.ResumeLayout(false);
            splitter1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitter1).EndInit();
            splitter1.ResumeLayout(false);
            panelChatHeadsContainer.ResumeLayout(false);
            panelChatHeads.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panelMainContainer.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Splitter splitter1;
        private AntdUI.Panel panelChatHeadsContainer;
        private AntdUI.Panel panel1;
        private AntdUI.Panel panelChatHeads;
        private AntdUI.Panel panelMainContainer;
        private AntdUI.Panel panel2;
        private Panel panel3;
        private AntdUI.Button btnFriends;
        private AntdUI.Button btnNotifications;
        private Panel panel4;
        private Label label1;
        private Panel panel5;
        private Panel panelFrdOrSrch;
        private AntdUI.Input input1;
        private AntdUI.Button btnFindUser;
    }
}
