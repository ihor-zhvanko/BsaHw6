using System;
using System.Linq;
using System.Collections.Generic;
using Airport.BusinessLogic.Models;

using Airport.Common.Exceptions;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

using AutoMapper;

namespace Airport.BusinessLogic.Services
{
  public interface IPlaneService : IService<PlaneModel>
  {
    IList<PlaneDetails> GetAllDetails();
    PlaneDetails GetDetails(int id);
  }

  public class PlaneService : BaseService<PlaneModel, Plane>, IPlaneService
  {
    IPlaneTypeService _planeTypeService;
    public PlaneService(
      IUnitOfWork unitOfWork,
      IPlaneTypeService planeTypeService
      )
      : base(unitOfWork)
    {
      _planeTypeService = planeTypeService;
    }

    public IList<PlaneDetails> GetAllDetails()
    {
      var planes = _unitOfWork.Set<Plane>().Details();
      return planes.Select(PlaneDetails.Create).ToList();
    }

    public PlaneDetails GetDetails(int id)
    {
      var plane = _unitOfWork.Set<Plane>()
        .Details(x => x.Id == id).FirstOrDefault();

      if (plane == null)
        throw new NotFoundException("Plane with such id was not found");

      return PlaneDetails.Create(plane);
    }
  }
}