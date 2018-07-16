using System;
using System.Threading.Tasks;
using Airport.Data.Repositories;
using Airport.Data.Models;

namespace Airport.Data.UnitOfWork
{
  public interface IUnitOfWork
  {
    IRepository<TEntity> Set<TEntity>() where TEntity : Entity;
    int SaveChanges();
    Task<int> SaveChangesAsync();
  }
}