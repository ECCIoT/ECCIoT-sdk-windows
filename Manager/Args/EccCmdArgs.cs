using ECC_sdk_windows.Manager.Utils;
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

    [ArgsAttribute("Terminal_CheckTerminalIdentity")]
    public class CheckTerminalIdentityArgs : BaseCmdArgs
    {
        public string apikey;
        public string token;
        public string platform;
        public string version;
    }

    [ArgsAttribute("Terminal_BindDevice")]
    public class BindDeviceArgs : BaseCmdArgs
    {
        public string itemID;
        public string token;
    }

    [ArgsAttribute("Terminal_ControlDevice")]
    public class ControlDeviceArgs : BaseCmdArgs
    {
        public string itemID;
        public string token;
        public string atCmd;
    }
}
