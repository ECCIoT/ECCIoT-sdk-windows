using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.EccArgs
{
    public abstract class BaseEccArgs { }

    public abstract class BaseEventArgs : BaseEccArgs
    {
        public static T Deserialize<T>(string strJson)
        {
            return JsonConvert.DeserializeObject<T>(strJson);
        }
    }

    public abstract class BaseCmdArgs : BaseEccArgs
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
