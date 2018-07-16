using System;
using System.Collections.Generic;
using Airport.BusinessLogic.Models;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

using AutoMapper;

namespace Airport.BusinessLogic.Services
{
  public interface IPilotService : IService<PilotModel>
  { }

  public class PilotService : BaseService<PilotModel, Pilot>, IPilotService
  {
    public PilotService(IUnitOfWork unitOfWork)
      : base(unitOfWork)
    { }
  }
}