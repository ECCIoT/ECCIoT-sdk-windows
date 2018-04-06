using ECC_sdk_windows.Adapter;
using ECC_sdk_windows.Comm.Listener;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ECCIoT_sdk_windows.Comm
{
    /// <summary>
    /// 异步通信的Socket客户端工具类
    /// 参考资料：https://blog.csdn.net/mss359681091/article/details/51790931
    /// </summary>
    public class EccSocket : IDisposable
    {
        //Socket
        public Socket Socket { get; private set; }
        private int maxCacheSize = 2048;
        //回调接口
        private IEccReceiptListener EccReceiptListener { get; set; }
        private IEccDataReceiveListener EccDataReceiveListener { get; set; }
        private IEccExceptionListener EccExceptionListener { get; set; }
        //字符编码
        private Encoding encoding = Encoding.UTF8;
        public Encoding Encoding { set { encoding = value; } }

        /// <summary>
        /// 
        /// </summary>
        
        public EccSocket(IEccReceiptListener receiptListener, IEccDataReceiveListener dataReceiveListener, IEccExceptionListener exceptionListener)
        {
            EccReceiptListener = receiptListener;
            EccDataReceiveListener = dataReceiveListener;
            EccExceptionListener = exceptionListener;
        }

        public EccSocket(EccAdapter adapter)
        {
            EccReceiptListener = adapter;
            EccDataReceiveListener = adapter;
            EccExceptionListener = adapter;
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="ipep">网络终结点</param>
        public void Connect(IEccReceiptListener listener, IPEndPoint ipep)
        {
            //创建套接字  
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //开始对一个远程主机建立异步的连接请求
                Socket.BeginConnect(ipep, asyncResult =>
                {
                    //结束挂起的异步连接请求
                    Socket.EndConnect(asyncResult);
                    //连接完成回调
                    if(listener!=null) EccReceiptListener.Ecc_Connection(listener, true);
                    //接受消息  
                    Recive();
                }, null);
            }
            catch (SocketException ex)
            {
                //与服务器连接失败
                if (listener != null) EccReceiptListener.Ecc_Connection(listener, false);
                //异常回调
                EccExceptionListener.Ecc_BreakOff(ex);
            }

        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message">消息字符串</param>
        public void Send(IEccReceiptListener listener, string message)
        {
            if (Socket == null || message == string.Empty) return;
            //编码  
            byte[] data = encoding.GetBytes(message);
            try
            {
                //异步发送数据
                Socket.BeginSend(data, 0, data.Length, SocketFlags.None, asyncResult =>
                {
                    //完成发送消息  
                    int length = Socket.EndSend(asyncResult);
                    //消息发送成功
                    if (listener != null) EccReceiptListener.Ecc_Sent(listener, message, true);
                }, null);
            }
            catch (SocketException ex)
            {
                //消息发送失败
                if (listener != null) EccReceiptListener.Ecc_Sent(listener, message, false);
                //异常回调
                EccExceptionListener.Ecc_BreakOff(ex);
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
                Socket.BeginReceive(data, 0, data.Length, SocketFlags.None,
                asyncResult =>
                {
                    try
                    {
                        int length = Socket.EndReceive(asyncResult);
                        //消息接收回调
                        if(length>0) EccDataReceiveListener.Ecc_Received(encoding.GetString(data), length);
                        //重启异步接收数据
                        Recive();
                    }
                    catch (ObjectDisposedException ex)
                    {
                        //Console.WriteLine(ex.Message);
                    }
                }, null);
            }
            catch (SocketException ex)
            {
                //异常回调
                EccExceptionListener.Ecc_BreakOff(ex);
            }
            
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            if(Socket.Connected) Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
            Socket.Dispose();
        }

        /// <summary>
        /// 销毁对象并执行回调
        /// </summary>
        /// <param name="listener"></param>
        public void Dispose(IEccReceiptListener listener)
        {
            Socket.BeginDisconnect(true, asyncResult =>
            {
                Dispose();
                if(listener!=null) EccReceiptListener.Ecc_Closed(listener);
            }, null);
        }
    }
}