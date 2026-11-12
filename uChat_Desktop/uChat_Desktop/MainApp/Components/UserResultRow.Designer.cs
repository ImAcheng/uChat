namespace uChat_Desktop.MainApp.Components {
    partial class UserResultRow {
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
            lbUsername = new Label();
            panel1 = new Panel();
            btnGoMsg = new AntdUI.Button();
            btnAddFrd = new AntdUI.Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lbUsername
            // 
            lbUsername.Dock = DockStyle.Fill;
            lbUsername.Location = new Point(0, 0);
            lbUsername.Name = "lbUsername";
            lbUsername.Padding = new Padding(10, 0, 0, 0);
            lbUsername.Size = new Size(300, 50);
            lbUsername.TabIndex = 0;
            lbUsername.Text = "Username";
            lbUsername.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(btnGoMsg);
            panel1.Controls.Add(btnAddFrd);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(300, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(100, 50);
            panel1.TabIndex = 1;
            // 
            // btnGoMsg
            // 
            btnGoMsg.BackColor = Color.FromArgb(64, 64, 64);
            btnGoMsg.DefaultBack = Color.Transparent;
            btnGoMsg.Dock = DockStyle.Fill;
            btnGoMsg.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnGoMsg.ForeColor = Color.White;
            btnGoMsg.IconHoverSvg = "MessageOutlined";
            btnGoMsg.IconSvg = "MessageOutlined";
            btnGoMsg.Location = new Point(50, 0);
            btnGoMsg.Name = "btnGoMsg";
            btnGoMsg.Size = new Size(50, 50);
            btnGoMsg.TabIndex = 0;
            // 
            // btnAddFrd
            // 
            btnAddFrd.BackColor = SystemColors.Control;
            btnAddFrd.DefaultBack = Color.Transparent;
            btnAddFrd.Dock = DockStyle.Left;
            btnAddFrd.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnAddFrd.ForeColor = Color.White;
            btnAddFrd.IconHoverSvg = "UserAddOutlined";
            btnAddFrd.IconSvg = "UserAddOutlined";
            btnAddFrd.Location = new Point(0, 0);
            btnAddFrd.Name = "btnAddFrd";
            btnAddFrd.Size = new Size(50, 50);
            btnAddFrd.TabIndex = 1;
            // 
            // UserResultRow
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(lbUsername);
            Controls.Add(panel1);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            Margin = new Padding(4);
            Name = "UserResultRow";
            Size = new Size(400, 50);
            Load += UserResultRow_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lbUsername;
        private Panel panel1;
        private AntdUI.Button btnGoMsg;
        private AntdUI.Button btnAddFrd;
    }
}
