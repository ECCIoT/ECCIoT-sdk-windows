using ECC_sdk_windows.Manager.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Manager.Function
{
    interface ICheckIdentityCallback : IBaseEventParserCallback
    {
        void RTC_AskIdentity(AskIdentityArgs args);
        void RTC_APIKeyVerified(APIKeyVerifiedArgs args);
        void RTC_APIKeyInvalid(APIKeyInvalidArgs args);
        void RTC_CommunicationOutage(CommunicationOutageArgs args);
    }
}
