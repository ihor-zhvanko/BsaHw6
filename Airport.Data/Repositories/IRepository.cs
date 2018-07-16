using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Airport.Data.Repositories
{
  public interface IRepository<TEntity>
  {
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);

    TEntity Create(TEntity entity, string createdBy = null);

    TEntity Update(TEntity entity, string modifiedBy = null);

    void Delete(TEntity entity);

    TEntity Get(int id);
    void Delete(int id);
    IEnumerable<TEntity> Details(Expression<Func<TEntity, bool>> filter = null);
  }
}