using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECCIoT_sdk_windows
{
    /// <summary>
    /// 尚未对ECCIoT设置APIKey
    /// </summary>
    public class NotSetAPIKeyException : Exception
    {
        public override string ToString()
        {
            return "No APIKey has been set for ECCIoT.";
        }
    }

    /// <summary>
    /// 尚未完成与服务器之间的连接
    /// </summary>
    public class UnconnectedCompletionException : Exception
    {
        public override string ToString()
        {
            return "The connection to the server has not yet been completed.";
        }
    }

    /// <summary>
    /// 未连接服务器
    /// </summary>
    public class UnconnectedServerException : Exception
    {
        public override string ToString()
        {
            return "No connection to the server.";
        }
    }



}
