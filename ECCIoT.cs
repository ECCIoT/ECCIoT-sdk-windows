using ECC_sdk_windows;
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
    public class ECCIoT
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
        //服务端终端节点
        private static IPEndPoint ipep = new IPEndPoint(ecc_server_address, ecc_server_port);
        //通信工具类
        private EccSocket eccSocket;

        /*Ecc事件适配器*/
        private EccEventAdapter eventAdapter;

        /*必要参数*/
        public string API_Key { get; set; }

        /*字符编码*/
        private static Encoding encoding = Encoding.UTF8;
        public static Encoding Encoding
        {
            set { encoding = value; if (GetInstance().eccSocket != null) GetInstance().eccSocket.Encoding=value; }
            get { return encoding; }
        }

        private ECCIoT()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiptListener"></param>
        /// <param name="eccEvevt"></param>
        public delegate void ConnectDelegate(IEccReceiptListener receiptListener, EccEventAdapter adapter);
        public static void Connect(IEccReceiptListener receiptListener, EccEventAdapter adapter)
        {
            //初始化EccSocket
            if (GetInstance().eccSocket != null) return;

            //实例化EccEventAdapter对象
            GetInstance().eventAdapter = adapter;

            //实例化EccSocket对象
            GetInstance().eccSocket = new EccSocket(GetInstance().eventAdapter);

            //连接服务器
            GetInstance().eccSocket.Encoding = Encoding;
            GetInstance().eccSocket.Connect(receiptListener, ipep);
        }
        public static void Connect(AsyncCallback callback, EccEventAdapter adapter)
        {
            ConnectDelegate connectFn = Connect;
            connectFn.BeginInvoke(null, adapter, callback, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="message"></param>
        public delegate void SendDelegate(IEccReceiptListener listener, string message);
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
        public static void Send(AsyncCallback callback, string message)
        {
            SendDelegate sendFn = Send;
            sendFn.BeginInvoke(null, message, callback, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listener"></param>
        public delegate void CloseDelegate(IEccReceiptListener listener);
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
        public static void Close(AsyncCallback callback)
        {
            CloseDelegate closeFn = Close;
            closeFn.BeginInvoke(null, callback, null);
        }
    }
}
