using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Common.Constant
{
    public enum LogType
    {
        [Description("信息")]
        Info = 2,
        [Description("警告")]
        Warning = 4,
        [Description("错误")]
        Error = 8
    }
   
}
