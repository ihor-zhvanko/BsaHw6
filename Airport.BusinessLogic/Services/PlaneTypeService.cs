using System;
using System.Collections.Generic;
using AutoMapper;

using Airport.Common.DTOs;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

namespace Airport.BusinessLogic.Services
{
  public interface IPlaneTypeService : IService<PlaneTypeDTO>
  { }

  public class PlaneTypeService : BaseService<PlaneTypeDTO, PlaneType>, IPlaneTypeService
  {
    public PlaneTypeService(IUnitOfWork unitOfWork)
      : base(unitOfWork)
    { }
  }
}