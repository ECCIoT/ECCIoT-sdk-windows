using ECC_sdk_windows.Manager.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Manager.Function
{
    public interface IBaseEventParserCallback
    {
        /*
        /// <summary>
        /// 询问API_KEY
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_CheckAPIKey(AskIdentityArgs args);
        /// <summary>
        /// APIKey已通过验证
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_APIKeyVerified(APIKeyVerifiedArgs args);
        /// <summary>
        /// APIKey无效
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_APIKeyInvalid(APIKeyInvalidArgs args);
        /// <summary>
        /// 接收更新设备项的数据集
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_UpdateItemsData(UpdateItemsDataArgs args);
        /// <summary>
        /// 接收紧急消息
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_Alarm(AlarmEventArgs args);
        */
        void InvalidActionInstruction(String action, String content);


    }
}
