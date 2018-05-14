using ECCIoT_sdk_windows;
using System;
using ECC_sdk_windows.Comm.Listener;
using ECC_sdk_windows.Manager.Args;
using ECC_sdk_windows.Manager.Function;
using ECCIoT_sdk_windows.EccException;
using System.Net.Sockets;
using ECC_sdk_windows.Utils;
using System.Reflection;
using ECC_sdk_windows.Manager.Utils;

namespace ECC_sdk_windows.Manager
{
    /// <summary>
    /// ECC通信管理器
    /// 管理EccSocket对象，将往来的通信数据转换为具体的命令和事件
    /// </summary>
    public class EccManager : IEccReceiptListener, IEccDataReceiveListener, IEccExceptionListener, ITerminal2ServerEventCallback, IRtcCheckIdentityCallback
    {
        /*EccEvent回调接口*/
        //private IBaseEventParserCallback eccEvevt;

        /*命令内容解析器*/
        public ContentParser Parser { get; set; }

        /*IEccExceptionListener回调接口*/
        public IEccExceptionListener ExceptionListener { private get; set; }

        //ECCIoT示例
        public ECCIoT EcciotInstance { private get; set; }

        public EccManager(ContentParser parser)
        {
            Parser = parser;
        }

        public EccManager(IBaseEventParserCallback callback)
        {

            Parser = new ContentParser(callback);
        }

        public EccManager(ContentParser parser, IEccExceptionListener eccExceptionListener)
        {
            Parser = parser;
            this.ExceptionListener = eccExceptionListener;
        }

        public EccManager(IBaseEventParserCallback callback, IEccExceptionListener eccExceptionListener)
        {
            Parser = new ContentParser(callback);
            this.ExceptionListener = eccExceptionListener;
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
            ParsingCommands(new EventJson(msg));
        }

        /*异常错误回调接口*/
        void IEccExceptionListener.Ecc_BreakOff(Exception ex)
        {
            if (ExceptionListener != null)
            {
                ExceptionListener.Ecc_BreakOff(ex);
                return;
            }
            //code...
        }


        void IEccExceptionListener.Ecc_ConnectionFail(SocketException ex)
        {
            if (ExceptionListener != null)
            {
                ExceptionListener.Ecc_ConnectionFail(ex);
                return;
            }
            //code...
        }

        /// <summary>
        /// 解析命令
        /// </summary>
        /// <param name="eventJson"></param>
        private void ParsingCommands(EventJson eventJson)
        {
            Parser.Parse(eventJson.action, eventJson.content);
            /*
            switch (eventJson.action)
            {
                case "EccEvent_CheckAPIKey":
                    eccEvevt.EccEvent_CheckAPIKey(new AskIdentityArgs(eventJson.content));
                    break;
                case "EccEvent_APIKeyVerified":
                    eccEvevt.EccEvent_APIKeyVerified(new APIKeyVerifiedArgs(eventJson.content));
                    break;
                case "EccEvent_APIKeyInvalid":
                    eccEvevt.EccEvent_APIKeyInvalid(new APIKeyInvalidArgs(eventJson.content));
                    break;
                case "EccEvent_UpdateItemsData":
                    eccEvevt.EccEvent_UpdateItemsData(new UpdateItemsDataArgs(eventJson.content));
                    break;
                case "EccEvent_Alarm":
                    eccEvevt.EccEvent_Alarm(new AlarmEventArgs(eventJson.content));
                    break;
                default:
                    throw new UnknownEventException();
            }
            */
        }

        public void Send_Cmd(BaseCmdArgs args, AsyncCallback successful, AsyncCallback failure)
        {
            ArgsAttribute aa = args.GetType().GetCustomAttribute<ArgsAttribute>();

            CmdJson cmd = new CmdJson
            {
                action = aa.Action,
                content = args.ToString()
            };
            EcciotInstance.Send(cmd.ToString(), successful, failure);
        }

        void ITerminal2ServerEventCallback.Terminal_ControlDevice(ControlDeviceArgs args, AsyncCallback successful, AsyncCallback failure)
        {
            CmdJson cmd = new CmdJson
            {
                action = "Terminal_ControlDevice",
                content = args.ToString()
            };
            EcciotInstance.Send(cmd.ToString(), successful, failure);
        }

        void ITerminal2ServerEventCallback.Terminal_BindDevice(BindDeviceArgs args, AsyncCallback successful, AsyncCallback failure)
        {
            CmdJson cmd = new CmdJson
            {
                action = "Terminal_BindDevice",
                content = args.ToString()
            };
            EcciotInstance.Send(cmd.ToString(), successful, failure);
        }

        void IRtcCheckIdentityCallback.Terminal_CheckTerminalIdentity(CheckTerminalIdentityArgs args, AsyncCallback successful, AsyncCallback failure)
        {
            CmdJson cmd = new CmdJson
            {
                action = "Terminal_CheckTerminalIdentity",
                content = args.ToString()
            };
            EcciotInstance.Send(cmd.ToString(), successful, failure);
        }
    }
}
