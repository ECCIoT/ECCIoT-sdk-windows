using ECC_sdk_windows.Manager.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Manager.Function
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
        void IEccCmd.EccCmd_Example(ExampleCmdArgs args, AsyncCallback callback)
        {
            ((Example)EccCmd_Example).BeginInvoke(args, callback, null);
        }
        */

        /// <summary>
        /// 发送API_KEY
        /// </summary>
        /// <param name="args"></param>
        /// <param name="successful"></param>
        /// <param name="failure"></param>
        void EccCmd_CheckAPIKey(CheckAPIKeyCmdArgs args, AsyncCallback successful, AsyncCallback failure);
        /// <summary>
        /// 发送单个设备的控制命令
        /// </summary>
        /// <param name="args"></param>
        /// <param name="successful"></param>
        /// <param name="failure"></param>
        void EccCmd_ControlItem(ControlItemCmdArgs args, AsyncCallback successful, AsyncCallback failure);
    }
}
