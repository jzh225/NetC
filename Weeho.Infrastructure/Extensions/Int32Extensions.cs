using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Extensions
{
    /// <summary>
    /// Class Int32Extensions.
    /// </summary>
    public static class Int32Extensions
    {

        /// <summary>
        /// To the enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public static T ToEnum<T>(this int value) where T : struct
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal ToDecimal(this int value)
        {
            return (decimal)value;
        }
        /// <summary>
        /// 超过5位的数字加"万"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToFansString(this int value, string lbl = "W")
        {
            if (value >= 10000)
            {
                return string.Format("{0}{1}", value / 10000, lbl);
            }
            else
            {
                return value.ToString();
            }
        }
        public static string GetCommentTest(this int starCount)
        {
            if (starCount < 1)
            {
                return "差评";
            }
            else if (starCount >= 1 && starCount <= 3)
            {
                return "中评";
            }
            else
            {
                return "好评";
            }
        }
    }
}
