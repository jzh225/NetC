using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Weeho.Model;

namespace Weeho.Dac
{
    /// <summary>
    /// 数据仓储基础接口
    /// </summary>
    /// <typeparam name="TEntity">BaseEntity派生类</typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        DbContext _dataContext { get; set; }


        #region 查询
        /// <summary>
        /// 按条件获取单条数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>TEntity实体</returns>
        TEntity Get(Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 按条件获取单条数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>TEntity实体</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>TEntity类型的所有数据</returns>
        IEnumerable<TEntity> GetAll(params OrderByExpression<TEntity, object>[] orderByExpressions);
        /// <summary>
        /// 获取所有数据-异步
        /// </summary>
        /// <returns>TEntity类型的所有数据</returns>
        Task<List<TEntity>> GetAllAsync(params OrderByExpression<TEntity, object>[] orderByExpressions);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>数据集合</returns>
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params OrderByExpression<TEntity, object>[] orderByExpressions);
        /// <summary>
        /// 查询数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>数据集合</returns>
        Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where, params OrderByExpression<TEntity, object>[] orderByExpressions);
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>数据集合</returns>
        Tuple<int, int, IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> where, Paging paging, params OrderByExpression<TEntity, object>[] orderByExpressions);
        /// <summary>
        /// 分页查询数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>数据集合</returns>
        Tuple<int, int, Task<List<TEntity>>> GetManyAsync(Expression<Func<TEntity, bool>> where, Paging paging, params OrderByExpression<TEntity, object>[] orderByExpressions);
        int Count(Expression<Func<TEntity, bool>> where);
        int Sum(Expression<Func<TEntity, bool>> where,Expression<Func<TEntity, int>> selector);
        decimal Sum(Expression<Func<TEntity, bool>> where,Expression<Func<TEntity, decimal>> selector);
        float Sum(Expression<Func<TEntity, bool>> where,Expression<Func<TEntity, float>> selector);
        double Avg(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> selector);
        decimal Avg(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, decimal>> selector);
        float Avg(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, float>> selector);

        int Max(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> selector);

        TEntity First(Expression<Func<TEntity, bool>> where, params OrderByExpression<TEntity, object>[] orderByExpressions);
        #endregion

        #region 更新
        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entities">The entity.</param>
        void Insert(params TEntity[] entities);
        /// <summary>
        /// 修改单条数据
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        void Update(TEntity entity);
        /// <summary>
        /// Updates the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="action">The action.</param>
        void Update(Expression<Func<TEntity, bool>> @where, Action<TEntity> action);
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="id">实体编号</param>
        void Delete(int id);
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        void Delete(TEntity entity);
        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="where">查询条件</param>
        void Delete(Expression<Func<TEntity, bool>> where);
        void Delete(Expression<Func<TEntity, bool>> @where, Action<TEntity> action);

        #endregion



        /// <summary>
        /// 提交数据更改
        /// </summary>
        /// <returns>影响的行数</returns>
        int Commit();
        /// <summary>
        /// 提交数据更改-异步
        /// </summary>
        /// <returns>影响的行数</returns>
        Task<int> CommitAsync();

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns>DbContextTransaction.</returns>
        DbContextTransaction BeginTransaction();
    }
}
