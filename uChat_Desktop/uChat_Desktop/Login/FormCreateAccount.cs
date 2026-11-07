using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uChat_Desktop.Lang;
using uChat_Desktop.Login.Creates;
using uChat_Desktop.Service;

namespace uChat_Desktop.Login {
    public partial class FormCreateAccount : AntdUI.Window {
        public FormCreateAccount() {
            InitializeComponent();
        }

        public static FormCreateAccount Instance;
        public CreateAccountRequest createAccRq = new CreateAccountRequest();

        public string _Username, _Password;

        private int _PageCounter = 0;
        private static List<UserControl> _Pages;

        private void FormCreateAccount_Load(object sender, EventArgs e) {
            Instance = this;

            _Pages = new List<UserControl>() {
                new AccInfoUC() { Parent =  panelMain, Dock = DockStyle.Fill },
                new MoreInfoUC() { Parent =  panelMain, Dock = DockStyle.Fill },
            };

            SetPage(0);
        }

        public void SetPage(int page) {
            _PageCounter = Math.Clamp(page, 0, _Pages.Count - 1);
            _Pages[_PageCounter].BringToFront();
        }

        public void NextPage() {
            _PageCounter = Math.Clamp(_PageCounter + 1, 0, _Pages.Count - 1);
            _Pages[_PageCounter].BringToFront();
        }

        public void PreviousPage() {
            _PageCounter = Math.Clamp(_PageCounter - 1, 0, _Pages.Count - 1);
            _Pages[_PageCounter].BringToFront();
        }
    }
}
