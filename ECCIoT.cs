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
    /// 
    /// </summary>
    public class ECCIoT : IEccListener
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
        private IPAddress ecc_server_address = IPAddress.Parse(Properties.Resources.ECCIoT_Server_Address);
        //服务端端口
        private int ecc_server_port = int.Parse(Properties.Resources.ECCIoT_Server_Port);
        //通信工具类
        private EccSocket eccSocket;

        /*监听器集合*/
        private List<IEccListener> lstListener = new List<IEccListener>();


        private ECCIoT(){
            IPEndPoint ipep = new IPEndPoint(ecc_server_address, ecc_server_port);
            eccSocket = new EccSocket(ipep, this);
        }

        public static void Connect(IEccListener listener)
        {
            GetInstance().eccSocket.Connect(listener);
        }

        public static void Send(IEccListener listener,string message)
        {
            /* 不必这么写
            if (GetInstance().eccSocket.Socket==null || !GetInstance().eccSocket.Socket.Connected)
            {
                //抛出异常
                return;
            }
            */
            GetInstance().eccSocket.Send(listener, message);
        }


        public void Ecc_Connected(IEccListener listener)
        {
            try
            {
                listener.Ecc_Connected(listener);
            }
            catch(NullReferenceException ex)
            {
                foreach (IEccListener el in lstListener)    //使用迭代器可能会导致侦听接口从List中移除时发生异常，应使用for循环控制
                {
                    el.Ecc_Connected(null);
                }
            }
        }

        public void Ecc_Sent(IEccListener listener, string msg, bool isSucceed)
        {
            try
            {
                listener.Ecc_Sent(listener,msg,isSucceed);
            }
            catch (NullReferenceException ex)
            {
                foreach (IEccListener el in lstListener)
                {
                    el.Ecc_Sent(null, msg, isSucceed);
                }
            }
        }

        public void Ecc_OnException(IEccListener listener, Exception e)
        {
        }

        public void Ecc_Receive(string msg, int len)
        {
            //2018年3月30日23:47:54
            //我觉得接收的回调函数应该有一个单独的侦听接口
        }

        public void Ecc_OnReceiveException(SocketException ex)
        {
            
        }
    }
}
