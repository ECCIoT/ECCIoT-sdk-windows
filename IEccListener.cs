using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows
{
    /// <summary>
    /// ECCIoT-SDK事件侦听器接口
    /// todo: 接口方法中的参数应该为事件对象，即：为其设计事件类
    /// </summary>
    public interface IEccListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void Ecc_Receive(string msg, int len);

        /// <summary>
        /// 
        /// </summary>
        void Ecc_Connected(IEccListener sender);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isSucceed"></param>
        void Ecc_Sent(IEccListener sender, string msg,Boolean isSucceed);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        void Ecc_OnException(IEccListener sender, Exception e);
        void Ecc_OnReceiveException(SocketException ex);
    }
}
