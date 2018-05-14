using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECC_sdk_windows.Manager.Utils
{
    public class ArgsAttribute : Attribute
    {
        public string Action { get; set; }
        public ArgsAttribute(String action)
        {
            Action = action;
        }
    }
}
