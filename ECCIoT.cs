using ECC_sdk_windows.Adapter;
using ECC_sdk_windows.Comm.Listener;
using ECCIoT_sdk_windows.Comm;
using ECCIoT_sdk_windows.EccException;
using System;
using System.Net;
using System.Text;

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
        private static ECCIoT GetInstance()
        {
            if (uniqueInstance == null)
                lock (locker)
                    if (uniqueInstance == null)
                        uniqueInstance = new ECCIoT();
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
        private EccAdapter eccAdapter;
        public static EccAdapter Adapter { get { return GetInstance().eccAdapter; } }

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

        //建立连接
        public static void Connect(EccAdapter adapter, IEccReceiptListener receiptListener)
        {
            //初始化EccSocket
            if (GetInstance().eccSocket != null && GetInstance().eccSocket.Socket.Connected) return;

            //保存Ecc适配器对象，并为其设置ECCIoT实例
            GetInstance().eccAdapter = adapter;
            adapter.EcciotInstance = GetInstance();

            //实例化Ecc通信对象
            GetInstance().eccSocket = new EccSocket(GetInstance().eccAdapter)
            {
                //设置字符编码
                Encoding = Encoding
            };

            //连接服务器
            GetInstance().eccSocket.Connect(ipep, receiptListener);
        }

        public static void Connect(EccAdapter adapter, AsyncCallback successful, AsyncCallback failure)
        {
            //初始化EccSocket
            if (GetInstance().eccSocket != null && GetInstance().eccSocket.Socket.Connected) return;

            //保存Ecc适配器对象，并为其设置ECCIoT实例
            GetInstance().eccAdapter = adapter;
            adapter.EcciotInstance = GetInstance();

            //实例化Ecc通信对象
            GetInstance().eccSocket = new EccSocket(GetInstance().eccAdapter)
            {
                //设置字符编码
                Encoding = Encoding
            };

            //连接服务器
            GetInstance().eccSocket.Connect(ipep,successful,failure);
        }

        //发送数据
        public void Send(string message, IEccReceiptListener listener)
        {
            if (GetInstance().eccSocket == null)
            {
                //未连接服务器
                listener.Ecc_Sent(listener, message, false);
                throw new UnconnectedServerException();
            }
            else if (!GetInstance().eccSocket.Socket.Connected)
            {
                //未完成与服务器的连接
                listener.Ecc_Sent(listener, message, false);
                throw new UnconnectedCompletionException();
            }
            else
            {
                GetInstance().eccSocket.Send(message,listener);
            }
        }

        public void Send(string message, AsyncCallback successful, AsyncCallback failure)
        {
            //检查连接状态
            if(GetInstance().eccSocket == null || !GetInstance().eccSocket.Socket.Connected)
            {
                ((Action)VoidAction).BeginInvoke(failure,null);

                if (GetInstance().eccSocket == null)
                {
                    throw new UnconnectedServerException();
                }
                else
                {
                    throw new UnconnectedCompletionException();
                }
            }
            //发送命令
            GetInstance().eccSocket.Send(message, successful, failure);
        }

        //关闭连接
        public delegate void CloseDelegate(IEccReceiptListener listener);
        public static void Close(IEccReceiptListener listener)
        {
            if (GetInstance().eccSocket != null)
            {
                if (GetInstance().eccSocket.Socket.Connected)
                {
                    //关闭并销毁Socket通信对象
                    GetInstance().eccSocket.Close(listener);
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

        private void VoidAction()
        {

        }
    }
}
