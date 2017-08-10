using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System.Web.Script.Serialization;

namespace Weeho.Common.Helper
{
   public class JsonHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serializer(object obj)
        {
            if (obj != null)
            {
                string rst = string.Empty;
                var js = new JavaScriptSerializer();
                rst = js.Serialize(obj);
                return rst;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="rst"></param>
        /// <returns></returns>
        public static T Deserializer<T>(string json)
        {
            var js = new JavaScriptSerializer();
            return js.Deserialize<T>(json);
        }
    }
}
