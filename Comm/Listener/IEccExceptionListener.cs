using System;
using System.Net.Sockets;

namespace ECC_sdk_windows.Comm.Listener
{
    /// <summary>
    /// 异常错误回调接口
    /// </summary>
    public interface IEccExceptionListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        void Ecc_BreakOff(Exception ex);

        void Ecc_ConnectionFail(SocketException ex);
    }
}
