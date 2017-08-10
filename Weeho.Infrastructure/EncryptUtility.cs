using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace Weeho.Infrastructure
{
    /// <summary>
    /// Class EncryptUtility.
    /// </summary>
    public class EncryptUtility
    {
        /// <summary>
        /// The key
        /// </summary>
        public static string Key = "huab4ao6";
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="pToEncrypt">The p to encrypt.</param>
        /// <returns>System.String.</returns>
        public static string Encrypt(string pToEncrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中
            //原来使用的UTF8编码，我改成Unicode编码了，不行
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            //byte[]  inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt); 

            //建立加密对象的密钥和偏移量
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            //使得输入密码必须输入英文文本
            des.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(Key);
            //创建其支持存储区为内存的流
            MemoryStream ms = new MemoryStream();
            //将数据流链接到加密转换的流
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //Write  the  byte  array  into  the  crypto  stream 
            //(It  will  end  up  in  the  memory  stream) 
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            //用缓冲区的当前状态更新基础数据源或储存库，随后清除缓冲区
            cs.FlushFinalBlock();
            //Get  the  data  back  from  the  memory  stream,  and  into  a  string 
            byte[] EncryptData = (byte[])ms.ToArray();
            return System.Convert.ToBase64String(EncryptData, 0, EncryptData.Length);
        }
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="pToDecrypt">The p to decrypt.</param>
        /// <returns>System.String.</returns>
        public static string Decrypt(string pToDecrypt)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //Put  the  input  string  into  the  byte  array 
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);

            //建立加密对象的密钥和偏移量，此值重要，不能修改
            des.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(Key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush  the  data  through  the  crypto  stream  into  the  memory  stream 
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>System.String.</returns>
        public static string EncodeBase64(string code)
        {
            string encode = "";
            byte[] bytes = Encoding.Default.GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>System.String.</returns>
        public static string DecodeBase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.Default.GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }


        public static string MD5(string code)
        {
            byte[] result = Encoding.Default.GetBytes(code);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output);
        }
    }
}
