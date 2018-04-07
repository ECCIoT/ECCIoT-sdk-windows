using ECC_sdk_windows.Manager.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Manager.Function
{
    public interface IEccEvevt
    {
        /// <summary>
        /// 询问API_KEY
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_CheckAPIKey(CheckAPIKeyEventArgs args);
        /// <summary>
        /// APIKey已通过验证
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_APIKeyVerified(APIKeyVerifiedEventArgs args);
        /// <summary>
        /// APIKey无效
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_APIKeyInvalid(APIKeyInvalidEventArgs args);
        /// <summary>
        /// 接收更新设备项的数据集
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_UpdateItemsData(UpdateItemsDataEventArgs args);
        /// <summary>
        /// 接收紧急消息
        /// </summary>
        /// <param name="args"></param>
        void EccEvent_Alarm(AlarmEventArgs args);
    }
}
