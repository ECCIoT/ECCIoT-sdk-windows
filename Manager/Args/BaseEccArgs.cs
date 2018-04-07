using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Manager.Args
{
    /// <summary>
    /// ECC事件参数的基类
    /// </summary>
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

    /// <summary>
    /// 事件参数的基类
    /// </summary>
    public abstract class BaseEventArgs : BaseEccArgs
    {
        public static string eventActionName = "";
        public static string EventActionName { get; set; }

        /// <summary>
        /// 获取BaseEventArgs类所属的所有直属子类
        /// </summary>
        public static List<Type> GetSubclassTypes()
        {
            List<Type> lstType = new List<Type>();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type t in types)
            {
                if (t.BaseType == typeof(BaseEventArgs))
                {
                    lstType.Add(t);
                }
            }
            return lstType;
        }
    }

    /// <summary>
    /// 命令参数的基类
    /// </summary>
    public abstract class BaseCmdArgs : BaseEccArgs {}
}
