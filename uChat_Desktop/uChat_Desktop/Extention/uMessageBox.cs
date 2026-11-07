using AntdUI;
using uChat_Desktop.MainApp;

namespace uChat_Desktop.Extention {
    public static class uMessageBox {
        public static void Show(Form parent, string content, TType type) {
            Modal.config(parent, "uChat", content, type)
                .SetDraggable(false)
                .SetCancelHide()
                .SetOk("Confirm")
                .open();
        }
    }
}
