using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.EccArgs
{
    public class CheckAPIKeyEventArgs : BaseEventArgs
    {
        public static void Main()
        {
            CheckAPIKeyEventArgs args = CheckAPIKeyEventArgs.Deserialize<CheckAPIKeyEventArgs>("");
        }
    }
}
