using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Weeho.Model;
using Weeho.Dac;
using Weeho.Caching.Redis;

namespace Weeho.Biz
{
    /// <summary>
    /// 业务逻辑基础类
    /// </summary>
    /// <typeparam name="TEntity">BaseEntity派生类</typeparam>
    /// <seealso cref="TradingPlatform.EF.Core.Service.IService{TEntity}" />
    public class BaseService<TEntity> : IService<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository<TEntity> _repository;
        private readonly IRedisCachingService _redisCachingService;
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{TEntity}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BaseService(IRepository<TEntity> repository, IRedisCachingService redisCachingService)
        {
            _repository = repository;
            _redisCachingService = redisCachingService;
        }
        #region 查询
        /// <summary>
        /// 按条件获取单条数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>TEntity实体</returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return _repository.Get(where);
        }
        /// <summary>
        /// 按条件获取单条数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>TEntity实体</returns>
        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            return _repository.GetAsync(where);
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="orderByExpressions">排序参数</param>
        /// <returns>TEntity类型的所有数据</returns>
        public virtual IEnumerable<TEntity> GetAll(params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            return _repository.GetAll(orderByExpressions);
        }
        /// <summary>
        /// 获取所有数据-异步
        /// </summary>
        /// <returns>TEntity类型的所有数据</returns>
        public virtual Task<List<TEntity>> GetAllAsync(params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            return _repository.GetAllAsync(orderByExpressions);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderByExpressions">排序参数</param>
        /// <returns>数据集合</returns>
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where,
            params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            return _repository.GetMany(where, orderByExpressions);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual Tuple<int, int, IEnumerable<TEntity>> GetManyEmpty()
        {
            return new Tuple<int, int, IEnumerable<TEntity>>(0, 0, new List<TEntity>());
        }
        /// <summary>
        /// 查询数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderByExpressions">The order by expressions.</param>
        /// <returns>数据集合</returns>
        public virtual Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where,
            params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            return _repository.GetManyAsync(where, orderByExpressions);
        }
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <param name="orderByExpressions">排序参数</param>
        /// <returns>数据集合</returns>
        public virtual Tuple<int, int, IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> where,
            Paging paging,
            params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            return _repository.GetMany(where, paging, orderByExpressions);
        }

        /// <summary>
        /// 分页查询数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <param name="orderByExpressions">The order by expressions.</param>
        /// <returns>数据集合</returns>
        public virtual Tuple<int, int, Task<List<TEntity>>> GetManyAsync(Expression<Func<TEntity, bool>> where,
            Paging paging,
            params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            return _repository.GetManyAsync(where, paging, orderByExpressions);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> where)
        {
            return _repository.Count(where);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual int Sum(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> selector)
        {
            return _repository.Sum(where, selector);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual decimal Sum(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, decimal>> selector)
        {
            return _repository.Sum(where, selector);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual float Sum(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, float>> selector)
        {
            return _repository.Sum(where, selector);
        }
        public virtual double Avg(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> selector)
        {
            return _repository.Avg(where, selector);
        }
        public virtual decimal Avg(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, decimal>> selector)
        {
            return _repository.Avg(where, selector);
        }
        public virtual float Avg(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, float>> selector)
        {
            return _repository.Avg(where, selector);
        }
        public virtual int Max(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> selector)
        {
            return _repository.Max(where, selector);
        }
        public virtual TEntity First(Expression<Func<TEntity, bool>> where, params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            return _repository.First(where, orderByExpressions);
        }
        #endregion

        #region 更新

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        /// <returns>影响的行数</returns>
        public virtual int Insert(TEntity entity)
        {
            _repository.Insert(entity);
            return _repository.Commit();
        }
        /// <summary>
        /// 插入单条数据-异步
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        /// <returns>影响的行数</returns>
        public virtual Task<int> InsertAsync(TEntity entity)
        {
            _repository.Insert(entity);
            return _repository.CommitAsync();
        }
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entities">The entity.</param>
        /// <returns>System.Int32.</returns>
        public virtual int Insert(params TEntity[] entities)
        {
            _repository.Insert(entities);
            return _repository.Commit();
        }
        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entities">The entity.</param>
        /// <returns>Task&lt;System.Int32&gt;.</returns>
        public virtual Task<int> InsertAsync(params TEntity[] entities)
        {
            _repository.Insert(entities);
            return _repository.CommitAsync();
        }
        /// <summary>
        /// 修改单条数据
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        /// <returns>影响的行数</returns>
        public virtual int Update(TEntity entity)
        {
            _repository.Update(entity);
            return _repository.Commit();
        }
        /// <summary>
        /// 修改单条数据-异步
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        /// <returns>影响的行数</returns>
        public virtual Task<int> UpdateAsync(TEntity entity)
        {
            _repository.Update(entity);
            return _repository.CommitAsync();
        }
        /// <summary>
        /// Updates the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="action">The action.</param>
        /// <returns>System.Int32.</returns>
        public virtual int Update(Expression<Func<TEntity, bool>> @where, Action<TEntity> action)
        {
            _repository.Update(@where, action);
            return _repository.Commit();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual Task<int> UpdateAsync(Expression<Func<TEntity, bool>> @where, Action<TEntity> action)
        {
            _repository.Update(@where, action);
            return _repository.CommitAsync();
        }
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="id">实体编号</param>
        /// <returns>影响的行数</returns>
        public virtual int Delete(int id)
        {
            _repository.Delete(id);
            return _repository.Commit();
        }
        /// <summary>
        /// 删除单条数据-异步
        /// </summary>
        /// <param name="id">实体编号</param>
        /// <returns>影响的行数</returns>
        public virtual Task<int> DeleteAsync(int id)
        {
            _repository.Delete(id);
            return _repository.CommitAsync();
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        /// <returns>影响的行数</returns>
        public virtual int Delete(TEntity entity)
        {
            _repository.Delete(entity);
            return _repository.Commit();
        }
        /// <summary>
        /// 删除单条数据-异步
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        /// <returns>影响的行数</returns>
        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            _repository.Delete(entity);
            return _repository.CommitAsync();
        }
        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>影响的行数</returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> where)
        {
            _repository.Delete(where);
            return _repository.Commit();
        }
        /// <summary>
        /// 按条件删除数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>影响的行数</returns>
        public virtual Task<int> DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            _repository.Delete(where);
            return _repository.CommitAsync();
        }

        public virtual int Delete(Expression<Func<TEntity, bool>> @where, Action<TEntity> action)
        {
            _repository.Delete(@where, action);
            return _repository.Commit();
        }
        public virtual Task<int> DeleteAsync(Expression<Func<TEntity, bool>> @where, Action<TEntity> action)
        {
            _repository.Delete(@where, action);
            return _repository.CommitAsync();
        }
        #endregion
    }
}
