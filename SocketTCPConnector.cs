using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECC_sdk_windows
{
    public class SocketTCPConnector
    {
        public const int STATUS_CODE_CLOSED = 0;
        public const int STATUS_CODE_UNKNOWNHOST = 1;
        public const int STATUS_CODE_EXCEPTION = 2;
        public const int STATUS_CODE_CONNECTED = 3;
        public const int STATUS_CODE_INTERRUPTION = 4;
        public const int STATUS_CODE_UNCONNECTION = 5;

        public string ip { private get; set; }
        public int port { private get; set; }
        public OnSocketReceiveListener listener { set; private get; }
        public Socket socket { get; private set; }
        private Boolean isRun;

        public SocketTCPConnector(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public SocketTCPConnector(string ip, int port, OnSocketReceiveListener listener)
        {
            this.ip = ip;
            this.port = port;
            this.listener = listener;
        }

        public void Close()
        {
            listener = null;
            socket.Close();
        }

        public void Start()
        {
            if (!isRun)
            {
                isRun = true;
                IPAddress address = IPAddress.Parse(ip);
                IPEndPoint endpoint = new IPEndPoint(address, port);
                //创建服务端负责监听的套接字，参数（使用IPV4协议，使用流式连接，使用TCO协议传输数据）
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    socket.Connect(endpoint);
                }
                catch (SocketException e)
                {
                    if (listener != null) listener.onStateChanged(STATUS_CODE_UNCONNECTION);
                    return;
                }
                if (socket.Connected)
                {
                    //连接成功
                    if (listener != null) listener.onStateChanged(STATUS_CODE_CONNECTED);
                }
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveData);
                Thread thradReceiveData = new Thread(pts);
                thradReceiveData.IsBackground = true;
                thradReceiveData.Start(socket);
            }
        }

        private void ReceiveData(object socketClientPara)
        {
            Socket socketClient = socketClientPara as Socket;
            while (isRun)
            {
                //定义一个接受用的缓存区（100M字节数组）
                //byte[] arrMsgRec = new byte[1024 * 1024 * 100];
                //将接收到的数据存入arrMsgRec数组,并返回真正接受到的数据的长度   
                if (socketClient.Connected)
                {
                    try
                    {
                        byte[] byteMessage = new byte[1024];
                        socketClient.Receive(byteMessage);
                        string recvMsg = Encoding.UTF8.GetString(byteMessage).Replace("\0", "");
                        if (listener != null) listener.onReceiveData(recvMsg);
                    }
                    catch (Exception e)
                    {
                        if (listener != null) listener.onStateChanged(STATUS_CODE_EXCEPTION);
                        break;
                    }
                }
                else
                {
                    if (listener != null) listener.onStateChanged(STATUS_CODE_INTERRUPTION);
                }
            }

        }

        /// <summary>
        /// 判断对象是否建立连接
        /// </summary>
        /// <returns>是否已连接</returns>
        public Boolean Connected()
        {
            return socket.Connected;
        }

        public void SendString(string str)
        {
            SendData(System.Text.Encoding.UTF8.GetBytes(str + "\n"));
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        public void SendData(byte[] buffer)
        {
            socket.Send(buffer);
        }

        /// <summary>
        /// 向远程主机发送文件
        /// </summary>
        /// <param name="socket" >要发送数据且已经连接到远程主机的 socket</param>
        /// <param name="fileName">待发送的文件名称</param>
        /// <param name="maxBufferLength">文件发送时的缓冲区大小</param>
        /// <param name="outTime">发送缓冲区中的数据的超时时间</param>
        /// <returns>0:发送文件成功；-1:超时；-2:发送文件出现错误；-3:发送文件出现异常；-4:读取待发送文件发生错误</returns>
        /// <remarks >
        /// 当 outTime 指定为-1时，将一直等待直到有数据需要发送
        /// </remarks>
        public int SendFile(string fileName, int maxBufferLength, int outTime)
        {
            if (fileName == null || maxBufferLength <= 0)
            {
                throw new ArgumentException("待发送的文件名称为空或发送缓冲区的大小设置不正确.");
            }
            int flag = 0;
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                long fileLen = fs.Length;                        // 文件长度
                long leftLen = fileLen;                            // 未读取部分
                int readLen = 0;                                // 已读取部分
                byte[] buffer = null;

                if (fileLen <= maxBufferLength)
                {            /* 文件可以一次读取*/
                    buffer = new byte[fileLen];
                    readLen = fs.Read(buffer, 0, (int)fileLen);
                    flag = SendData(buffer, outTime);
                }
                else
                {
                    /* 循环读取文件,并发送 */

                    while (leftLen != 0)
                    {
                        if (leftLen < maxBufferLength)
                        {
                            buffer = new byte[leftLen];
                            readLen = fs.Read(buffer, 0, Convert.ToInt32(leftLen));
                        }
                        else
                        {
                            buffer = new byte[maxBufferLength];
                            readLen = fs.Read(buffer, 0, maxBufferLength);
                        }
                        if ((flag = SendData(buffer, outTime)) < 0)
                        {
                            break;
                        }
                        leftLen -= readLen;
                    }
                }
                fs.Flush();
                fs.Close();
            }
            catch (IOException e)
            {
                flag = -4;
            }
            return flag;
        }

        /// <summary>
        /// 向远程主机发送数据
        /// </summary>
        /// <param name="socket">要发送数据且已经连接到远程主机的 Socket</param>
        /// <param name="buffer">待发送的数据</param>
        /// <param name="outTime">发送数据的超时时间，以秒为单位，可以精确到微秒</param>
        /// <returns>0:发送数据成功；-1:超时；-2:发送数据出现错误；-3:发送数据时出现异常</returns>
        /// <remarks >
        /// 当 outTime 指定为-1时，将一直等待直到有数据需要发送
        /// </remarks>
        public int SendData(byte[] buffer, int outTime)
        {
            if (socket == null || socket.Connected == false)
            {
                throw new ArgumentException("参数socket 为null，或者未连接到远程计算机");
            }
            if (buffer == null || buffer.Length == 0)
            {
                throw new ArgumentException("参数buffer 为null ,或者长度为 0");
            }

            int flag = 0;
            try
            {
                int left = buffer.Length;
                int sndLen = 0;

                while (true)
                {
                    if ((socket.Poll(outTime * 100, SelectMode.SelectWrite) == true))
                    {        // 收集了足够多的传出数据后开始发送
                        sndLen = socket.Send(buffer, sndLen, left, SocketFlags.None);
                        left -= sndLen;
                        if (left == 0)
                        {
                            flag = 0;               // 数据已经全部发送
                            break;
                        }
                        else
                        {
                            if (sndLen > 0)
                            {
                                continue;           // 数据部分已经被发送
                            }
                            else
                            {
                                flag = -2;          // 发送数据发生错误
                                break;
                            }
                        }
                    }
                    else
                    {
                        flag = -1;                  // 超时退出
                        break;
                    }
                }
            }
            catch (SocketException e)
            {
                flag = -3;
            }
            return flag;
        }

        public interface OnSocketReceiveListener
        {
            /**
             * 接收到数据
             * @param str
             */
            void onReceiveData(String str);

            /**
             * 当状态发生变化
             * @param STATUS_CODE
             */
            void onStateChanged(int STATUS_CODE);
        }
    }
}
