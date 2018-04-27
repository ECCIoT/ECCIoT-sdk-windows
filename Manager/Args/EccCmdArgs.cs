    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Manager.Args
{
    /**
     *  参数不能使用访问器，
     *  在使用json序列化时字段首字母为大写，
     *  与Java命名规范冲突，
     *  在Java端反序列化时无法正确的构造对象。
     */


    public class CheckAPIKeyCmdArgs : BaseCmdArgs
    {
        public string apiKey;
    }
    public class ControlItemCmdArgs : BaseCmdArgs
    {
        public string itemID;
        public string atCmd;
    }
}
