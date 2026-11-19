namespace uChat_Desktop.MainApp.Components {
    partial class MessageDisplayer {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageDisplayer));
            lbUsername = new Label();
            panel1 = new Panel();
            lbMessage = new TextBox();
            panel2 = new Panel();
            imgAvatar = new PictureBox();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imgAvatar).BeginInit();
            SuspendLayout();
            // 
            // lbUsername
            // 
            lbUsername.AutoSize = true;
            lbUsername.Dock = DockStyle.Top;
            lbUsername.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lbUsername.ForeColor = Color.White;
            lbUsername.Location = new Point(45, 5);
            lbUsername.Margin = new Padding(5);
            lbUsername.Name = "lbUsername";
            lbUsername.Size = new Size(87, 20);
            lbUsername.TabIndex = 1;
            lbUsername.Text = "Username";
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(45, 25);
            panel1.Name = "panel1";
            panel1.Size = new Size(2, 20);
            panel1.TabIndex = 2;
            // 
            // lbMessage
            // 
            lbMessage.BackColor = Color.FromArgb(64, 64, 64);
            lbMessage.BorderStyle = BorderStyle.None;
            lbMessage.CausesValidation = false;
            lbMessage.Dock = DockStyle.Fill;
            lbMessage.ForeColor = Color.White;
            lbMessage.Location = new Point(47, 25);
            lbMessage.Multiline = true;
            lbMessage.Name = "lbMessage";
            lbMessage.ReadOnly = true;
            lbMessage.Size = new Size(548, 20);
            lbMessage.TabIndex = 3;
            lbMessage.Text = "Message";
            // 
            // panel2
            // 
            panel2.Controls.Add(imgAvatar);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(5, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(40, 40);
            panel2.TabIndex = 4;
            // 
            // imgAvatar
            // 
            imgAvatar.Dock = DockStyle.Top;
            imgAvatar.Image = (Image)resources.GetObject("imgAvatar.Image");
            imgAvatar.Location = new Point(0, 0);
            imgAvatar.Name = "imgAvatar";
            imgAvatar.Size = new Size(40, 40);
            imgAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            imgAvatar.TabIndex = 1;
            imgAvatar.TabStop = false;
            // 
            // MessageDisplayer
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(lbMessage);
            Controls.Add(panel1);
            Controls.Add(lbUsername);
            Controls.Add(panel2);
            Name = "MessageDisplayer";
            Padding = new Padding(5);
            Size = new Size(600, 50);
            Load += MessageDisplayer_Load;
            SizeChanged += MessageDisplayer_SizeChanged;
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)imgAvatar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lbUsername;
        private Panel panel1;
        private TextBox lbMessage;
        private Panel panel2;
        private PictureBox imgAvatar;
    }
}
