using System;
using System.Collections.Generic;

using AutoMapper;

using Airport.Common.DTOs;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

namespace Airport.BusinessLogic.Services
{
  public interface IPilotService : IService<PilotDTO>
  { }

  public class PilotService : BaseService<PilotDTO, Pilot>, IPilotService
  {
    public PilotService(IUnitOfWork unitOfWork)
      : base(unitOfWork)
    { }
  }
}