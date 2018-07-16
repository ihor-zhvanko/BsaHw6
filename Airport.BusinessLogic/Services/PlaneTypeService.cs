using System;
using System.Collections.Generic;
using Airport.BusinessLogic.Models;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

using AutoMapper;

namespace Airport.BusinessLogic.Services
{
  public interface IPlaneTypeService : IService<PlaneTypeModel>
  { }

  public class PlaneTypeService : BaseService<PlaneTypeModel, PlaneType>, IPlaneTypeService
  {
    public PlaneTypeService(IUnitOfWork unitOfWork)
      : base(unitOfWork)
    { }
  }
}