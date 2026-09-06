using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp {
    public partial class FormBackground : Form {
        public FormBackground() {
            InitializeComponent();
        }

        private System.Windows.Forms.Timer ApiChecksTimer = 
            new System.Windows.Forms.Timer() { Interval = 10000 };

        private async void FormBackground_Load(object sender, EventArgs e) {
            if (!await CheckConnection()) {
                MessageBox.Show("Failed to connect to uChat service.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            // I put a delay here bcuz
            // I want the splash screen to be shown correctly.
            Thread.Sleep(2000);
            this.Hide();

            if (!await AccountService.CheckSavedLogin()) {
                using var login = new FormLogin();
                if (login.ShowDialog() != DialogResult.OK) {
                    Application.Exit();
                    return;
                }
            }

            await _StartMainApp();

            ApiChecksTimer.Tick += ApiChecksTimer_Tick;
            ApiChecksTimer.Enabled = true;
        }

        private async void ApiChecksTimer_Tick(object? sender, EventArgs e) {
            if (!await CheckConnection()) {

            }
        }

        public static async Task<bool> CheckConnection() {
            using HttpClient client = new HttpClient();

            try {
                await client.GetAsync(ApiService.ApiUri);
            }
            catch (HttpRequestException) {
                return false;
            }

            return true;
        }

        private async Task _StartMainApp() {
            var formMain = new FormMain();
            await formMain.ShowAsync();
        }
    }
}
