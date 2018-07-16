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
  public class PlaneRepository : Repository<Plane>, IRepository<Plane>
  {
    private AirportDbContext _dbContext;
    public PlaneRepository(AirportDbContext dbContext) : base(dbContext)
    {
      _dbContext = dbContext;
    }

    public override IEnumerable<Plane> Details(Expression<Func<Plane, bool>> filter = null)
    {
      var planes = _dbContext.Plane
        .Include(x => x.PlaneType);
      if (filter != null)
        return planes.Where(filter);

      return planes;
    }
  }
}