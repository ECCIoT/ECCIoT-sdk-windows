using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows
{
    /// <summary>
    /// https://blog.csdn.net/mss359681091/article/details/51790931
    /// </summary>
    public class EccSocket
    {
        //Socket
        private Socket socket;   
        private IPEndPoint ipep;
        private int maxCacheSize = 2048;
        //回调接口
        private IEccListener eccListener;
        //字符编码
        private Encoding encoding = Encoding.UTF8;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipep">网络终结点</param>
        /// <param name="eccListener"></param>
        /// <param name="encoding"></param>
        public EccSocket(IPEndPoint ipep,IEccListener eccListener,Encoding encoding)
        {
            this.ipep = ipep;
            this.eccListener = eccListener;
            this.encoding = encoding;
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        public void Connect()
        {
            //端口及IP  
            //IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6065);
            //创建套接字  
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //开始对一个远程主机建立异步的连接请求
            client.BeginConnect(ipep, asyncResult =>
            {
                //结束挂起的异步连接请求
                client.EndConnect(asyncResult);
                //连接完成回调
                eccListener.Ecc_Connected();
                //接受消息  
                Recive();
            }, null);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message">消息字符串</param>
        public void Send(string message)
        {
            if (socket == null || message == string.Empty) return;
            //编码  
            byte[] data = encoding.GetBytes(message);
            try
            {
                //异步发送数据
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, asyncResult =>
                {
                    //完成发送消息  
                    int length = socket.EndSend(asyncResult);
                    //消息发送成功
                    eccListener.Ecc_Sent(message, true);
                }, null);
            }
            catch (SocketException ex)
            {
                //消息发送失败
                eccListener.Ecc_Sent(message, false);
                //异常回调
                eccListener.Ecc_OnException(ex);
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        private void Recive()
        {
            //缓存区
            byte[] data = new byte[maxCacheSize];
            try
            {
                //开始接收数据  
                socket.BeginReceive(data, 0, data.Length, SocketFlags.None,
                asyncResult =>
                {
                    int length = socket.EndReceive(asyncResult);
                    //消息接收回调
                    eccListener.Ecc_Receive(encoding.GetString(data), length);
                    //重启异步接收数据
                    Recive();
                }, null);
            }
            catch (SocketException ex)
            {
                eccListener.Ecc_OnException(ex);
            }
        }
    }
}
