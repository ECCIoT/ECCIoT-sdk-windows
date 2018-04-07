using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace ECC_sdk_windows
{

    public abstract class ActionJson
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


    public class EventJson : ActionJson
    {
        public string Action { set; get; }
        public Boolean Flag { set; get; }
        public string Content { set; get; }
        public string Message { set; get; }

        public EventJson(string strJson) 
        {
            try
            {
                JObject json = JObject.Parse(strJson);
                
                Action = (string)json["action"];
                if (json.ContainsKey("flag")) Flag = (Boolean)json["flag"];
                if (json.ContainsKey("content")) Content = (string)json["content"];
                if (json.ContainsKey("message")) Message = (string)json["message"]; 
            }catch(JsonReaderException ex)
            {
                throw ex;
            }catch(ArgumentNullException ex)
            {
                throw ex;
            }
        }
    }

    public class CmdJson : ActionJson
    {
        public string Action { set; get; }
        public string Content { set; get; }
    }
}
 