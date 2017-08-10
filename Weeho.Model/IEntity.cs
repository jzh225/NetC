using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Model
{
    /// <summary>
    /// 实体基础类
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        /// <value>The identifier.</value>
        int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <value>The created time.</value>
        DateTime CreatedTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <value>The updated time.</value>
        DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        bool IsDeleted { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        /// <value>The row version.</value>
        byte[] RowVersion { get; set; }
    }
}
