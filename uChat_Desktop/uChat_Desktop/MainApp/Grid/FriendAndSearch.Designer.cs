namespace uChat_Desktop.MainApp.Grid {
    partial class FriendAndSearch {
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
            panel5 = new Panel();
            input1 = new AntdUI.Input();
            btnFindUsers = new AntdUI.Button();
            panelFrdOrSrch = new Panel();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Controls.Add(input1);
            panel5.Controls.Add(btnFindUsers);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(703, 52);
            panel5.TabIndex = 1;
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
            input1.Size = new Size(651, 52);
            input1.TabIndex = 7;
            // 
            // btnFindUsers
            // 
            btnFindUsers.BackColor = Color.FromArgb(72, 72, 72);
            btnFindUsers.DefaultBack = Color.FromArgb(72, 72, 72);
            btnFindUsers.Dock = DockStyle.Right;
            btnFindUsers.ForeColor = Color.White;
            btnFindUsers.IconSvg = "SearchOutlined";
            btnFindUsers.Location = new Point(651, 0);
            btnFindUsers.Name = "btnFindUsers";
            btnFindUsers.Size = new Size(52, 52);
            btnFindUsers.TabIndex = 8;
            btnFindUsers.Click += btnFindUsers_Click;
            // 
            // panelFrdOrSrch
            // 
            panelFrdOrSrch.BackColor = Color.FromArgb(64, 64, 64);
            panelFrdOrSrch.Dock = DockStyle.Fill;
            panelFrdOrSrch.Location = new Point(0, 52);
            panelFrdOrSrch.Name = "panelFrdOrSrch";
            panelFrdOrSrch.Size = new Size(703, 518);
            panelFrdOrSrch.TabIndex = 8;
            // 
            // FriendAndSearch
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(panelFrdOrSrch);
            Controls.Add(panel5);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "FriendAndSearch";
            Size = new Size(703, 570);
            Load += FriendAndSearch_Load;
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel5;
        private AntdUI.Input input1;
        private AntdUI.Button btnFindUsers;
        private Panel panelFrdOrSrch;
    }
}
