using System;
using System.Collections.Generic;
using Airport.BusinessLogic.Models;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

using AutoMapper;

namespace Airport.BusinessLogic.Services
{
  public interface IFlightService : IService<FlightModel>
  { }

  public class FlightService : BaseService<FlightModel, Flight>, IFlightService
  {
    public FlightService(IUnitOfWork unitOfWork)
      : base(unitOfWork)
    { }
  }
}