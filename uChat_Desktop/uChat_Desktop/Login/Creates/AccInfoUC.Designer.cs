namespace uChat_Desktop.Login.Creates {
    partial class AccInfoUC {
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
            tbPasswordConfirm = new AntdUI.Input();
            label1 = new AntdUI.Label();
            lbFailedMessage = new AntdUI.Label();
            btnReturn = new AntdUI.Button();
            btnNext = new AntdUI.Button();
            tbPassword = new AntdUI.Input();
            tbUsername = new AntdUI.Input();
            lbPassword = new AntdUI.Label();
            lbUsername = new AntdUI.Label();
            SuspendLayout();
            // 
            // tbPasswordConfirm
            // 
            tbPasswordConfirm.BackColor = Color.FromArgb(60, 60, 60);
            tbPasswordConfirm.ForeColor = Color.White;
            tbPasswordConfirm.Location = new Point(131, 204);
            tbPasswordConfirm.Name = "tbPasswordConfirm";
            tbPasswordConfirm.PrefixText = "";
            tbPasswordConfirm.Size = new Size(388, 50);
            tbPasswordConfirm.TabIndex = 22;
            // 
            // label1
            // 
            label1.ForeColor = Color.White;
            label1.Location = new Point(136, 183);
            label1.Name = "label1";
            label1.Size = new Size(380, 23);
            label1.TabIndex = 28;
            label1.Text = "Confirm Password";
            // 
            // lbFailedMessage
            // 
            lbFailedMessage.Font = new Font("Microsoft JhengHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            lbFailedMessage.ForeColor = Color.Red;
            lbFailedMessage.Location = new Point(139, 260);
            lbFailedMessage.Name = "lbFailedMessage";
            lbFailedMessage.Size = new Size(380, 34);
            lbFailedMessage.TabIndex = 26;
            lbFailedMessage.Text = "(Failed Messages)";
            // 
            // btnReturn
            // 
            btnReturn.DefaultBack = SystemColors.ControlDarkDark;
            btnReturn.ForeColor = Color.White;
            btnReturn.Location = new Point(131, 367);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(388, 50);
            btnReturn.TabIndex = 25;
            btnReturn.Text = "Cancel";
            btnReturn.Click += btnReturn_Click;
            // 
            // btnNext
            // 
            btnNext.DefaultBack = Color.DarkGreen;
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(131, 311);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(388, 50);
            btnNext.TabIndex = 24;
            btnNext.Text = "Next";
            btnNext.Click += btnNext_Click;
            // 
            // tbPassword
            // 
            tbPassword.BackColor = Color.FromArgb(60, 60, 60);
            tbPassword.ForeColor = Color.White;
            tbPassword.Location = new Point(131, 132);
            tbPassword.Name = "tbPassword";
            tbPassword.PrefixText = "";
            tbPassword.Size = new Size(388, 50);
            tbPassword.TabIndex = 21;
            // 
            // tbUsername
            // 
            tbUsername.BackColor = Color.FromArgb(60, 60, 60);
            tbUsername.ForeColor = Color.White;
            tbUsername.Location = new Point(131, 50);
            tbUsername.Name = "tbUsername";
            tbUsername.PrefixText = "";
            tbUsername.Size = new Size(388, 50);
            tbUsername.TabIndex = 20;
            // 
            // lbPassword
            // 
            lbPassword.ForeColor = Color.White;
            lbPassword.Location = new Point(136, 111);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(380, 23);
            lbPassword.TabIndex = 23;
            lbPassword.Text = "Password";
            // 
            // lbUsername
            // 
            lbUsername.ForeColor = Color.White;
            lbUsername.Location = new Point(136, 28);
            lbUsername.Name = "lbUsername";
            lbUsername.Size = new Size(380, 23);
            lbUsername.TabIndex = 22;
            lbUsername.Text = "Username";
            // 
            // AccInfoUC
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(tbPasswordConfirm);
            Controls.Add(label1);
            Controls.Add(lbFailedMessage);
            Controls.Add(btnReturn);
            Controls.Add(btnNext);
            Controls.Add(tbPassword);
            Controls.Add(tbUsername);
            Controls.Add(lbPassword);
            Controls.Add(lbUsername);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "AccInfoUC";
            Size = new Size(650, 445);
            Load += AccInfoUC_Load;
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Input tbPasswordConfirm;
        private AntdUI.Label label1;
        private AntdUI.Label lbFailedMessage;
        private AntdUI.Button btnReturn;
        private AntdUI.Button btnNext;
        private AntdUI.Input tbPassword;
        private AntdUI.Input tbUsername;
        private AntdUI.Label lbPassword;
        private AntdUI.Label lbUsername;
    }
}
