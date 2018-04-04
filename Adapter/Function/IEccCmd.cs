using ECC_sdk_windows.EccArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Adapter
{
    public interface IEccCmd
    {
        //发送API_KEY
        void EccCmd_SendAPIKey(AsyncCallback callback, SendAPIKeyCmdArgs args);
        //发送单个设备的控制命令
        void EccCmd_ControlItem(AsyncCallback callback, ControlItemCmdArgs args);
    }
}
