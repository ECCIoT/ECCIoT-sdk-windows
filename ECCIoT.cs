using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ECCIoT_sdk_windows
{
    /// <summary>
    /// ECCIoT SDK
    /// </summary>
    public class ECCIoT : IEccReceiptListener,IEccDataReceiveListener, IEccExceptionListener
    {
        //======================================================================================
        // 定义一个静态变量来保存类的实例
        private static ECCIoT uniqueInstance;
        // 定义一个标识确保线程同步
        private static readonly object locker = new object();
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        private static ECCIoT GetInstance()
        {
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new ECCIoT();
                    }
                }
            }
            return uniqueInstance;
        }
        //======================================================================================

        /*ECC通信*/
        //服务端地址
        private static IPAddress ecc_server_address = IPAddress.Parse(ECC_sdk_windows.Properties.Resources.ECCIoT_Server_Address);
        //服务端端口
        private static int ecc_server_port = int.Parse(ECC_sdk_windows.Properties.Resources.ECCIoT_Server_Port);
        //通信工具类
        private EccSocket eccSocket;

        /*回调接口*/
        public static IEccDataReceiveListener EccDataReceiveListener { private get; set; }
        public static IEccExceptionListener EccExceptionListener { private get; set; }

        /*字符编码*/
        private static Encoding encoding = Encoding.UTF8;
        //字符编码
        public static Encoding Encoding
        {
            set { encoding = value; if (GetInstance().eccSocket != null) GetInstance().eccSocket.Encoding=value; }
            get { return encoding; }
        }

        /*必要参数*/
        public string API_Key { get; set; }
        private string Token { get; set; }

        private ECCIoT(){}

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="listener"></param>
        public static void Connect(IEccReceiptListener receiptListener)
        {
            //初始化EccSocket
            if (GetInstance().eccSocket != null) return;

            //实例化EccSocket对象
            GetInstance().eccSocket = new EccSocket(GetInstance(), GetInstance(), GetInstance());
            //连接服务器
            IPEndPoint ipep = new IPEndPoint(ecc_server_address, ecc_server_port);
            GetInstance().eccSocket.Encoding = Encoding;
            GetInstance().eccSocket.Connect(receiptListener, ipep);
        }

        /// <summary>
        /// 重新连接服务器
        /// </summary>
        /// <param name="listener"></param>
        public static void Reconnect(IEccReceiptListener listener)
        {
            //关闭通信
            Close(listener);
            //建立连接
            Connect(listener);
        }

        /// <summary>
        /// 向服务器发送消息
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="message"></param>
        public static void Send(IEccReceiptListener listener,string message)
        {
            if (GetInstance().eccSocket==null)
            {
                listener.Ecc_Sent(listener, message, false);
                //抛出异常
                return;
            }else if (!GetInstance().eccSocket.Socket.Connected)
            {
                listener.Ecc_Sent(listener, message, false);
                //由于采用异步连接，可能会处于连接中的状态。应抛出未连接的异常
                return;
            }
            else
            {
                GetInstance().eccSocket.Send(listener, message);
            }
        }

        /// <summary>
        /// 关闭与服务器的通信
        /// </summary>
        /// <param name="listener"></param>
        public static void Close(IEccReceiptListener listener)
        {
            if (GetInstance().eccSocket != null)
            {
                if (GetInstance().eccSocket.Socket.Connected)
                {
                    //关闭并销毁Socket通信对象
                    GetInstance().eccSocket.Dispose(listener);
                }
                //EccSocket对象置空
                GetInstance().eccSocket = null;
            }
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
            if (EccDataReceiveListener != null)
            {
                EccDataReceiveListener.Ecc_Received(msg,len);
            }
        }

        /*异常错误回调接口*/
        void IEccExceptionListener.Ecc_BreakOff(Exception e)
        {
            if (EccExceptionListener != null)
            {
                EccExceptionListener.Ecc_BreakOff(e);
            }
        }
    }
}
