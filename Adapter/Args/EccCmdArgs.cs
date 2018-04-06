using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Adapter.Args
{
    public class SendAPIKeyCmdArgs : BaseCmdArgs
    {
        public string APIKey { get; set; }
    }
    public class ControlItemCmdArgs : BaseCmdArgs
    {
        public string ItemID { get; set; }
        public string ATCmd  { get; set; }
    }
}
