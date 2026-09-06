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
    public partial class FormMain : Window {
        public FormMain() {
            InitializeComponent();
        }

        // objects
        public static FormMain instance;

        private async void FormMain_Load(object sender, EventArgs e) {
            // if (AccountService.LoggedAccount == null) {
            //     MessageBox.Show("oops! It seems like that there no logged account.\n" +
            //         "This might be a technical issue.\n" +
            //         "Please restart this application.");
            //     DialogResult = DialogResult.Abort;
            //     return;
            // }

            instance = this;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            Application.Exit();
        }
    }
}
