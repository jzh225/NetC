using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Common.Constant
{
    public class ConfigurationDict
    {
        public static readonly IDictionary<string, string> HttpMethod;
       
       
        static ConfigurationDict()
        {
            HttpMethod = new Dictionary<string, string>();
            HttpMethod.Add("get","get");
            HttpMethod.Add("post","post");
        }
    }
}
