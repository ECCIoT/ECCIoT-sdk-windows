using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.EccArgs
{
    public abstract class BaseEccArgs
    {
        public static T Deserialize<T>(string strJson)
        {
            return JsonConvert.DeserializeObject<T>(strJson);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public abstract class BaseEventArgs : BaseEccArgs {}

    public abstract class BaseCmdArgs : BaseEccArgs {}
}
