using ECC_sdk_windows.Manager.Args;
using ECC_sdk_windows.Manager.Function;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Utils
{
    public class ContentParser
    {
        private IBaseEventParserCallback callback;
        private Type type;

        public ContentParser(IBaseEventParserCallback callback)
        {
            this.callback = callback;
            this.type = callback.GetType();
        }

        public ContentParser(IBaseEventParserCallback callback,Type type)
        {
            this.callback = callback;
            this.type = type;
        }

        public Boolean Parse(string action, string content)
        {
            //获取回调接口中的所有方法
            MethodInfo[] mis = type.GetMethods();
            //遍历方法
            foreach (MethodInfo mi in mis)
            {
                //获取方法名称
                string mn = mi.Name;
                //若方法不匹配则继续下一次遍历
                if (mn != action) continue;
                //获取方法中所有参数
                ParameterInfo[] pis = mi.GetParameters();
                //遍历参数查找命令参数类型
                Type typeArgs = null;
                foreach (ParameterInfo pi in pis)
                {
                    //检验父类
                    if(pi.ParameterType.BaseType == typeof(BaseEventArgs))
                    {
                        typeArgs = pi.ParameterType;
                        break;
                    }
                }
                if (typeArgs == null)
                {
                    //返回解析失败(参数类设计有误或回调方法参数设计有误)
                    return false;
                }
                //根据参数类型通过反射创建对象（执行构造函数）
                //构造函数参数数组
                object[] parameters = new object[1] { content };
                // 创建类的实例 
                object objArgs = Assembly.GetExecutingAssembly().CreateInstance(typeArgs.FullName, true, BindingFlags.Default, null, parameters, null, null);
                
                //反射调用对应的回调方法
                mi.Invoke(callback, new object[1] { objArgs });

                //返回解析成功
                return true;
            }
            //调用表示无法解析的回调方法
            callback.InvalidActionInstruction(action,content);
            //返回解析失败
            return false;
        }
    }
    
}
