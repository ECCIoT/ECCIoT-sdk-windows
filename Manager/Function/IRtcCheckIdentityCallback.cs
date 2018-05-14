using ECC_sdk_windows.Manager.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Manager.Function
{
    interface IRtcCheckIdentityCallback
    {
        void Terminal_CheckTerminalIdentity(CheckTerminalIdentityArgs args, AsyncCallback successful, AsyncCallback failure);
    }
}
