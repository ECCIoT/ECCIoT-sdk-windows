using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Listener
{
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
}
