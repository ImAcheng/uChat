namespace uChat_Desktop.Login {
    partial class FormCreateAccount {
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
            panelMain = new Panel();
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
            pageHeader1.TabIndex = 9;
            pageHeader1.Text = " ";
            // 
            // panelMain
            // 
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 35);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(650, 445);
            panelMain.TabIndex = 10;
            // 
            // FormCreateAccount
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(650, 480);
            Controls.Add(panelMain);
            Controls.Add(pageHeader1);
            DisableTheme = true;
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            Name = "FormCreateAccount";
            Resizable = false;
            Text = "FormCreateAccount";
            Load += FormCreateAccount_Load;
            ResumeLayout(false);
        }

        #endregion

        private LinkLabel llb_go_create_acc;
        private AntdUI.PageHeader pageHeader1;
        private Panel panelMain;
    }
}