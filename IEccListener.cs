using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows
{
    /// <summary>
    /// 操作回执回调接口
    /// </summary>
    public interface IEccReceiptListener
    {
        /// <summary>
        /// 连接建立回调方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="isSucceed"></param>
        void Ecc_Connection(IEccReceiptListener listener, Boolean isSucceed);

        /// <summary>
        /// 消息发送回调方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="isSucceed"></param>
        void Ecc_Sent(IEccReceiptListener listener, string msg,Boolean isSucceed);
    }

    /// <summary>
    /// 数据接收回调接口
    /// </summary>
    public interface IEccDataReceiveListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void Ecc_Received(string msg, int len);
    }

    /// <summary>
    /// 异常错误回调接口
    /// </summary>
    public interface IEccExceptionListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        void Ecc_BreakOff(Exception e);
    }

}
