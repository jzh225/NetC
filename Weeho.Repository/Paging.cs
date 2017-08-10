using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Dac
{
    /// <summary>
    /// 翻页参数类
    /// </summary>
    public class Paging
    {
       
        /// <summary>
        /// 当前页
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex { get; set; } = 1;
        
        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize { get; set; } = 15;
       
        /// <summary>
        /// Gets the skip count.
        /// </summary>
        /// <value>The skip count.</value>
        public int SkipCount
        {
            get
            {
                if (PageIndex <= 0)
                    PageIndex = 1;
                return (PageIndex - 1) * PageSize;
            }
        }
        /// <summary>
        /// Gets or sets the record count.
        /// </summary>
        /// <value>The record count.</value>
        public int RecordCount { get; set; }

        /// <summary>
        /// Gets the page count.
        /// </summary>
        /// <value>The page count.</value>
        public int PageCount
        {
            get
            {
                return RecordCount % PageSize == 0 ? RecordCount / PageSize : (RecordCount / PageSize + 1);
            }
        }
    }
}
