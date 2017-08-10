using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Common.Helper
{
    public class ReturnJsonFormat
    {
        public bool Success { get; set; }

        public string Msg { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        public object Data { get; set; }

        public ReturnJsonFormat()
        {
            Success = true;
            Msg = "";
            Code = "";
            Status = "";
            Data = null;
        }

    }

}
