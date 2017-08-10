using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Extensions
{
    public static class EnumExtension
    {
        public static Dictionary<int, string> ToDictionary<T>()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();

            foreach (var item in Enum.GetValues(typeof(T)))
            {
                string str = item.GetDescription();
                int value = (int)item;

                dic.Add(value, str);
            }

            return dic;
        }

        public static string ToHtmlSelectOptions<T>()
        {
            var dic = ToDictionary<T>();

            string str = "";

            foreach (var key in dic.Keys)
            {
                str += "<option value=\"" + key + "\">" + dic[key] + "</option>";
            }

            return str;
        }
    }
}
