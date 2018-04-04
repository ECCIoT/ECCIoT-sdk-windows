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
        public string Action { set; get; }
        public Boolean Flag { set; get; }
        public string Content { set; get; }
        public string Message { set; get; }

        public ActionJson(string strJson)
        {
            JObject json = JObject.Parse(strJson);

            Action = (string)json["action"];
            Flag = (Boolean)json["flag"];
            Content = (string)json["content"];
            Message = (string)json["message"];
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static ActionJson Deserialize(string strJson)
        {
           return JsonConvert.DeserializeObject<ActionJson>(strJson);
        }
    }
}
