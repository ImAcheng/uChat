using AntdUI;
using uChat_Desktop.MainApp.Pages;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp {
    public partial class FormMain : Window {
        public FormMain() {
            InitializeComponent();
        }

        // objects
        public static FormMain instance;

        // pages
        private MainPage? mainPage;

        private void FormMain_Load(object sender, EventArgs e) {
            instance = this;
            mainPage = new MainPage() {
                Parent = panelMain,
                Dock = DockStyle.Fill
            };
        }

        private async void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            await ApiService.CloseAllWebSockets();
            Application.Exit();
        }

        private void FormMain_ResizeBegin(object sender, EventArgs e) {
            panelMain.SuspendLayout();
        }

        private void FormMain_ResizeEnd(object sender, EventArgs e) {
            panelMain.ResumeLayout(true);
            panelMain.PerformLayout();
        }
    }
}
