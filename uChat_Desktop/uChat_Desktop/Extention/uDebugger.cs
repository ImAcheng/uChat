using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uChat_Desktop.Extention {
    public static class uDebugger {
        public static void Print(string ctx) {
            Debug.WriteLine("[Debug] " + ctx);
        }
    }
}
