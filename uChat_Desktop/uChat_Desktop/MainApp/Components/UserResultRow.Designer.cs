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
            SuspendLayout();
            // 
            // lbUsername
            // 
            lbUsername.Dock = DockStyle.Fill;
            lbUsername.Location = new Point(0, 0);
            lbUsername.Name = "lbUsername";
            lbUsername.Padding = new Padding(10, 0, 0, 0);
            lbUsername.Size = new Size(340, 50);
            lbUsername.TabIndex = 0;
            lbUsername.Text = "Username";
            lbUsername.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(340, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(60, 50);
            panel1.TabIndex = 1;
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
            ResumeLayout(false);
        }

        #endregion

        private Label lbUsername;
        private Panel panel1;
    }
}
