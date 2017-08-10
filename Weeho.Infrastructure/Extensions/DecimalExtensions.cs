using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Extensions
{
    /// <summary>
    /// Class DecimalExtensions.
    /// </summary>
    public static class DecimalExtensions
    {

        /// <summary>
        /// 小数转整数，类似四舍五入
        /// </summary>
        /// <param name="value">小数</param>
        /// <returns>整数</returns>
        public static int ToInt(this decimal value)
        {
            var decimalNum = value - (int)value;
            if (decimalNum >= 0.5m)
                return ((int)value) + 1;
            else
                return (int)value;
        }
        /// <summary>
        /// 小数转成价格，如3.123123会转成3.12
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="format">小数位数格式</param>
        /// <returns>string</returns>
        public static string ToPrice(this decimal value, string format = "0.00")
        {
            return value.ToString(format);
        }

        /// <summary>
        /// 价格区间，会转成如 200-300
        /// </summary>
        /// <param name="fromPrice">From price.</param>
        /// <param name="toPrice">To price.</param>
        /// <returns>System.String.</returns>
        public static string ToShortPriceRange(this decimal fromPrice, decimal toPrice)
        {
            if (fromPrice == toPrice)
                return fromPrice.ToShortPrice();
            else
                return string.Format("{0}-{1}", fromPrice.ToShortPrice(), toPrice.ToShortPrice());
        }

        /// <summary>
        /// 转成价格，如200.45将转成200，小于0时将转成"暂无价格"
        /// </summary>
        /// <param name="price">The price.</param>
        /// <param name="decimalPlaces">The decimal places.</param>
        /// <returns>System.String.</returns>
        public static string ToShortPrice(this decimal price, int decimalPlaces = 0)
        {
            //if (price < 0)
            //    return "暂无价格";
            return price.ToString("f" + decimalPlaces);
        }

        /// <summary>
        /// 转成价格，如"¥200/晚起"
        /// </summary>
        /// <param name="price">The price.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.String.</returns>
        public static string ToCnDayPrice(this decimal price, string format = "0.00")
        {
            if (price < 0)
                return "暂无报价";

            return string.Format("&yen;{0}/晚起", price.ToString(format));
        }

        /// <summary>
        /// 转成价格，如"¥200"
        /// </summary>
        /// <param name="price">The price.</param>
        /// <param name="format">The format.</param>
        /// <returns>System.String.</returns>
        public static string ToCnPrice(this decimal price, string format = "0.00")
        {
            if (price < 0)
                return "暂无报价";

            return string.Format("&yen;{0}", price.ToString(format));
        }
    }
}
