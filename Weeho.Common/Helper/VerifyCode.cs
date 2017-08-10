using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Common.Constant;
using Weeho.Infrastructure;
using Weeho.Infrastructure.Extensions;
using Weeho.Infrastructure.VerifyCode;

namespace Weeho.Common.Helper
{
    /// <summary>
    /// Class VerifyCode.
    /// </summary>
    public class VerifyCode
    {
        /// <summary>
        /// Charses the vertify.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public static byte[] CharsVertify()
        {
            AbstractVertify vertify = new CharsVertify(CharSet.Alphas);
            System.IO.MemoryStream ms = vertify.Draw();
            SessionUtility.Set(ConfigurationKey.ValidateCode, vertify.result) ;

            return ms.ToArray();
        }

        /// <summary>
        /// Mathes the vertify.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        public static byte[] MathVertify()
        {
            AbstractVertify vertify = new MathVertify(Num.Single);
            System.IO.MemoryStream ms = vertify.Draw();
            SessionUtility.Set(ConfigurationKey.ValidateCode, vertify.result);
            return ms.ToArray();
        }


        /// <summary>
        /// Validates the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Validate(string code)
        {
            var vCode = SessionUtility.Get<string>(ConfigurationKey.ValidateCode);
            if (vCode.IsNullOrEmpty())
                return false;
            return vCode.ToLower() == code.ToLower();
        }
    }
}
