using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Infrastructure.Extensions
{
    /// <summary>
    /// Class DoubleExtensions.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// to int
        /// </summary>
        /// <param name="value">double</param>
        /// <returns>int</returns>
        public static int ToInt(this double value)
        {
            return ((decimal)value).ToInt();
        }

    }
}
