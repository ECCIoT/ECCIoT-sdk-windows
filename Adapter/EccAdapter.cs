using ECC_sdk_windows.Adapter;
using ECC_sdk_windows.EccArgs;
using ECC_sdk_windows.Listener;
using ECCIoT_sdk_windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows
{
    public delegate void x(string msg);
    public delegate void y(string msg);

    public class EccAdapter : IEccReceiptListener, IEccDataReceiveListener, IEccExceptionListener, IEccCmd
    {
        /*EccEventAdapter回调接口*/
        private IEccEvevt eccEvevt;

        //ECCIoT示例
        public ECCIoT EcciotInstance { private get; set; }

        public EccAdapter(IEccEvevt eccEvevt)
        {
            this.eccEvevt = eccEvevt;
        }

        /*
        public EccAdapter(IEccEvevt eccEvevt, ECCIoT ecciotInstance)
        {
            this.eccEvevt = eccEvevt;
            this.EcciotInstance = ecciotInstance;
        }
        */

        /*操作回执回调接口*/
        void IEccReceiptListener.Ecc_Connection(IEccReceiptListener listener, bool isSucceed)
        {
            listener.Ecc_Connection(listener, isSucceed);
        }
        void IEccReceiptListener.Ecc_Sent(IEccReceiptListener listener, string msg, bool isSucceed)
        {
            listener.Ecc_Sent(listener, msg, isSucceed);
        }
        void IEccReceiptListener.Ecc_Closed(IEccReceiptListener listener)
        {
            listener.Ecc_Closed(listener);
        }

        /*数据接收回调接口*/
        void IEccDataReceiveListener.Ecc_Received(string msg, int len)
        {
            
        }

        /*异常错误回调接口*/
        void IEccExceptionListener.Ecc_BreakOff(Exception e)
        {
            
        }

        //发送APIKey
        void IEccCmd.EccCmd_CheckAPIKey(AsyncCallback callback, SendAPIKeyCmdArgs args)
        {
            CmdJson cmd = new CmdJson
            {
                Action = "EccCmd_CheckAPIKey",
                Content = args.ToString()
            };
            EcciotInstance.Send(callback, cmd.ToString());
        }

        //发送控制命令
        void IEccCmd.EccCmd_ControlItem(AsyncCallback callback, ControlItemCmdArgs args)
        {
            CmdJson cmd = new CmdJson
            {
                Action = "EccCmd_ControlItem",
                Content = args.ToString()
            };
            EcciotInstance.Send(callback, cmd.ToString());
        }
    }

    interface IEccEventAdapter
    {

    }





    

}
