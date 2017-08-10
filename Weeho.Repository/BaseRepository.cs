using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Weeho.Model;

namespace Weeho.Dac
{
    /// <summary>
    /// 数据仓储接类
    /// </summary>
    public class BaseRepository<TEntity> : Disposable, IRepository<TEntity> where TEntity : class, IEntity
    {

        /// <summary>
        /// Gets or sets the include entity.
        /// </summary>
        /// <value>The include entity.</value>
        public List<Expression<Func<TEntity, object>>> IncludeEntity { get; set; }

        /// <summary>
        /// 数据库上下文
        /// </summary>
        /// <value>The data context.</value>
        public DbContext _dataContext { get; set; }
        /// <summary>
        /// 数据库实体集合
        /// </summary>
        /// <value>The dbset.</value>
        public DbSet<TEntity> Dbset
        {
            get
            {
                return _dataContext.Set<TEntity>();
            }
        }
        /// <summary>
        /// 数据库实体集合
        /// </summary>
        /// <value>The dbset.</value>
        public IQueryable<TEntity> Query
        {
            get
            {
                var _dbset = Dbset.AsQueryable();
                foreach (var entity in IncludeEntity)
                {
                    _dbset = _dbset.Include(entity);
                }
                return _dbset;
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public BaseRepository(DbContext dbContext)
        {
            _dataContext = dbContext;
            IncludeEntity = new List<Expression<Func<TEntity, object>>>();
        }

        #region 查询

        /// <summary>
        /// 按条件获取单条数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>TEntity实体</returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> @where)
        {
            return Query.Where(where).FirstOrDefault();
        }
        /// <summary>
        /// 按条件获取单条数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>TEntity实体</returns>
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> @where)
        {
            return await Query.Where(where).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>TEntity类型的所有数据</returns>
        public virtual IEnumerable<TEntity> GetAll(params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            var query = Query;
            for (int i = 0; i < orderByExpressions.Length; i++)
            {
                if (i == 0)
                {
                    query = orderByExpressions[i].ApplyOrderBy(query);
                }
                else
                {
                    query = orderByExpressions[i].ApplyThenBy((IOrderedQueryable<TEntity>)query);
                }
            }
            if (orderByExpressions.Length == 0)
            {
                query = query.OrderByDescending(m => m.Id);
            }
            return query.AsNoTracking().AsEnumerable();
        }
        /// <summary>
        /// 获取所有数据-异步
        /// </summary>
        /// <returns>TEntity类型的所有数据</returns>
        public virtual async Task<List<TEntity>> GetAllAsync(params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            var query = Query;
            for (int i = 0; i < orderByExpressions.Length; i++)
            {
                if (i == 0)
                {
                    query = orderByExpressions[i].ApplyOrderBy(query);
                }
                else
                {
                    query = orderByExpressions[i].ApplyThenBy((IOrderedQueryable<TEntity>)query);
                }
            }
            if (orderByExpressions.Length == 0)
            {
                query = query.OrderByDescending(m => m.Id);
            }
            return await query.AsNoTracking().ToListAsync();
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>数据集合</returns>
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            var query = Query;
            for (int i = 0; i < orderByExpressions.Length; i++)
            {
                if (i == 0)
                {
                    query = orderByExpressions[i].ApplyOrderBy(query);
                }
                else
                {
                    query = orderByExpressions[i].ApplyThenBy((IOrderedQueryable<TEntity>)query);
                }
            }
            if (orderByExpressions.Length == 0)
            {
                query = query.OrderByDescending(m => m.Id);
            }
            return query.Where(where).AsNoTracking().ToList();
        }
        /// <summary>
        /// 查询数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>数据集合</returns>
        public virtual async Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> @where, params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            var query = Query;
            for (int i = 0; i < orderByExpressions.Length; i++)
            {
                if (i == 0)
                {
                    query = orderByExpressions[i].ApplyOrderBy(query);
                }
                else
                {
                    query = orderByExpressions[i].ApplyThenBy((IOrderedQueryable<TEntity>)query);
                }
            }
            if (orderByExpressions.Length == 0)
            {
                query = query.OrderByDescending(m => m.Id);
            }
            return await query.Where(where).AsNoTracking().ToListAsync();
        }
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>数据集合</returns>
        public virtual Tuple<int, int, IEnumerable<TEntity>> GetMany(Expression<Func<TEntity, bool>> where,
            Paging paging,
            params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            if (paging == null)
            {
                paging = new Paging { PageIndex = 1, PageSize = int.MaxValue };
            }

            var query = Query;
            if (where != null)
            {
                query = query.Where(where);
            }
            for (int i = 0; i < orderByExpressions.Length; i++)
            {
                if (i == 0)
                {
                    query = orderByExpressions[i].ApplyOrderBy(query);
                }
                else
                {
                    query = orderByExpressions[i].ApplyThenBy((IOrderedQueryable<TEntity>)query);
                }
            }
            if (orderByExpressions.Length == 0)
            {
                query = query.OrderByDescending(m => m.Id);
            }
            paging.RecordCount = query.Count();
            var result = query.Skip(paging.SkipCount)
                    .Take(paging.PageSize).AsNoTracking().ToList();
            return new Tuple<int, int, IEnumerable<TEntity>>(paging.PageCount, paging.RecordCount, result);
        }
        /// <summary>
        /// 分页查询数据-异步
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="paging">分页信息</param>
        /// <returns>数据集合</returns>
        public virtual Tuple<int, int, Task<List<TEntity>>> GetManyAsync(Expression<Func<TEntity, bool>> where, Paging paging, params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            if (paging == null)
            {
                paging = new Paging { PageIndex = 1, PageSize = int.MaxValue };
            }

            var query = Query;
            if (where != null)
            {
                query = query.Where(where);
            }
            paging.RecordCount = query.Count();

            for (int i = 0; i < orderByExpressions.Length; i++)
            {
                if (i == 0)
                {
                    query = orderByExpressions[i].ApplyOrderBy(query);
                }
                else
                {
                    query = orderByExpressions[i].ApplyThenBy((IOrderedQueryable<TEntity>)query);
                }
            }
            if (orderByExpressions.Length == 0)
            {
                query = query.OrderByDescending(m => m.Id);
            }
            var result = query.Skip(paging.SkipCount)
                    .Take(paging.PageSize).AsNoTracking().ToListAsync();
            return new Tuple<int, int, Task<List<TEntity>>>(paging.PageCount, paging.RecordCount, result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> where)
        {
            return Query.Count(where);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual int Sum(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> selector)
        {
            return Query.Where(where).Select(selector).DefaultIfEmpty(0).Sum();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual decimal Sum(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, decimal>> selector)
        {
            return Query.Where(where).Select(selector).DefaultIfEmpty(0).Sum();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual float Sum(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, float>> selector)
        {
            return Dbset.Where(where).Select(selector).DefaultIfEmpty(0).Sum();
        }
        public virtual double Avg(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> selector)
        {
            return Dbset.Where(where).Select(selector).DefaultIfEmpty(0).Average();
        }
        public virtual decimal Avg(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, decimal>> selector)
        {
            return Dbset.Where(where).Select(selector).DefaultIfEmpty(0).Average();
        }
        public virtual float Avg(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, float>> selector)
        {
            return Dbset.Where(where).Select(selector).DefaultIfEmpty(0).Average();
        }
        public virtual int Max(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, int>> selector)
        {
            return Dbset.Where(where).Select(selector).DefaultIfEmpty(0).Max();
        }
        public virtual TEntity First(Expression<Func<TEntity, bool>> where, params OrderByExpression<TEntity, object>[] orderByExpressions)
        {
            var query = Query;
            for (int i = 0; i < orderByExpressions.Length; i++)
            {
                if (i == 0)
                {
                    query = orderByExpressions[i].ApplyOrderBy(query);
                }
                else
                {
                    query = orderByExpressions[i].ApplyThenBy((IOrderedQueryable<TEntity>)query);
                }
            }
            return query.FirstOrDefault();
        }
        #endregion

        #region 更新
        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        public virtual void Insert(TEntity entity)
        {
            Dbset.Add(entity);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public virtual void Insert(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                Dbset.Add(entity);
            }
        }
        /// <summary>
        /// 修改单条数据
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            Dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
        /// <summary>
        /// Updates the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="action">The action.</param>
        public virtual void Update(Expression<Func<TEntity, bool>> @where, Action<TEntity> action)
        {
            var objects = Dbset.Where(where);

            //objects.ForEach(entity =>
            //{
            //    Dbset.Attach(entity);
            //    var entityEntry = _dataContext.Entry(entity);
            //    action(entity);
            //    entityEntry.State = EntityState.Modified;
            //});
            foreach (TEntity entity in objects)
            {
                Dbset.Attach(entity);
                var entityEntry = _dataContext.Entry(entity);
                action(entity);
                entityEntry.State = EntityState.Modified;
            }
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="id">实体编号</param>
        /// <exception cref="System.Data.Entity.Core.ObjectNotFoundException">entity</exception>
        public virtual void Delete(int id)
        {
            var entity = Get(m => m.Id == id);
            if (entity == null)
                throw new ObjectNotFoundException("entity");
            Dbset.Remove(entity);
        }
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity">TEntity实体</param>
        public virtual void Delete(TEntity entity)
        {
            Dbset.Remove(entity);
        }
        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="where">查询条件</param>
        public virtual void Delete(Expression<Func<TEntity, bool>> @where)
        {
            var objects = Dbset.Where(where);
            //foreach (TEntity obj in objects)
            Dbset.RemoveRange(objects);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> @where, Action<TEntity> action)
        {
            var objects = Dbset.Where(where);

            foreach (TEntity entity in objects)
            {
                action(entity);
                Dbset.Remove(entity);
            }
        }
        #endregion
        /// <summary>
        /// Disposes the core.
        /// </summary>
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }

        /// <summary>
        /// 提交数据更改
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Commit()
        {
            return _dataContext.SaveChanges();
        }
        /// <summary>
        /// 提交数据更改-异步
        /// </summary>
        /// <returns>影响的行数</returns>
        public Task<int> CommitAsync()
        {
            return _dataContext.SaveChangesAsync();
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns>DbContextTransaction.</returns>
        public DbContextTransaction BeginTransaction()
        {
            return _dataContext.Database.BeginTransaction();
        }
    }
}
