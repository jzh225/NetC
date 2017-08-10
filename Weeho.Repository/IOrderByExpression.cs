using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeho.Model;

namespace Weeho.Dac
{
    public interface IOrderByExpression<TEntity> where TEntity : IEntity
    {
        IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query);
        IOrderedQueryable<TEntity> ApplyThenBy(IOrderedQueryable<TEntity> query);
    }
}
