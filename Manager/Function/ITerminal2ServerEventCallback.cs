using ECC_sdk_windows.Manager.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Manager.Function
{
    interface ITerminal2ServerEventCallback
    {
        void Terminal_ControlDevice(ControlDeviceArgs args, AsyncCallback successful, AsyncCallback failure);
        void Terminal_BindDevice(BindDeviceArgs args, AsyncCallback successful, AsyncCallback failure);
    }
}
