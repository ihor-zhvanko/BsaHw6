using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.Data.MockData;
using Airport.Data.Repositories;
using Airport.Data.Models;

using Airport.Data.DatabaseContext;

namespace Airport.Data.UnitOfWork
{
  public class UnitOfWork : IUnitOfWork
  {
    protected readonly AirportDbContext _dbContext;

    public UnitOfWork(AirportDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public int SaveChanges()
    {
      return _dbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
      return await _dbContext.SaveChangesAsync();
    }

    public IRepository<TEntity> Set<TEntity>() where TEntity : Entity
    {
      var entityType = typeof(TEntity);
      if (entityType == typeof(Airhostess))
      {
        return (IRepository<TEntity>)new AirhostessRepository(_dbContext);
      }
      else if (entityType == typeof(Crew))
      {
        return (IRepository<TEntity>)new CrewRepository(_dbContext);
      }
      else if (entityType == typeof(Departure))
      {
        return (IRepository<TEntity>)new DepartureRepository(_dbContext);
      }
      else if (entityType == typeof(Flight))
      {
        return (IRepository<TEntity>)new FlightRepository(_dbContext);
      }
      else if (entityType == typeof(Pilot))
      {
        return (IRepository<TEntity>)new PilotRepository(_dbContext);
      }
      else if (entityType == typeof(Plane))
      {
        return (IRepository<TEntity>)new PlaneRepository(_dbContext);
      }
      else if (entityType == typeof(PlaneType))
      {
        return (IRepository<TEntity>)new PlaneTypeRepository(_dbContext);
      }
      else if (entityType == typeof(Ticket))
      {
        return (IRepository<TEntity>)new TicketRepository(_dbContext);
      }

      throw new NotImplementedException($"No repository for: {entityType.Name}");
    }
  }
}