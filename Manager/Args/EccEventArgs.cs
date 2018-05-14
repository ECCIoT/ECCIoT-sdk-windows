using ECC_sdk_windows.Manager.Utils;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ECC_sdk_windows.Manager.Args
{
    /// <summary>
    /// 询问APIKey的事件参数
    /// </summary>
    public class AskIdentityArgs : BaseEventArgs
    {
        public string message;
        public AskIdentityArgs(string content)
        {
            //解析数据为Json对象
            JObject jo = new JObject(content);
            //获取数据
            message = jo["message"].ToString();
        }
    }

    /// <summary>
    /// APIKey已验证的事件参数
    /// </summary>
    public class APIKeyVerifiedArgs : BaseEventArgs
    {
        public APIKeyVerifiedArgs(string content)
        {

        }
    }

    /// <summary>
    /// APIKey无效的事件参数
    /// </summary>
    public class APIKeyInvalidArgs : BaseEventArgs
    {
        public string message;
        public APIKeyInvalidArgs(string content)
        {
            //解析数据为Json对象
            JObject jo = new JObject(content);
            //获取数据
            message = jo["message"].ToString();
        }
    }

    /// <summary>
    /// 更新项目集数据的事件参数
    /// </summary>
    [ArgsAttribute("")]
    public class UpdateItemsDataArgs : BaseEventArgs
    {
        public new static string eventActionName = "";
        public ItemData[] itemsData;

        public UpdateItemsDataArgs(string content)
        {
            //解析数据为Json对象
            JObject jobj = new JObject(content);
            //获取json数组
            JArray jarr = new JArray(jobj["array"]);
            //实例化数据集数组
            itemsData = new ItemData[jarr.Count];
            //遍历数据
            for (int i = 0; i < jarr.Count; i++)
            {
                //按序获取数组元素
                JObject json = new JObject(jarr[i].ToString());
                //实例化项目数据对象
                itemsData[i] = new ItemData();
                //获取项目ID
                itemsData[i].itemID = json["itemID"].ToString();
                //获取属性表
                JArray jarrAb = new JArray(json["attribute"]);
                foreach (JObject jo in jarrAb)
                {
                    itemsData[i].attribute.Add(jo["field"].ToString(), jo["value"].ToString());
                }
            }
        }

        public class ItemData
        {
            public string itemID;
            public Dictionary<string, string> attribute;
        }
    }

    /// <summary>
    /// 警报消息的事件参数
    /// </summary>
    public class AlarmEventArgs : BaseEventArgs
    {
        public string itemID;
        public string field;
        public string value;

        public AlarmEventArgs(string content)
        {
            //解析数据为Json对象
            JObject jo = new JObject(content);
            //获取数据
            itemID = jo["itemID"].ToString();
            field = jo["field"].ToString();
            value = jo["value"].ToString();
        }
    }

    [ArgsAttribute("RTC_CommunicationOutage")]
    public class CommunicationOutageArgs : BaseEventArgs
    {
        string message;
        public CommunicationOutageArgs(string content)
        {
            //解析数据为Json对象
            JObject jo = new JObject(content);
            //获取数据
            message = jo["message"].ToString();
        }
    }
}
