using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Weeho.Common.Constant;
using Weeho.Infrastructure;

namespace Weeho.Common.Helper
{
    public class Base64StringToImage
    {
        public static string Base64ToImg(string FileString)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(FileString);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                string newFileName = Code.GenerateTransactionId() + ".jpg";
                string dirPath = Path.Combine(HttpContext.Current.Server.MapPath(ConfigurationSetting.ImagePath));
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                string path = Path.Combine(dirPath, newFileName);
                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                ms.Close();
                //if (File.Exists(txtFileName))
                //{
                //    File.Delete(txtFileName);
                //}
                return ConfigurationSetting.ImagePath + "/" + newFileName;
            }
            catch (Exception ex)
            {
                return "Base64StringToImage 转换失败\nException：" + ex.Message;
            }
        }
    }
}
