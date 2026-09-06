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
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Microsoft JhengHei UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label1.Location = new Point(12, 117);
            label1.Name = "label1";
            label1.Size = new Size(326, 60);
            label1.TabIndex = 0;
            label1.Text = "uChat";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label2.Location = new Point(12, 177);
            label2.Name = "label2";
            label2.Size = new Size(326, 37);
            label2.TabIndex = 1;
            label2.Text = "Connecting to Service...";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormBackground
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(350, 350);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "FormBackground";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "uChat Background Services";
            Load += this.FormBackground_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
    }
}