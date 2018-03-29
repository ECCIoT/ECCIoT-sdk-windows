using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows
{
    /// <summary>
    /// ECCIoT-SDK事件侦听器接口
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
        void Ecc_Connected();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isSucceed"></param>
        void Ecc_Sent(string msg,Boolean isSucceed);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        void Ecc_OnException(Exception e);
    }
}
