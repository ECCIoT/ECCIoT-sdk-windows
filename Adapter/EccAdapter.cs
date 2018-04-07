using ECCIoT_sdk_windows;
using System;
using ECC_sdk_windows.Comm.Listener;
using ECC_sdk_windows.Adapter.Args;
using ECC_sdk_windows.Adapter.Function;
using ECCIoT_sdk_windows.EccException;
using System.Net.Sockets;

namespace ECC_sdk_windows.Adapter
{
    public class EccAdapter : IEccReceiptListener, IEccDataReceiveListener, IEccExceptionListener, IEccCmd
    {
        /*EccEventAdapter回调接口*/
        private IEccEvevt eccEvevt;

        //ECCIoT示例
        public ECCIoT EcciotInstance { private get; set; }

        public EccAdapter(IEccEvevt eccEvevt)
        {
            this.eccEvevt = eccEvevt;
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
            
        }
        void IEccExceptionListener.Ecc_ConnectionFail(SocketException ex)
        {
            
        }

        /// <summary>
        /// 解析命令
        /// </summary>
        /// <param name="eventJson"></param>
        private void ParsingCommands(EventJson eventJson)
        {
            switch (eventJson.Action)
            {
                case "EccEvent_CheckAPIKey":
                    eccEvevt.EccEvent_CheckAPIKey(new CheckAPIKeyEventArgs(eventJson.Content));
                    break;
                case "EccEvent_APIKeyVerified":
                    eccEvevt.EccEvent_APIKeyVerified(new APIKeyVerifiedEventArgs(eventJson.Content));
                    break;
                case "EccEvent_APIKeyInvalid":
                    eccEvevt.EccEvent_APIKeyInvalid(new APIKeyInvalidEventArgs(eventJson.Content));
                    break;
                case "EccEvent_UpdateItemsData":
                    eccEvevt.EccEvent_UpdateItemsData(new UpdateItemsDataEventArgs(eventJson.Content));
                    break;
                case "EccEvent_Alarm":
                    eccEvevt.EccEvent_Alarm(new AlarmEventArgs(eventJson.Content));
                    break;
                default:
                    throw new UnknownEventException();
            }
        }

        public void EccCmd_CheckAPIKey(SendAPIKeyCmdArgs args, AsyncCallback successful, AsyncCallback failure)
        {
            CmdJson cmd = new CmdJson
            {
                Action = "EccCmd_CheckAPIKey",
                Content = args.ToString()
            };
            EcciotInstance.Send(cmd.ToString(),successful, failure);
        }

        public void EccCmd_ControlItem(ControlItemCmdArgs args, AsyncCallback successful, AsyncCallback failure)
        {
            CmdJson cmd = new CmdJson
            {
                Action = "EccCmd_ControlItem",
                Content = args.ToString()
            };
            EcciotInstance.Send(cmd.ToString(),successful, failure);
        }

        
    }

    interface IEccEventAdapter
    {

    }





    

}
