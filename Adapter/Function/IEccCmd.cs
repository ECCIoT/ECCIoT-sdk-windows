using ECC_sdk_windows.Adapter.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Adapter.Function
{
    public interface IEccCmd
    {
        /*
        //IEccCmd接口实现时使用异步执行的示例
        private delegate void Example(ExampleCmdArgs args);
        private void EccCmd_Example(ExampleCmdArgs args)
        {
           //do somethings... 
        }
        void IEccCmd.EccCmd_Example(AsyncCallback callback, ExampleCmdArgs args)
        {
            ((Example)EccCmd_Example).BeginInvoke(args, callback, null);
        }
        */

        //发送API_KEY
        void EccCmd_CheckAPIKey(AsyncCallback callback, SendAPIKeyCmdArgs args);
        //发送单个设备的控制命令
        void EccCmd_ControlItem(AsyncCallback callback, ControlItemCmdArgs args);
    }
}
