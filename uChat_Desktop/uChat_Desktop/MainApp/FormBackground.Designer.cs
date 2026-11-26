namespace uChat_Desktop.MainApp {
    partial class FormBackground {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            label1 = new Label();
            lbConn = new Label();
            panelBack = new Panel();
            btnStartConnect = new AntdUI.Button();
            lbInputHint = new AntdUI.Label();
            tbIP = new AntdUI.Input();
            panelBack.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Microsoft JhengHei UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(11, 110);
            label1.Name = "label1";
            label1.Size = new Size(326, 60);
            label1.TabIndex = 0;
            label1.Text = "uChat";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbConn
            // 
            lbConn.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            lbConn.Location = new Point(11, 170);
            lbConn.Name = "lbConn";
            lbConn.Size = new Size(326, 37);
            lbConn.TabIndex = 1;
            lbConn.Text = "lbConn";
            lbConn.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelBack
            // 
            panelBack.BorderStyle = BorderStyle.FixedSingle;
            panelBack.Controls.Add(btnStartConnect);
            panelBack.Controls.Add(lbInputHint);
            panelBack.Controls.Add(tbIP);
            panelBack.Controls.Add(lbConn);
            panelBack.Controls.Add(label1);
            panelBack.Dock = DockStyle.Fill;
            panelBack.Location = new Point(0, 0);
            panelBack.Name = "panelBack";
            panelBack.Size = new Size(350, 350);
            panelBack.TabIndex = 2;
            // 
            // btnStartConnect
            // 
            btnStartConnect.DefaultBack = Color.RoyalBlue;
            btnStartConnect.ForeColor = Color.White;
            btnStartConnect.IconSvg = "RightOutlined";
            btnStartConnect.Location = new Point(279, 282);
            btnStartConnect.Name = "btnStartConnect";
            btnStartConnect.Size = new Size(58, 55);
            btnStartConnect.TabIndex = 4;
            btnStartConnect.Click += btnStartConnect_Click;
            // 
            // lbInputHint
            // 
            lbInputHint.ForeColor = Color.White;
            lbInputHint.Location = new Point(15, 262);
            lbInputHint.Name = "lbInputHint";
            lbInputHint.Size = new Size(322, 23);
            lbInputHint.TabIndex = 3;
            lbInputHint.Text = "Target Server IP:";
            // 
            // tbIP
            // 
            tbIP.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbIP.Location = new Point(11, 282);
            tbIP.Name = "tbIP";
            tbIP.PlaceholderText = "Please enter your server IP";
            tbIP.Size = new Size(262, 55);
            tbIP.TabIndex = 2;
            tbIP.TextChanged += tbIP_TextChanged;
            // 
            // FormBackground
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(350, 350);
            Controls.Add(panelBack);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "FormBackground";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "uChat Background Services";
            FormClosing += FormBackground_FormClosing;
            Load += FormBackground_Load;
            panelBack.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private Label label1;
        private Label lbConn;
        private Panel panelBack;
        private AntdUI.Input tbIP;
        private AntdUI.Label lbInputHint;
        private AntdUI.Button btnStartConnect;
    }
}