using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure
{
    public class Code
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pre"></param>
        /// <returns></returns>
        public static string GenerateTransactionId(string pre="")
        {
            //string random = new Random(DateTime.Now.Millisecond).Next(1000,9999).ToString();
            string random = GenerateCheckCodeNum(6);
            return string.Format("{0}{1}{2}",pre,DateTime.Now.ToString("yyyyMMddHHmmss"),random);
        }
        private static int rep = 0;
        /// <summary> 
        /// 生成随机数字字符串 
        /// </summary> 
        /// <param name="codeCount">待生成的位数</param> 
        /// <returns>生成的数字字符串</returns> 
        private static string GenerateCheckCodeNum(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                int num = random.Next();
                str = str + ((char)(0x30 + ((ushort)(num % 10)))).ToString();
            }
            return str;
        }

        /// <summary> 
        /// 生成随机字母字符串(数字字母混和) 
        /// </summary> 
        /// <param name="codeCount">待生成的位数</param> 
        /// <returns>生成的字母字符串</returns> 
        private static string GenerateCheckCode(int codeCount)
        {
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
}
