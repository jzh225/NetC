// ***********************************************************************
// Assembly         : Infrastructure
// Author           : 
// Created          : 11-14-2016
//
// Last Modified By : 
// Last Modified On : 11-14-2016
// ***********************************************************************
// <copyright file="GifVertify.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using Weeho.Infrastructure.VerifyCode.Gif;

namespace Weeho.Infrastructure.VerifyCode
{

    /// <summary>
    /// Gif动画验证码
    /// </summary>
    /// <seealso cref="Infrastructure.VerifyCode.AbstractVertify" />
    public class GifVertify :AbstractVertify
    {
        /// <summary>
        /// The result
        /// </summary>
        string _result = "";
        /// <summary>
        /// Initializes a new instance of the <see cref="GifVertify"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public GifVertify(HttpRequestBase request) 
        {
            string currentPath = request.PhysicalApplicationPath;
            string[] mFrames = new string[] { currentPath + @"Images\other\1.jpg", currentPath + @"Images\other\3.jpg", currentPath + @"Images\other\2.jpg" };

            string verPath = currentPath + @"Images\index\ver.gif";

            FileInfo file = new FileInfo(verPath);

            if (file.Exists)
            {
                file.Delete();
            }

            AnimatedGifEncoder mEncoder = new AnimatedGifEncoder();
            mEncoder.Start(verPath);
            mEncoder.SetDelay(300);
            mEncoder.SetRepeat(0);

            for (int i = 0; i < mFrames.Length; i++)
            {
                mEncoder.AddFrame(Image.FromFile(mFrames[i]));
            }

            mEncoder.Finish();
            _result = mFrames.Length.ToString();
                  
        }
        /// <summary>
        /// 获取验证结果
        /// </summary>
        /// <value>The result.</value>
        public override string result
        {
            get { return _result; }
        }
        /// <summary>
        /// 重写添加字符的方法
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetChar()
        {
            return "";
        }
    }

}