using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECCIoT_sdk_windows.EccException
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

    /// <summary>
    /// 接收到来自服务器的未知事件消息
    /// </summary>
    public class UnknownEventException : Exception
    {
        public override string ToString()
        {
            return "Receive unknown event messages from the server.";
        }
    }

}
