using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weeho.Model
{
    /// <summary>
    /// 数据上下文接口
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// 提交更新
        /// </summary>
        /// <returns>影响的行数</returns>
        int Commit();
        /// <summary>
        /// 提交更新-异步
        /// </summary>
        /// <returns>影响的行数</returns>
        Task<int> CommitAsync();

    }
}
