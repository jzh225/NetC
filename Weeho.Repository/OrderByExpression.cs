using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Weeho.Infrastructure.Extensions;
using Weeho.Model;

namespace Weeho.Dac
{
    public class OrderByExpression<TEntity, TOrderBy> : IOrderByExpression<TEntity>
    where TEntity : IEntity
    {
        //private Expression<Func<TEntity, TOrderBy>> _expression;
        private bool _descending;
        private string _propertyName;
        public OrderByExpression(Expression<Func<TEntity, object>> expression, bool descending = false)
        {
            _propertyName = expression.GetPropertyName();
            _descending = descending;

        }
        public OrderByExpression(string propertyName, bool descending = false)
        {
            _propertyName = propertyName;
            _descending = descending;

        }
        public IOrderedQueryable<TEntity> ApplyOrderBy(
            IQueryable<TEntity> query)
        {
            //if (_descending)
            //    //return query.OrderByDescending(_expression);
            //    return OrderBy<TEntity>(query);
            //else
            //    return query.OrderBy(_expression);

            return OrderBy(query);
        }

        public IOrderedQueryable<TEntity> ApplyThenBy(
            IOrderedQueryable<TEntity> query)
        {
            //if (_descending)
            //    return query.ThenByDescending(_expression);
            //else
            //    return query.ThenBy(_expression);
            return ThenBy(query);
        }
        private IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> query)
        {
            ParameterExpression[] typeParams = new ParameterExpression[] { Expression.Parameter(typeof(TEntity), "") };

            System.Reflection.PropertyInfo pi = typeof(TEntity).GetProperty(_propertyName);

            return (IOrderedQueryable<TEntity>)query.Provider.CreateQuery(
            Expression.Call(
             typeof(Queryable),
           _descending ? "OrderByDescending" : "OrderBy",
             new Type[] { typeof(TEntity), pi.PropertyType },
             query.Expression,
             Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
            );
        }
        private IOrderedQueryable<TEntity> ThenBy(IQueryable<TEntity> query)
        {
            ParameterExpression[] typeParams = new ParameterExpression[] { Expression.Parameter(typeof(TEntity), "") };

            System.Reflection.PropertyInfo pi = typeof(TEntity).GetProperty(_propertyName);

            return (IOrderedQueryable<TEntity>)query.Provider.CreateQuery(
            Expression.Call(
             typeof(Queryable),
           _descending ? "ThenByDescending" : "ThenBy",
             new Type[] { typeof(TEntity), pi.PropertyType },
             query.Expression,
             Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
            );
        }




       
    }
}
