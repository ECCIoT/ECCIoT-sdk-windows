using ECC_sdk_windows.EccArgs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows
{

    class ActionJson
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static EventJson Deserialize(string strJson)
        {
            return JsonConvert.DeserializeObject<EventJson>(strJson);
        }
    }


    class EventJson : ActionJson
    {
        public string Action { set; get; }
        public Boolean Flag { set; get; }
        public string Content { set; get; }
        public string Message { set; get; }

        public EventJson(string strJson)
        {
            JObject json = JObject.Parse(strJson);

            Action = (string)json["action"];
            Flag = (Boolean)json["flag"];
            Content = (string)json["content"];
            Message = (string)json["message"];
        }
    }

    class CmdJson : ActionJson
    {
        public string Action { set; get; }
        public string Content { set; get; }
    }
}
 