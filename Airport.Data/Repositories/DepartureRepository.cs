using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Airport.Common.Exceptions;

using Airport.Data.MockData;
using Airport.Data.Models;
using Airport.Data.DatabaseContext;

namespace Airport.Data.Repositories
{
  public class DepartureRepository : Repository<Departure>, IRepository<Departure>
  {
    private AirportDbContext _dbContext;
    public DepartureRepository(AirportDbContext dbContext) : base(dbContext)
    {
      _dbContext = dbContext;
    }

    public override IEnumerable<Departure> Details(Expression<Func<Departure, bool>> filter = null)
    {
      var departures = _dbContext.Departure
        .Include(x => x.Plane)
        .Include(x => x.Flight)
        .Include(x => x.Crew);
      if (filter != null)
        return departures.Where(filter);

      return departures;
    }
  }
}