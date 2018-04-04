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

        public EccAdapter(IEccEvevt eccEvevt)
        {
            this.eccEvevt = eccEvevt;
        }

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

        void IEccCmd.EccCmd_SendAPIKey(AsyncCallback callback, SendAPIKeyCmdArgs args)
        {
            throw new NotImplementedException();
        }

        void IEccCmd.EccCmd_ControlItem(AsyncCallback callback, ControlItemCmdArgs args)
        {
            throw new NotImplementedException();
        }
    }

    interface IEccEventAdapter
    {

    }





    

}
