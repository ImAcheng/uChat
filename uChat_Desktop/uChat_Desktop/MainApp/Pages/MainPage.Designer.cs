namespace uChat_Desktop.MainApp.Pages {
    partial class MainPage {
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
            panelServers = new AntdUI.Panel();
            panelMain = new Panel();
            SuspendLayout();
            // 
            // panelServers
            // 
            panelServers.AutoScroll = true;
            panelServers.Back = Color.FromArgb(68, 68, 68);
            panelServers.Dock = DockStyle.Left;
            panelServers.Location = new Point(0, 0);
            panelServers.Name = "panelServers";
            panelServers.Radius = 0;
            panelServers.Size = new Size(50, 515);
            panelServers.TabIndex = 0;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(68, 68, 68);
            panelMain.Dock = DockStyle.Fill;
            panelMain.ForeColor = Color.White;
            panelMain.Location = new Point(50, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(850, 515);
            panelMain.TabIndex = 1;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Controls.Add(panelMain);
            Controls.Add(panelServers);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "MainPage";
            Size = new Size(900, 515);
            Load += MainPage_Load;
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Panel panelServers;
        private Panel panelMain;
    }
}
