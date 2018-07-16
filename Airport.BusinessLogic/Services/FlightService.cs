using System;
using System.Collections.Generic;
using AutoMapper;

using Airport.Common.DTOs;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

namespace Airport.BusinessLogic.Services
{
  public interface IFlightService : IService<FlightDTO>
  { }

  public class FlightService : BaseService<FlightDTO, Flight>, IFlightService
  {
    public FlightService(IUnitOfWork unitOfWork)
      : base(unitOfWork)
    { }
  }
}