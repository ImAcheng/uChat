namespace uChat_Desktop {
    partial class FormLogin {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            pageHeader1 = new AntdUI.PageHeader();
            tbUsername = new AntdUI.Input();
            tbPassword = new AntdUI.Input();
            lbUsername = new AntdUI.Label();
            lbPassword = new AntdUI.Label();
            btnLogin = new AntdUI.Button();
            btnExit = new AntdUI.Button();
            lbFailedMessage = new AntdUI.Label();
            llb_go_create_acc = new LinkLabel();
            SuspendLayout();
            // 
            // pageHeader1
            // 
            pageHeader1.ColorScheme = AntdUI.TAMode.Dark;
            pageHeader1.Dock = DockStyle.Top;
            pageHeader1.Location = new Point(0, 0);
            pageHeader1.MaximizeBox = false;
            pageHeader1.MinimizeBox = false;
            pageHeader1.Name = "pageHeader1";
            pageHeader1.ShowButton = true;
            pageHeader1.Size = new Size(650, 35);
            pageHeader1.TabIndex = 0;
            pageHeader1.Text = " ";
            // 
            // tbUsername
            // 
            tbUsername.BackColor = Color.FromArgb(60, 60, 60);
            tbUsername.ForeColor = Color.White;
            tbUsername.Location = new Point(130, 101);
            tbUsername.Name = "tbUsername";
            tbUsername.PrefixText = "";
            tbUsername.Size = new Size(388, 50);
            tbUsername.TabIndex = 1;
            // 
            // tbPassword
            // 
            tbPassword.BackColor = Color.FromArgb(60, 60, 60);
            tbPassword.ForeColor = Color.White;
            tbPassword.Location = new Point(130, 183);
            tbPassword.Name = "tbPassword";
            tbPassword.PrefixText = "";
            tbPassword.Size = new Size(388, 50);
            tbPassword.TabIndex = 2;
            // 
            // lbUsername
            // 
            lbUsername.ForeColor = Color.White;
            lbUsername.Location = new Point(135, 79);
            lbUsername.Name = "lbUsername";
            lbUsername.Size = new Size(380, 23);
            lbUsername.TabIndex = 3;
            lbUsername.Text = "Username";
            // 
            // lbPassword
            // 
            lbPassword.ForeColor = Color.White;
            lbPassword.Location = new Point(135, 162);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(380, 23);
            lbPassword.TabIndex = 4;
            lbPassword.Text = "Password";
            // 
            // btnLogin
            // 
            btnLogin.DefaultBack = Color.DarkGreen;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(130, 322);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(388, 50);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.Click += btnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.DefaultBack = SystemColors.ControlDarkDark;
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(130, 378);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(388, 50);
            btnExit.TabIndex = 6;
            btnExit.Text = "Exit";
            btnExit.Click += btnExit_Click;
            // 
            // lbFailedMessage
            // 
            lbFailedMessage.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lbFailedMessage.ForeColor = Color.Red;
            lbFailedMessage.Location = new Point(135, 240);
            lbFailedMessage.Name = "lbFailedMessage";
            lbFailedMessage.Size = new Size(380, 34);
            lbFailedMessage.TabIndex = 7;
            lbFailedMessage.Text = "(Failed Messages)";
            // 
            // llb_go_create_acc
            // 
            llb_go_create_acc.ActiveLinkColor = Color.DodgerBlue;
            llb_go_create_acc.LinkColor = Color.DodgerBlue;
            llb_go_create_acc.Location = new Point(135, 299);
            llb_go_create_acc.Name = "llb_go_create_acc";
            llb_go_create_acc.Size = new Size(380, 20);
            llb_go_create_acc.TabIndex = 8;
            llb_go_create_acc.TabStop = true;
            llb_go_create_acc.Text = "llb_go_create_acc";
            llb_go_create_acc.TextAlign = ContentAlignment.MiddleRight;
            llb_go_create_acc.VisitedLinkColor = Color.SteelBlue;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(650, 480);
            Controls.Add(llb_go_create_acc);
            Controls.Add(lbFailedMessage);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(tbPassword);
            Controls.Add(tbUsername);
            Controls.Add(lbPassword);
            Controls.Add(lbUsername);
            Controls.Add(pageHeader1);
            Dark = true;
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            Mode = AntdUI.TAMode.Dark;
            Name = "FormLogin";
            Text = "uChat - Login";
            Load += FormLogin_Load;
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Input tbUsername;
        private AntdUI.Input tbPassword;
        private AntdUI.Label lbUsername;
        private AntdUI.Label lbPassword;
        private AntdUI.Button btnLogin;
        private AntdUI.Button btnExit;
        private AntdUI.Label lbFailedMessage;
        private LinkLabel llb_go_create_acc;
    }
}
