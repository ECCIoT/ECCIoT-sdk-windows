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

    public class EccEventAdapter : IEccReceiptListener, IEccDataReceiveListener, IEccExceptionListener, IEccCommand
    {
        /*EccEventAdapter回调接口*/
        private IEccEvevt eccEvevt;

        public EccEventAdapter(IEccEvevt eccEvevt)
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
    }

    public interface IEccEvevt
    {
        //接收服务端询问API_KEY

        //接收更新设备项的数据集

        //接收紧急消息

        //
    }

    public interface IEccCommand
    {
        //发送API_KEY

        //发送单个设备的控制命令


    }
}
