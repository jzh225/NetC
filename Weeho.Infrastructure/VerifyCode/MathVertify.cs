// ***********************************************************************
// Assembly         : Infrastructure
// Author           : 
// Created          : 11-14-2016
//
// Last Modified By : 
// Last Modified On : 11-14-2016
// ***********************************************************************
// <copyright file="MathVertify.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weeho.Infrastructure.VerifyCode
{
    /// <summary>
    /// 创建数学计算验证码
    /// </summary>
    /// <seealso cref="Infrastructure.VerifyCode.AbstractVertify" />
    public class MathVertify : AbstractVertify
    {

        /// <summary>
        /// The time
        /// </summary>
        private int   time = -1  // 第几次调用GetChar方法，以便生成不同的值
                                 /// <summary>
                                 /// The para1
                                 /// </summary>
                    , para1 = 0  // 第一个值
                                 /// <summary>
                                 /// The para2
                                 /// </summary>
                    , para2 = 0 ;// 第二个值
        /// <summary>
        /// The method
        /// </summary>
        private string method = null; //加法、减法
        /// <summary>
        /// The number
        /// </summary>
        private Num num;

        /// <summary>
        /// 创建数学计算验证码
        /// </summary>
        /// <param name="num">The number.</param>
        public MathVertify(Num num)
        {
            this.num = num;
        }

        /// <summary>
        /// 重载 获取验证码结果的方法
        /// </summary>
        /// <value>The result.</value>
        public override string result
        {
            get { return GetResult().ToString(); }
        }

        /// <summary>
        /// 重载 获取单个字符的方法
        /// </summary>
        /// <returns>System.String.</returns>
        public override string GetChar()
        {
            time++;
            switch (time)
            {
                case 0:
                    para1 = (R.Next((int)num));
                    return para1.ToString();
                case 2:
                    para2 = (R.Next((int)num));
                    return para2.ToString();
                case 1:
                    method = GetOperator();
                    return method;
                default: return "";
            }
        }
        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetOperator()
        {
            if (R.Next(2) == 1)
            {
                return "加";
            }
            else
            {
                return "减";
            }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int GetResult()
        {
            if (method == "加")
            {
                return para1 + para2;
            }
            else
            {
                return para1 - para2;
            }
        }
    }
}
