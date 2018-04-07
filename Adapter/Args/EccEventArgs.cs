using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Adapter.Args
{
    public class CheckAPIKeyEventArgs : BaseEventArgs
    {
        public CheckAPIKeyEventArgs(string content)
        {

        }
    }
    public class APIKeyVerifiedEventArgs : BaseEventArgs
    {
        public APIKeyVerifiedEventArgs(string content)
        {

        }
    }
    public class APIKeyInvalidEventArgs : BaseEventArgs
    {
        public APIKeyInvalidEventArgs(string content)
        {

        }
    }

    /// <summary>
    /// 更新项目集数据的事件参数
    /// </summary>
    public class UpdateItemsDataEventArgs : BaseEventArgs
    {
        public new static string eventActionName = "";
        public ItemData[] ItemsData { get; set; }

        public UpdateItemsDataEventArgs(string content)
        {
            //解析数据为Json数组
            JArray jarr = new JArray(content);
            //实例化数据集数组
            ItemsData = new ItemData[jarr.Count];
            //遍历数据
            for (int i=0;i<jarr.Count;i++)
            {
                //按序获取数组元素
                JObject json = new JObject(jarr[i].ToString());
                //实例化项目数据对象
                ItemsData[i] = new ItemData();
                //获取项目ID
                ItemsData[i].ItemID = json["ItemID"].ToString();
                //获取属性表
                JArray jarrAb = new JArray(json["Attribute"]);
                foreach (JObject jo in jarrAb)
                {
                    ItemsData[i].Attribute.Add(jo["Field"].ToString(), jo["Value"].ToString());
                }
            }
        }

        public class ItemData
        {
            public string ItemID { get; set; }
            public Dictionary<string, string> Attribute { get; set; }
        }
    }

    /// <summary>
    /// 警报消息的事件参数
    /// </summary>
    public class AlarmEventArgs : BaseEventArgs
    {
        public string ItemID { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }

        public AlarmEventArgs(string content)
        {
            //解析数据为Json对象
            JObject jo = new JObject(content);
            //获取数据
            ItemID = jo["ItemID"].ToString();
            Field = jo["Field"].ToString();
            Value = jo["Value"].ToString();
        }
    }
}
