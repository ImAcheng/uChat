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
using uChat_Desktop.Extention;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp.Grid {
    public partial class FriendAndSearch : UserControl {
        private readonly AccountDTO _CurAccount;

        public FriendAndSearch() {
            InitializeComponent();
            if (AccountService.LoggedAccount == null) {
                uMessageBox.Show(FormMain.instance, 
                    "Oops! It seems like that you haven't logged in.\nPlease login to use uChat.", 
                    AntdUI.TType.Error);
                FormMain.instance.Close();
                return;
            }
        }

        private FriendDisplay frdDisplay;

        private void FriendAndSearch_Load(object sender, EventArgs e) {
            frdDisplay = new FriendDisplay(_CurAccount.AccountId);
        }
    }
}
