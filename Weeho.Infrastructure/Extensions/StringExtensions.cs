using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;

namespace Weeho.Infrastructure.Extensions
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// to datetime
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="defaultValue">default</param>
        /// <returns>datetime</returns>
        public static DateTime ToDateTime(this string value, DateTime defaultValue = new DateTime())
        {
            DateTime result = defaultValue;
            if (DateTime.TryParse(value, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// 超过5位的数字加"万"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToPriceString(this string value)
        {
            string temp = value.Split('.')[0];
            if (temp.Length >= 5)
            {
                return string.Format("{0:0.00万}", (decimal)(decimal.Parse(value) / 10000));
            }
            else
            {
                return string.Format("{0:0.00}", decimal.Parse(value));
            }
        }

        /// <summary>
        /// 截取指定数量的长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length">指定长度</param>
        /// <param name="endWith">尾部增加的字符</param>
        /// <returns></returns>
        public static string ToOmitString(this string value, int length, string endWith)
        {
            if (string.IsNullOrEmpty(value))
                // throw new NullReferenceException("原字符串不能为空");  
                return value + endWith;
            if (length < 1)
                throw new Exception("返回的字符串长度必须大于[0]");
            if (value.Length > length)
            {
                string strTmp = value.Substring(0, length);
                if (string.IsNullOrEmpty(endWith))
                    return strTmp;
                else
                    return strTmp + endWith;
            }
            return value;
        }

        public static int ToPwdSecurityNumber(this string password)
        {
            int[] arr = { 0, 30, 60, 100 };
            //空字符串强度值为0
            if (password == "") return 0;
            //字符统计
            int iNum = 0, iLtt = 0, iSym = arr[0];
            foreach (char c in password)
            {
                if (c >= '0' && c <= '9') iNum++;
                else if (c >= 'a' && c <= 'z') iLtt++;
                else if (c >= 'A' && c <= 'Z') iLtt++;
                else iSym++;
            }
            if (iLtt == 0 && iSym == 0) return arr[1]; //纯数字密码
            if (iNum == 0 && iLtt == 0) return arr[1]; //纯符号密码
            if (iNum == 0 && iSym == 0) return arr[1]; //纯字母密码
            if (password.Length <= 6) return arr[1]; //长度不大于6的密码
            if (iLtt == 0) return arr[2]; //数字和符号构成的密码
            if (iSym == 0) return arr[2]; //数字和字母构成的密码
            if (iNum == 0) return arr[2]; //字母和符号构成的密码
            if (password.Length <= 10) return arr[2]; //长度不大于10的密码
            return arr[3]; //由数字、字母、符号构成的密码 
        }

        /// <summary>
        /// to double
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="defaultValue">default</param>
        /// <returns>double</returns>
        public static double ToDouble(this string value, double defaultValue = 0)
        {
            double result = defaultValue;
            if (double.TryParse(value, out result))
                return result;
            else
                return defaultValue;
        }
        /// <summary>
        /// to bool
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="defaultValue">default</param>
        /// <returns>bool</returns>
        public static bool ToBool(this string value, bool defaultValue = false)
        {
            bool result = defaultValue;
            if (bool.TryParse(value, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// to int
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="defaultValue">default</param>
        /// <returns>int</returns>
        public static int ToInt(this string value, int defaultValue = 0)
        {
            int result = defaultValue;
            if (int.TryParse(value, out result))
                return result;
            else
                return defaultValue;
        }
        /// <summary>
        /// To the long.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        public static long ToLong(this string value, long defaultValue = 0)
        {
            long result = defaultValue;
            if (long.TryParse(value, out result))
                return result;
            else
                return defaultValue;
        }

        /// <summary>
        /// to decimal
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>decimal</returns>
        public static decimal ToDecimal(this string value, decimal defaultValue = 0)
        {
            decimal result = defaultValue;
            if (decimal.TryParse(value, out result))
                return result;
            return defaultValue;
        }

        /// <summary>
        /// isint
        /// </summary>
        /// <param name="value">string</param>
        /// <returns>int</returns>
        public static bool IsInt(this string value)
        {
            int result = 0;
            return int.TryParse(value, out result);
        }
        /// <summary>
        /// Determines whether the specified value is long.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is long; otherwise, <c>false</c>.</returns>
        public static bool IsLong(this string value)
        {
            long result = 0;
            return long.TryParse(value, out result);
        }
        /// <summary>
        /// Determines whether [contains ingore case] [the specified compare].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="compare">The compare.</param>
        /// <returns><c>true</c> if [contains ingore case] [the specified compare]; otherwise, <c>false</c>.</returns>
        public static bool ContainsIngoreCase(this string value, string compare)
        {
            return value.ToLower().Contains(compare.ToLower());
        }
        /// <summary>
        /// Determines whether [is null or empty] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is null or empty] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// Shorters the than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ShorterThan(this string value, int maxLength)
        {
            return value.Length >= maxLength;
        }
        /// <summary>
        /// Subs the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string SubString(this string value, int length)
        {
            if (value.IsNullOrEmpty() || value.Length <= length)
                return value;
            return value.Substring(0, length);
        }
        #region 正则表达式
        /// <summary>
        /// 是否电子邮件
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is email; otherwise, <c>false</c>.</returns>
        public static bool IsEmail(this string value)
        {
            string text = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(value, text);
        }

        /// <summary>
        /// 是否Ip
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is ip; otherwise, <c>false</c>.</returns>
        public static bool IsIp(this string value)
        {
            string text = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
            return Regex.IsMatch(value, text);
        }

        /// <summary>
        /// 是否整数
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is numeric; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(this string value)
        {
            string text = @"^\-?[0-9]+$";
            return Regex.IsMatch(value, text);
        }

        /// <summary>
        /// 是否绝对路径
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is physical path] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsPhysicalPath(this string value)
        {
            string text = @"^\s*[a-zA-Z]:.*$";
            return Regex.IsMatch(value, text);
        }

        /// <summary>
        /// 是否相对路径
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is relative path] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsRelativePath(this string value)
        {
            if (value.IsNullOrEmpty())
                return false;
            if (value.StartsWith("/") || value.StartsWith("?"))
                return false;
            if (Regex.IsMatch(value, @"^\s*[a-zA-Z]{1,10}:.*$"))
                return false;
            return true;
        }

        /// <summary>
        /// 是否安全字符串，例如包含"slect insert"等注入关键字
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is safety; otherwise, <c>false</c>.</returns>
        public static bool IsSafety(this string value)
        {
            string text1 = value.Replace("%20", " ");
            text1 = Regex.Replace(text1, @"\s", " ");
            string text2 = "select |insert |delete from |count\\(|drop table|update |truncate |asc\\(|mid\\(|char\\(|xp_cmdshell|exec master|net localgroup administrators|:|net user|\"|\\'| or ";
            return !Regex.IsMatch(text1, text2, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Determines whether the specified value is unicode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is unicode; otherwise, <c>false</c>.</returns>
        public static bool IsUnicode(this string value)
        {
            string text = @"^[\u4E00-\u9FA5\uE815-\uFA29]+$";
            return Regex.IsMatch(value, text);
        }

        /// <summary>
        /// Determines whether the specified value is URL.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is URL; otherwise, <c>false</c>.</returns>
        public static bool IsUrl(this string value)
        {
            string text = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
            return Regex.IsMatch(value, text, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否是身份证号，验证以下3种情况:
        /// 1、身份证号码为15位数字；
        /// 2、身份证号码为18位数字；
        /// 3、身份证号码为17位数字+1个字母
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is identity card] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsIdentityCard(this string value)
        {
            return Regex.IsMatch(value, @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否是手机号
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isRestrict">是否按严格格式验证</param>
        /// <returns><c>true</c> if [is mobile no] [the specified is restrict]; otherwise, <c>false</c>.</returns>
        public static bool IsMobileNo(this string value, bool isRestrict = false)
        {
            //if (!isRestrict)
            //{
            //    return Regex.IsMatch(value, @"^[1]\d{10}$", RegexOptions.IgnoreCase);
            //}
            return Regex.IsMatch(value, @"^[1][3-9]\d{9}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Determines whether [is contain character and number] [the specified is restrict].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isRestrict">if set to <c>true</c> [is restrict].</param>
        /// <returns><c>true</c> if [is contain character and number] [the specified is restrict]; otherwise, <c>false</c>.</returns>
        public static bool IsContainCharAndNum(this string value, bool isRestrict = false)
        {
            Regex pattern = new Regex("^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,16}$");
            return !pattern.IsMatch(value);
        }

        public static bool CheckPassword(this string value, bool isRestrict = false)
        {
            Regex pattern = new Regex(@"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,10}$");
            return pattern.IsMatch(value);
        }
        #endregion

        /// <summary>
        /// 从字符串中的尾部删除指定的字符串
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="removedString">The removed string.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">原字符串中不包含移除字符串！</exception>
        public static string Remove(this string sourceString, string removedString)
        {
            try
            {
                if (sourceString.IndexOf(removedString) < 0)
                    throw new Exception("原字符串中不包含移除字符串！");
                string result = sourceString;
                int lengthOfSourceString = sourceString.Length;
                int lengthOfRemovedString = removedString.Length;
                int startIndex = lengthOfSourceString - lengthOfRemovedString;
                string tempSubString = sourceString.Substring(startIndex);
                if (tempSubString.ToUpper() == removedString.ToUpper())
                {
                    result = sourceString.Remove(startIndex, lengthOfRemovedString);
                }
                return result;
            }
            catch
            {
                return sourceString;
            }
        }

        /// <summary>
        /// 获取拆分符右边的字符串
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="splitChar">The split character.</param>
        /// <returns>System.String.</returns>
        public static string RightSplit(this string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[tempString.Length - 1].ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取拆分符左边的字符串
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="splitChar">The split character.</param>
        /// <returns>System.String.</returns>
        public static string LeftSplit(this string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[0].ToString();
            }
            return result;
        }

        /// <summary>
        /// 去掉最后一个逗号
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <returns>System.String.</returns>
        public static string DelLastComma(this string origin)
        {
            if (origin.IndexOf(",") == -1)
            {
                return origin;
            }
            return origin.Substring(0, origin.LastIndexOf(","));
        }

        /// <summary>
        /// 删除不可见字符
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns>System.String.</returns>
        public static string DeleteUnVisibleChar(this string sourceString)
        {
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder(131);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int Unicode = sourceString[i];
                if (Unicode >= 16)
                {
                    sBuilder.Append(sourceString[i].ToString());
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 获取数组元素的合并字符串
        /// </summary>
        /// <param name="stringArray">The string array.</param>
        /// <returns>System.String.</returns>
        public static string GetArrayString(this string[] stringArray)
        {
            string totalString = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                totalString = totalString + stringArray[i];
            }
            return totalString;
        }

        /// <summary>
        /// 获取某一字符串在字符串数组中出现的次数
        /// </summary>
        /// <param name="stringArray">The string array.</param>
        /// <param name="findString">The find string.</param>
        /// <returns>A int value...</returns>
        public static int GetStringCount(this string[] stringArray, string findString)
        {
            int count = -1;
            string totalString = GetArrayString(stringArray);
            string subString = totalString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = totalString.Substring(subString.IndexOf(findString));
                count += 1;
            }
            return count;
        }

        /// <summary>
        /// 获取某一字符串在字符串中出现的次数
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="findString">匹配字符串</param>
        /// <returns>匹配字符串数量</returns>
        public static int GetStringCount(this string sourceString, string findString)
        {
            int count = 0;
            int findStringLength = findString.Length;
            string subString = sourceString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = subString.Substring(subString.IndexOf(findString) + findStringLength);
                count += 1;
            }
            return count;
        }

        /// <summary>
        /// 截取从startString开始到原字符串结尾的所有字符
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="startString">The start string.</param>
        /// <returns>A string value...</returns>
        public static string GetSubString(this string sourceString, string startString)
        {
            try
            {
                int index = sourceString.ToUpper().IndexOf(startString);
                if (index > 0)
                {
                    return sourceString.Substring(index);
                }
                return sourceString;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Gets the sub string.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <param name="beginRemovedString">The begin removed string.</param>
        /// <param name="endRemovedString">The end removed string.</param>
        /// <returns>System.String.</returns>
        public static string GetSubString(this string sourceString, string beginRemovedString, string endRemovedString)
        {
            try
            {
                if (sourceString.IndexOf(beginRemovedString) != 0)
                    beginRemovedString = "";

                if (sourceString.LastIndexOf(endRemovedString, sourceString.Length - endRemovedString.Length) < 0)
                    endRemovedString = "";

                int startIndex = beginRemovedString.Length;
                int length = sourceString.Length - beginRemovedString.Length - endRemovedString.Length;
                if (length > 0)
                {
                    return sourceString.Substring(startIndex, length);
                }
                return sourceString;
            }
            catch
            {
                return sourceString; ;
            }
        }

        /// <summary>
        /// 按字节数取出字符串的长度
        /// </summary>
        /// <param name="strTmp">要计算的字符串</param>
        /// <returns>字符串的字节数</returns>
        public static int GetByteCount(this string strTmp)
        {
            int intCharCount = 0;
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (System.Text.UTF8Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intCharCount = intCharCount + 2;
                }
                else
                {
                    intCharCount = intCharCount + 1;
                }
            }
            return intCharCount;
        }

        /// <summary>
        /// 按字节数要在字符串的位置
        /// </summary>
        /// <param name="intIns">字符串的位置</param>
        /// <param name="strTmp">要计算的字符串</param>
        /// <returns>字节的位置</returns>
        public static int GetByteIndex(int intIns, string strTmp)
        {
            int intReIns = 0;
            if (strTmp.Trim() == "")
            {
                return intIns;
            }
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (System.Text.UTF8Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intReIns = intReIns + 2;
                }
                else
                {
                    intReIns = intReIns + 1;
                }
                if (intReIns >= intIns)
                {
                    intReIns = i + 1;
                    break;
                }
            }
            return intReIns;
        }

        /// <summary>
        /// Cuts the right string.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <param name="len">The length.</param>
        /// <returns>System.String.</returns>
        public static string CutRightString(this string inputString, int len)
        {
            if (string.IsNullOrEmpty(inputString))
                return string.Empty;

            var input = Reverse(inputString);
            var output = CutString(input, len);
            return Reverse(output);
        }

        /// <summary>
        /// 从包含中英文的字符串中截取固定长度的一段，inputString为传入字符串，len为截取长度（一个汉字占两个位）。
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <param name="len">The length.</param>
        /// <returns>System.String.</returns>
        public static string CutString(this string inputString, int len)
        {
            if (inputString == null || inputString == "")
            {
                return "";
            }

            inputString = inputString.Trim();
            byte[] myByte = System.Text.Encoding.Default.GetBytes(inputString);
            if (myByte.Length > len)
            {
                string result = "";
                for (int i = 0; i < inputString.Length; i++)
                {
                    byte[] tempByte = System.Text.Encoding.Default.GetBytes(result);
                    if (tempByte.Length < len)
                    {
                        result += inputString.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return result + "...";
            }
            else
            {
                return inputString;
            }
        }

        /// <summary>
        /// 从包含中英文的字符串中截取固定长度的一段，inputString为传入字符串，len为截取长度（一个汉字占两个位）。
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <param name="len">The length.</param>
        /// <param name="end">The end.</param>
        /// <returns>System.String.</returns>
        public static string CutString(this string inputString, int len, string end)
        {
            inputString = inputString.Trim();
            byte[] myByte = System.Text.Encoding.Default.GetBytes(inputString);
            if (myByte.Length > len)
            {
                string result = "";
                for (int i = 0; i < inputString.Length; i++)
                {
                    byte[] tempByte = System.Text.Encoding.Default.GetBytes(result);
                    if (tempByte.Length < len)
                    {
                        result += inputString.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return result + end;
            }
            else
            {
                return inputString;
            }
        }

        /// <summary>
        /// 去除文本中的html代码。
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>System.String.</returns>
        public static string RemoveHtml(this string inputString)
        {
            return System.Text.RegularExpressions.Regex.Replace(inputString, @"<[^>]+>", "");
        }
        /// <summary>
        /// 去除文本中的换行代码。
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>System.String.</returns>
        public static string RemoveLine(this string inputString)
        {
            return inputString.Replace(@"\r", "").Replace(@"\n", "").Replace(@"\t", "");
        }
        /// <summary>
        /// 半角转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        /// <remarks>全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</remarks>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }


        /// <summary>
        /// 全角转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        /// <remarks>全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248</remarks>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// HTMLs the encode.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="encodeBlank">if set to <c>true</c> [encode blank].</param>
        /// <returns>System.String.</returns>
        public static string HtmlEncode(this string str, bool encodeBlank = true)
        {
            if ((str == "") || (str == null))
                return "";

            StringBuilder builder1 = new StringBuilder(str);

            builder1.Replace("&", "&amp;");
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            builder1.Replace("\"", "&quot;");
            builder1.Replace("'", "&#39;");
            builder1.Replace("\t", "&nbsp; &nbsp; ");

            if (encodeBlank)
                builder1.Replace(" ", "&nbsp;");

            builder1.Replace("\r", "");
            builder1.Replace("\n\n", "<p><br/></p>");
            builder1.Replace("\n", "<br />");
            return builder1.ToString();
        }

        /// <summary>
        /// Texts the encode.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        public static string TextEncode(this string s)
        {
            StringBuilder builder1 = new StringBuilder(s);
            builder1.Replace("&", "&amp;");
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            builder1.Replace("\"", "&quot;");
            builder1.Replace("'", "&#39;");
            return builder1.ToString();
        }

        /// <summary>
        /// Reverses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static string Repeat(this string s, int count)
        {
            return new StringBuilder(s.Length * count).Insert(0, s, count).ToString();
        }
        /// <summary>
        /// Masks the string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>System.String.</returns>
        public static string MaskString(this string s, int left = 3, int right = 3)
        {
            if (s.IsNullOrEmpty())
                return s;
            if (s.Length <= left + right)
                return s;
            string leftStr = s.Substring(0, left);
            string rightStr = s.Substring(s.Length - right, right);
            return leftStr + "***" + rightStr;
        }
        /// <summary>
        /// Gets the password level.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>PasswordLevel.</returns>
        public static PasswordLevel GetPasswordLevel(this string s)
        {
            return PasswordLevelKit.GetPasswordLevel(s);
        }

        /// <summary>
        /// To the m d5.
        /// </summary>
        /// <param name="a">a.</param>
        /// <returns>System.String.</returns>
        public static string ToMD5(this string a)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(a, "MD5");
        }

        public static string GenerateCheckCode(int codeCount)
        {
            int rep = 0;
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
    }
    /// <summary>
    /// Enum PasswordLevel
    /// </summary>
    /// 密码强度
    public enum PasswordLevel
    {
        /// <summary>
        /// The none
        /// </summary>
        None, //空
        /// <summary>
        /// The very week
        /// </summary>
        Very_Week, //很弱
        /// <summary>
        /// The week
        /// </summary>
        Week,  //弱
        /// <summary>
        /// The good
        /// </summary>
        Good,//好
        /// <summary>
        /// The strong
        /// </summary>
        Strong, //强
        /// <summary>
        /// The very strong
        /// </summary>
        Very_Strong //极强
    }
    /// <summary>
    /// Class PasswordLevelKit.
    /// </summary>
    /// 测试密码强度工具类
    /// 请参考http://www.refly.net/passwordchecker/
    class PasswordLevelKit
    {
        /// <summary>
        /// Gets the password level.
        /// </summary>
        /// <param name="paswword">The paswword.</param>
        /// <returns>PasswordLevel.</returns>
        public static PasswordLevel GetPasswordLevel(string paswword)
        {
            int nScore = GetPasswordScore(paswword);
            if (nScore >= 0 && nScore < 20) return PasswordLevel.Very_Week;
            if (nScore >= 20 && nScore < 40) return PasswordLevel.Week;
            if (nScore >= 40 && nScore < 60) return PasswordLevel.Good;
            if (nScore >= 60 && nScore < 80) return PasswordLevel.Strong;
            if (nScore >= 80) return PasswordLevel.Very_Strong;
            return PasswordLevel.None;
        }

        /// <summary>
        /// Gets the password score.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPasswordScore(string password)
        {
            password = new Regex(@"\s+").Replace(password, ""); //去空格
            if (password.Length == 0) return 0;
            string lowerPasswordString = password.ToLower();//全部转为小写形式
            int score = 0;//得分分数
            int nLen = password.Length;//密码字符总个数
            int nAlphaUC = 0; //大写英文字母个数
            int nAlphaLC = 0;  //小写英文字母个数
            int nNumber = 0;  //数字字符个数
            int nSymbol = 0;  //特殊字符个数
            int nMidChar = 0; //密码中间部分出现数字或者特殊字符的个数
            int nRequirements = 0;//达到最低需求四个条件的个数
            int nRepChar = 0; //重复字符的个数
            int nConsecAlphaUC = 0; //连续大写字母个数
            int nConsecAlphaLC = 0; //连续小写字母个数
            int nConsecNumber = 0;  //连续数字字符个数
            int nSeqAlpha = 0;//依顺序出现的字母序列的个数
            int nSeqNumber = 0;//依顺序出现的数字序列的个数
            int nTmpAlphaUC = 0;
            int nTmpAlphaLC = 0;
            int nTmpNumber = 0;

            for (int i = 0; i < nLen; i++)
            {
                if (char.IsUpper(password[i]))
                {
                    if (nTmpAlphaUC != 0) if ((nTmpAlphaUC + 1) == i) nConsecAlphaUC++;
                    nTmpAlphaUC = i;
                    nAlphaUC++;
                }
                else if (char.IsLower(password[i]))
                {
                    if (nTmpAlphaLC != 0) if ((nTmpAlphaLC + 1) == i) nConsecAlphaLC++;
                    nTmpAlphaLC = i;
                    nAlphaLC++;
                }
                else if (char.IsNumber(password[i]))
                {
                    if (i > 0 && i < (nLen - 1))
                    {
                        nMidChar++;
                    }
                    if (nTmpNumber != 0) if ((nTmpNumber + 1) == i) nConsecNumber++;
                    nTmpNumber = i;
                    nNumber++;
                }
                else
                {
                    if (i > 0 && i < (nLen - 1))
                    {
                        nMidChar++;
                    }
                    nSymbol++;
                }

                for (int j = 0; j < nLen; j++)
                {
                    if (i != j && lowerPasswordString[i].Equals(lowerPasswordString[j]))
                    {
                        nRepChar++;
                    }
                }
            }
            string sAlphas = "abcdefghijklmnopqrstuvwxyz";
            string sNumerics = "01234567890";
            int nSeqCount = 3;//规则:依顺序出现三个以上

            for (int i = 0; i < sAlphas.Length - nSeqCount; i++)
            {
                string sFwd = sAlphas.Substring(i, nSeqCount);
                char[] sRev = sFwd.ToCharArray();
                System.Array.Reverse(sRev);
                if (lowerPasswordString.IndexOf(sFwd) != -1
                        || lowerPasswordString.IndexOf(sRev.ToString()) != -1)
                    nSeqAlpha++;
            }

            for (int i = 0; i < sNumerics.Length - nSeqCount; i++)
            {
                string sFwd = sNumerics.Substring(i, nSeqCount);
                char[] sRev = sFwd.ToCharArray();
                System.Array.Reverse(sRev);
                if (lowerPasswordString.IndexOf(sFwd) != -1
                        || lowerPasswordString.IndexOf(sRev.ToString()) != -1)
                    nSeqNumber++;
            }
            score += nLen * 4;     //密码长度，加分
            score += nAlphaUC * 2;  // 大写字母字符个数，加分
            score += nAlphaLC * 2;  // 小写字母字符个数，加分
            score += nNumber * 4;   // 数字字符个数，加分
            score += nSymbol * 6;   // 特殊字符个数，加分
            score += nMidChar * 2;  // 密码中间部分出现数字或者特殊字符, 加分
            if (nAlphaUC > 0) nRequirements++;
            if (nAlphaLC > 0) nRequirements++;
            if (nNumber > 0) nRequirements++;
            if (nSymbol > 0) nRequirements++;
            if (nLen >= 8 && nRequirements >= 3) score += (nRequirements + 1) * 2; //满足最低需求, 加分
            if ((nAlphaLC > 0 || nAlphaUC > 0) && nSymbol == 0 && nNumber == 0) score -= nLen; //只有英文字母字符,扣分
            if (nAlphaLC == 0 && nAlphaUC == 0 && nSymbol == 0 && nNumber > 0) score -= nLen; //只有数字字符,扣分
            score -= nRepChar * (nRepChar - 1); //出现重复字符(忽略大小写), 扣分
            score -= nConsecAlphaUC * 2;   //大写字母连续（例如AD）,扣分
            score -= nConsecAlphaLC * 2;   //小写字母连续（例如AD）,扣分
            score -= nConsecNumber * 2;    //数字字符连续（例如13）,扣分
            score -= nSeqAlpha * 3;      //字母依顺序出现 (三个以上，例如abc）,扣分
            score -= nSeqNumber * 3;      //数字依顺序出现 (三个以上，例如123）,扣分
            if (score < 0) score = 0; if (score > 100) score = 100;
            return score;
        }
    }
}
