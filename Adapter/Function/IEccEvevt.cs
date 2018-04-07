using ECC_sdk_windows.Adapter.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Adapter.Function
{
    public interface IEccEvevt
    {
        //询问API_KEY
        void EccEvent_CheckAPIKey(CheckAPIKeyEventArgs args);
        //APIKey已通过验证
        void EccEvent_APIKeyVerified(APIKeyVerifiedEventArgs args);
        //APIKey无效
        void EccEvent_APIKeyInvalid(APIKeyInvalidEventArgs args);

        //接收更新设备项的数据集
        void EccEvent_UpdateItemsData(UpdateItemsDataEventArgs args);

        //接收紧急消息
        void EccEvent_Alarm(AlarmEventArgs args);
    }
}
