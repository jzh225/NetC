using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Common
{
    /// <summary>
    /// 加密解密相关
    /// </summary>
    public class Security
    {

        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns></returns>
        public static string Md5(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext))
                return "";

            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] _byte = md5.ComputeHash(GetBytes(plaintext));
            md5.Clear();
            return GetString(_byte);
        }

        #region SHA256

        /// <summary>
        /// SHA256
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns></returns>
        public static string SHA256(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext))
                return "";

            System.Security.Cryptography.SHA256 sha256 = new System.Security.Cryptography.SHA256Managed();
            byte[] _byte = sha256.ComputeHash(GetBytes(plaintext));
            sha256.Clear();
            return GetString(_byte);
        }
        private static string GetString(byte[] _byte)
        {

            //return System.Text.Encoding.Unicode.GetString(_byte);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item in _byte)
            {
                sb.Append(item.ToString("X").PadLeft(2, '0'));
            }

            string passwd = sb.ToString();
            passwd = passwd.Substring(1) + passwd.Substring(0, 1);

            return passwd;
        }

        /// <summary>
        /// 字体串转换为二进制流
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static byte[] GetBytes(string str)
        {
            return System.Text.Encoding.ASCII.GetBytes(str);

            //int len = str.Length;
            //byte[] _byte = new byte[len - 1];
            //ASCIIEncoding asc = new ASCIIEncoding();
            //_byte = asc.GetBytes(str);
            //return _byte;
        }
        #endregion

        #region DES加密解密算法
        /// <summary>
        /// DES加密，向量随机生成
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns></returns>
        public static string DesEncrypt(string plaintext)
        {
            //随机生成8位向量
            string iv = GenerateRandom();
            byte[] rgbKey = System.Text.Encoding.ASCII.GetBytes("SolinArt");
            byte[] rgbIV = System.Text.Encoding.ASCII.GetBytes(iv);
            byte[] _data = System.Text.Encoding.UTF8.GetBytes(plaintext);

            System.Security.Cryptography.DESCryptoServiceProvider _des = new System.Security.Cryptography.DESCryptoServiceProvider();
            System.Security.Cryptography.ICryptoTransform _encrypt = _des.CreateEncryptor(rgbKey, rgbIV);
            byte[] _result = _encrypt.TransformFinalBlock(_data, 0, _data.Length);
            _encrypt.Dispose();
            _des.Clear();

            string result = BitConverter.ToString(_result);
            result = iv + result.Replace("-", "");

            //首尾各加16位共计32位的MD5校验码
            #region 32位的MD5校验码
            //string _md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(result, "MD5");
            string _md5 = Md5(result);
            result = string.Format("{0}{1}{2}"
                , _md5.Substring(0, 16)
                , result
                , _md5.Substring(16)
                );
            #endregion

            return result;
        }

        /// <summary>
        /// DES解密
        /// </summary>
        public static string DesDecrypt(string encrypted)
        {
            //小于40位时，不是有效的加密串
            if (encrypted.Length <= 40)
                return "";
            else if (encrypted.Length % 2 != 0)
                return "";  //非偶数位不是有效的加密串

            //32位MD5校验码
            string _md5 = encrypted.Substring(0, 16);
            _md5 += encrypted.Substring(encrypted.Length - 16);
            //移除MD5
            encrypted = encrypted.Substring(16);
            encrypted = encrypted.Substring(0, encrypted.Length - 16);

            //进行MD5验证是否被修改
            //if (_md5 != System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(encrypted, "MD5"))
            if (_md5 != Md5(encrypted))
            {
                return "";
            }

            //截取，移除前8位向量长度
            string iv = encrypted.Substring(0, 8);
            //移除向量值
            encrypted = encrypted.Substring(8);

            //32位整型转换器
            System.ComponentModel.Int32Converter _int32 = new System.ComponentModel.Int32Converter();
            //字符串转换成数组
            byte[] _datas = new byte[encrypted.Length / 2];
            for (int i = 0; i < _datas.Length; i++)
            {
                _datas[i] = Convert.ToByte(_int32.ConvertFromInvariantString("0x" + encrypted.Substring(i * 2, 2)));
            }

            byte[] rgbKey = System.Text.Encoding.ASCII.GetBytes("SolinArt");
            byte[] rgbIV = System.Text.Encoding.ASCII.GetBytes(iv);

            System.Security.Cryptography.DESCryptoServiceProvider _des = new System.Security.Cryptography.DESCryptoServiceProvider();
            System.Security.Cryptography.ICryptoTransform _encrypt = _des.CreateDecryptor(rgbKey, rgbIV);
            byte[] _result = _encrypt.TransformFinalBlock(_datas, 0, _datas.Length);
            _encrypt.Dispose();
            _des.Clear();
            return System.Text.Encoding.UTF8.GetString(_result);
        }
        /// <summary>
        /// 随机生成8位由0－9、A-F的向量值
        /// </summary>
        protected static string GenerateRandom()
        {
            string vChar = "0123456789ABCDEF";
            int number;
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                number = random.Next();
                result += vChar.Substring(number % vChar.Length, 1);
            }
            return result;
        }
        #endregion
    }
}
