namespace uChat_Desktop.Login.Creates {
    partial class MoreInfoUC {
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
            btnReturn = new AntdUI.Button();
            btnCreate = new AntdUI.Button();
            tbNickname = new AntdUI.Input();
            lbDisplayName = new AntdUI.Label();
            btnPrevPage = new AntdUI.Button();
            label1 = new AntdUI.Label();
            cbGender = new AntdUI.Dropdown();
            SuspendLayout();
            // 
            // btnReturn
            // 
            btnReturn.DefaultBack = SystemColors.ControlDarkDark;
            btnReturn.ForeColor = Color.White;
            btnReturn.Location = new Point(329, 367);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(190, 50);
            btnReturn.TabIndex = 34;
            btnReturn.Text = "Cancel";
            btnReturn.Click += btnReturn_Click;
            // 
            // btnCreate
            // 
            btnCreate.DefaultBack = Color.DarkGreen;
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(131, 311);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(388, 50);
            btnCreate.TabIndex = 33;
            btnCreate.Text = "Create";
            btnCreate.Click += btnCreate_Click;
            // 
            // tbNickname
            // 
            tbNickname.BackColor = Color.FromArgb(60, 60, 60);
            tbNickname.ForeColor = Color.White;
            tbNickname.Location = new Point(131, 109);
            tbNickname.Name = "tbNickname";
            tbNickname.PrefixText = "";
            tbNickname.Size = new Size(388, 50);
            tbNickname.TabIndex = 29;
            // 
            // lbDisplayName
            // 
            lbDisplayName.ForeColor = Color.White;
            lbDisplayName.Location = new Point(136, 87);
            lbDisplayName.Name = "lbDisplayName";
            lbDisplayName.Size = new Size(380, 23);
            lbDisplayName.TabIndex = 31;
            lbDisplayName.Text = "Display Name";
            // 
            // btnPrevPage
            // 
            btnPrevPage.DefaultBack = SystemColors.ControlDarkDark;
            btnPrevPage.ForeColor = Color.White;
            btnPrevPage.Location = new Point(131, 367);
            btnPrevPage.Name = "btnPrevPage";
            btnPrevPage.Size = new Size(190, 50);
            btnPrevPage.TabIndex = 35;
            btnPrevPage.Text = "Back";
            btnPrevPage.Click += btnPrevPage_Click;
            // 
            // label1
            // 
            label1.ForeColor = Color.White;
            label1.Location = new Point(136, 184);
            label1.Name = "label1";
            label1.Size = new Size(380, 23);
            label1.TabIndex = 36;
            label1.Text = "Gender";
            // 
            // cbGender
            // 
            cbGender.Items.AddRange(new object[] { "Male", "Female", "Crossaint", "Rather not to say" });
            cbGender.Location = new Point(131, 203);
            cbGender.Name = "cbGender";
            cbGender.ShowArrow = true;
            cbGender.Size = new Size(388, 50);
            cbGender.TabIndex = 37;
            cbGender.Text = "(Please select your gender)";
            cbGender.TextAlign = ContentAlignment.MiddleLeft;
            cbGender.SelectedValueChanged += cbGender_SelectedValueChanged;
            // 
            // MoreInfoUC
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(cbGender);
            Controls.Add(label1);
            Controls.Add(btnPrevPage);
            Controls.Add(btnReturn);
            Controls.Add(btnCreate);
            Controls.Add(tbNickname);
            Controls.Add(lbDisplayName);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "MoreInfoUC";
            Size = new Size(650, 445);
            ResumeLayout(false);
        }

        #endregion
        private AntdUI.Button btnReturn;
        private AntdUI.Button btnCreate;
        private AntdUI.Input tbNickname;
        private AntdUI.Label lbDisplayName;
        private AntdUI.Button btnPrevPage;
        private AntdUI.Label label1;
        private AntdUI.Dropdown cbGender;
    }
}
