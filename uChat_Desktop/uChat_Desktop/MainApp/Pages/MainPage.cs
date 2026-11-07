using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uChat_Desktop.DataModel;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp.Pages {
    public partial class MainPage : UserControl {
        private readonly AccountDTO? _Account;

        public MainPage() {
            InitializeComponent();
            _Account = AccountService.LoggedAccount;
        }

        private DirectMessagePage dmPage;

        private void MainPage_Load(object sender, EventArgs e) {
            if (_Account == null) {
                MessageBox.Show("Oops, we have no account");
                // return;
            }

            dmPage = new  (_Account) {
                Parent = panelMain,
                Dock = DockStyle.Fill,
            };
        }
    }
}
