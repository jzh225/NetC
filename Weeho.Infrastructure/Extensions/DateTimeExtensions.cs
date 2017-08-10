using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Extensions
{
    /// <summary>
    /// Class DateTimeExtensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// to date string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// to datetime string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// to cn date string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string ToCnDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy年MM月dd日");
        }
        /// <summary>
        /// to cn datetime string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string ToCnDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy年MM月dd日 HH时mm分ss秒");
        }

        /// <summary>
        /// to date string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string ToDateString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ((DateTime)dateTime).ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// to datetime string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string ToDateTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ((DateTime)dateTime).ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// to cn date string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string ToCnDateString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ((DateTime)dateTime).ToString("yyyy年MM月dd日");
        }
        /// <summary>
        /// to cn datetime string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string ToCnDateTimeString(this DateTime? dateTime)
        {
            if (dateTime == null)
                return string.Empty;
            return ((DateTime)dateTime).ToString("yyyy年MM月dd日 HH时mm分ss秒");
        }
        /// <summary>
        /// get datetime to now timespan description
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string GetDateTimeDescription(this DateTime? dateTime)
        {
            DateTime now = DateTime.Now;
            DateTime time = dateTime ?? DateTime.Now;
            if (time >= now)
                return "刚刚";
            TimeSpan ts = new TimeSpan(now.Ticks - time.Ticks);
            if (ts.TotalDays > 30)
                return "一个月前";
            else if (ts.TotalDays > 21)
                return "三周前";
            else if (ts.TotalDays > 14)
                return "两周前";
            else if (ts.TotalDays > 7)
                return "一周前";
            else if (ts.TotalDays > 1)
                return ts.Days + "天前";
            else if (ts.TotalHours > 1)
                return ts.Hours + "小时前";
            else if (ts.TotalMinutes > 30)
                return "半小时前";
            else if (ts.TotalMinutes > 1)
                return ts.Minutes + "分钟前";
            else
                return ts.Seconds + "秒前";
        }

        /// <summary>
        /// Gets the date time description.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.String.</returns>
        public static string GetDateTimeDescription(this DateTime dateTime)
        {
            DateTime now = DateTime.Now;
            if (dateTime >= now)
                return "刚刚";
            TimeSpan ts = new TimeSpan(now.Ticks - dateTime.Ticks);
            if (ts.TotalDays > 30)
                return "一个月前";
            else if (ts.TotalDays > 21)
                return "三周前";
            else if (ts.TotalDays > 14)
                return "两周前";
            else if (ts.TotalDays > 7)
                return "一周前";
            else if (ts.TotalDays > 1)
                return ts.Days + "天前";
            else if (ts.TotalHours > 1)
                return ts.Hours + "小时前";
            else if (ts.TotalMinutes > 30)
                return "半小时前";
            else if (ts.TotalMinutes > 1)
                return ts.Minutes + "分钟前";
            else
                return ts.Seconds + "秒前";
        }
        /// <summary>
        /// get cn week string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string GetCnWeekString(this DateTime dateTime)
        {
            var dayOfWeek = (int)dateTime.DayOfWeek;

            string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            return weekdays[dayOfWeek];
        }
        /// <summary>
        /// get cn short week string
        /// </summary>
        /// <param name="dateTime">datetime</param>
        /// <returns>string</returns>
        public static string GetCnShortWeekString(this DateTime dateTime)
        {
            var dayOfWeek = (int)dateTime.DayOfWeek;

            string[] weekdays = { "日", "一", "二", "三", "四", "五", "六" };
            return weekdays[dayOfWeek];
        }
    }
}
