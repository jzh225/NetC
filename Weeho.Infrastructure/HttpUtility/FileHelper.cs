using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.HttpUtility
{
    /// <summary>
    /// Class FileHelper.
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 根据完整文件路径获取FileStream
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>FileStream.</returns>
        public static FileStream GetFileStream(string fileName)
        {
            FileStream fileStream = null;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                fileStream = new FileStream(fileName, FileMode.Open);
            }
            return fileStream;
        }

        /// <summary>
        /// 从Url下载文件
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="fullFilePathAndName">Full name of the file path and.</param>
        public static void DownLoadFileFromUrl(string url, string fullFilePathAndName)
        {
            using (FileStream fs = new FileStream(fullFilePathAndName, FileMode.OpenOrCreate))
            {
                HttpGetUtility.Download(url, fs);
                fs.Flush(true);
            }
        }
    }
}
