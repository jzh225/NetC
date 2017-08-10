using System;
using System.Xml;

namespace Weeho.Logon.Admin
{
    /// <summary>
    /// 把XML的类型转换为model
    /// </summary>
    class XmlToModel
    {
        /// <summary>
        /// 把XML转换成登录的数据实体
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static UserLoginData ToUser(string xml)
        {
            UserLoginData userData = null;

            if (!string.IsNullOrEmpty(xml))
            {
                try
                {
                    userData = new UserLoginData();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);

                    userData.Id = Convert.ToInt32(xmlDoc.SelectSingleNode("user/id").InnerText);
                    //userData.UserGuid = new Guid(xmlDoc.SelectSingleNode("user/guid").InnerText);
                    userData.LoginName = xmlDoc.SelectSingleNode("user/loginName").InnerText;
                    userData.Password = xmlDoc.SelectSingleNode("user/password").InnerText;
                    //userData.Mobile = xmlDoc.SelectSingleNode("user/mobile").InnerText;
                    //userData.Email = xmlDoc.SelectSingleNode("user/email").InnerText;
                    userData.NickName = xmlDoc.SelectSingleNode("user/nicknName").InnerText;
                    //userData.Photo = xmlDoc.SelectSingleNode("user/photo").InnerText;
                    //userData.Gender = Convert.ToInt32(xmlDoc.SelectSingleNode("user/gender").InnerText);
                    userData.Status = Convert.ToBoolean(xmlDoc.SelectSingleNode("user/status").InnerText);
                    userData.LoginIp = xmlDoc.SelectSingleNode("user/loginIp").InnerText;
                    userData.IsAdmin = Convert.ToBoolean(xmlDoc.SelectSingleNode("user/isAdmin").InnerText);
                    try { userData.LoginDate = Convert.ToDateTime(xmlDoc.SelectSingleNode("user/loginDate").InnerText); }
                    catch { userData.LoginDate = DateTime.Now; }
                }
                catch { }
            }

            return userData;
        }
    }
}
