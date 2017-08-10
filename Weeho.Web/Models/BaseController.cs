using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weeho.Biz.Interface;
using Weeho.Common.Constant;
using Weeho.Common.Filter;
using Weeho.DependencyResolver;
using Weeho.Infrastructure;
using Weeho.Infrastructure.Extensions;
using Weeho.Model.Entity;
using Weeho.Web.Areas.Admin;

namespace Weeho.Models
{
    /// <summary>
    /// Class BaseController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [ExceptionFilter]
    public class BaseController : Controller
    {
        public readonly ISysLogService _sysLogService;
        public bool AdminController { get; set; }
        public BaseController()
        {
            _sysLogService = IOC.Resolve<ISysLogService>();
        }

        #region help method
        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public string GetQueryString(string name, string defaultValue = "")
        {
            return Request.GetQueryString(name, defaultValue);
        }
        /// <summary>
        /// Gets the query string values.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String[].</returns>
        public string[] GetQueryStringValues(string name)
        {
            return Request.GetQueryStringValues(name);
        }
        /// <summary>
        /// Gets the query string values.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String[].</returns>
        public string[] GetFormValues(string name)
        {
            return Request.GetFormValues(name);
        }
        /// <summary>
        /// Gets the query int.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public int GetQueryInt(string name, int defaultValue = 0)
        {
            return Request.GetQueryInt(name, defaultValue);
        }
        public decimal GetQueryDecimal(string name, decimal defaultValue = 0)
        {
            return Request.GetQueryDecimal(name, defaultValue);
        }
        /// <summary>
        /// Gets the query long.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        public long GetQueryLong(string name, long defaultValue = 0)
        {
            return Request.GetQueryLong(name, defaultValue);
        }
        /// <summary>
        /// Gets the query date time.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>DateTime.</returns>
        public DateTime GetQueryDateTime(string name, DateTime defaultValue = new DateTime())
        {
            return Request.GetQueryString(name).ToDateTime(defaultValue);
        }
        /// <summary>
        /// Gets the form string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public string GetFormString(string name, string defaultValue = "")
        {
            return Request.GetFormString(name, defaultValue);
        }
        /// <summary>
        /// Gets the form check.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool GetFormCheck(string name)
        {
            return Request.GetFormString(name) == "on";
        }
        /// <summary>
        /// Gets the form int.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public int GetFormInt(string name, int defaultValue = 0)
        {
            return Request.GetFormInt(name, defaultValue);
        }
        /// <summary>
        /// Gets the form long.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.Int64.</returns>
        public long GetFormLong(string name)
        {
            return Request.GetFormLong(name);
        }
        /// <summary>
        /// Gets the form date time.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>DateTime.</returns>
        public DateTime GetFormDateTime(string name)
        {
            return Request.GetFormDateTime(name);
        }
        #endregion

        public JsonResult JsonError(string msg)
        {
            return Json(new
            {
                success = false,
                msg = msg
            });
        }
        public JsonResult JsonSuccess(string msg)
        {
            return Json(new
            {
                success = true,
                msg = msg
            });
        }
        public JsonResult JsonLoginTimeout()
        {
            return JsonError("登录超时，请重新登录！");
        }
        public virtual void Log(string content, LogType logType = LogType.Info)
        {
            _sysLogService.Insert(new SysLog
            {
                LogContent = content,
                LogType = (int)logType,
                LogIp = Request.GetUserIP(),
                LogUrl = Request.RawUrl
            });
        }
        public virtual void LogError(string content)
        {
            Log(content, LogType.Error);
        }
        public virtual void LogWarning(string content)
        {
            Log(content, LogType.Warning);
        }

        public virtual Tuple<bool, string> UploadPicture(HttpPostedFileBase picture)
        {
            if (picture != null && picture.ContentLength > 0)
            {
                if (picture.ContentLength > (1024 * 1024*15))
                {
                    return new Tuple<bool, string>(false, "图片超过15Mb");
                }
                if (!new[] { "image/jpg","image/jpeg", "image/gif", "image/bmp", "image/x-icon", "image/png" }
                .Any(m => m == picture.ContentType))
                {
                    return new Tuple<bool, string>(false, "格式错误，请选择正确的图片格式：jpg、jpeg、gif、bmp、png");
                }
                var VirtualPath = string.Format("{0}{1}", ConfigurationSetting.ImgVirtual, DateTime.Now.ToString("yyyyMMdd"));
                var fileName = Path.GetExtension(picture.FileName);
                var newFileName = string.Format("{0}{1}",Code.GenerateTransactionId(), fileName);
                var filepath = string.Format("{0}/{1}", ConfigurationSetting.ImagePath, DateTime.Now.ToString("yyyyMMdd"));
                //string dirPath = filepath;
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                string path = Path.Combine(filepath, newFileName);
                picture.SaveAs(path);
                 
                return new Tuple<bool, string>(true, VirtualPath + "/"+ newFileName);
            }
            return new Tuple<bool, string>(false, "文件不存在");
        }

        public virtual ActionResult ExportToExcel(object dataSource,string filename="excel")
        {
            GridView gv = new GridView();
            gv.DataSource = dataSource;
            gv.DataBind();
            Response.ClearContent();
            Response.Clear();
            Response.Charset = "UTF8";
            Response.ContentEncoding = Encoding.UTF8;
            Response.AddHeader("content-disposition", "attachment; filename="+filename+".xls");Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write("<meta http-equiv=\"content-type\" content=\"application/vnd.ms-excel; charset=utf-8\"/>"+sw.ToString());
            Response.Flush();
            Response.End();

            return Content("");
        }

        #region 获取用户真实IP
        /// <summary>
        /// 获取用户真实【透析nginx】
        /// </summary>
        public string UserIP
        {
            get
            {
                string userIp = System.Web.HttpContext.Current.Request.UserHostAddress;

                if (System.Web.HttpContext.Current.Request.Headers.Get("USER_REMOTE_HOST") != null && !string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.Headers.Get("USER_REMOTE_HOST")))
                {
                    userIp = System.Web.HttpContext.Current.Request.Headers.Get("USER_REMOTE_HOST");
                }
                else if (System.Web.HttpContext.Current.Request.ServerVariables.Get("REMOTE_ADDR") != null && !string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables.Get("REMOTE_ADDR")))
                {
                    userIp = System.Web.HttpContext.Current.Request.ServerVariables.Get("REMOTE_ADDR");
                }
                return userIp;
            }
        }
        #endregion
    }
}
