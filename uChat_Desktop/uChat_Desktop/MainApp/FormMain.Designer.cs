namespace uChat_Desktop.MainApp {
    partial class FormMain {
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
            pageHeader1 = new AntdUI.PageHeader();
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
            pageHeader1.Size = new Size(900, 35);
            pageHeader1.TabIndex = 1;
            pageHeader1.Text = " ";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(900, 550);
            Controls.Add(pageHeader1);
            Dark = true;
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Mode = AntdUI.TAMode.Dark;
            Name = "FormMain";
            Text = "uChat";
            FormClosing += FormMain_FormClosing;
            Load += FormMain_Load;
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
    }
}