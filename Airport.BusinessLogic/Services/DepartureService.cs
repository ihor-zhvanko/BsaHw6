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
  public interface IDepartureService : IService<DepartureModel>
  {
    IList<DepartureDetails> GetAllDetails();
    DepartureDetails GetDetails(int id);
    IList<DepartureModel> GetByCrewId(int crewId);
  }

  public class DepartureService : BaseService<DepartureModel, Departure>, IDepartureService
  {
    IFlightService _flightService;
    ICrewService _crewService;
    IPlaneService _planeService;

    public DepartureService(
      IUnitOfWork unitOfWork,
      IFlightService flightService,
      ICrewService crewService,
      IPlaneService planeService
      )
      : base(unitOfWork)
    {
      _flightService = flightService;
      _crewService = crewService;
      _planeService = planeService;
    }

    public IList<DepartureDetails> GetAllDetails()
    {
      var departures = _unitOfWork.Set<Departure>().Details();
      var crews = _unitOfWork.Set<Crew>()
        .Details(x => departures.Any(y => x.Id == y.CrewId))
        .Select(CrewDetails.Create);
      var planes = _unitOfWork.Set<Plane>()
        .Details(x => departures.Any(y => x.Id == y.PlaneId))
        .Select(PlaneDetails.Create);

      return departures.Select(x =>
      {
        var plane = planes.First(p => p.Id == x.PlaneId);
        var crew = crews.First(c => c.Id == x.CrewId);
        return DepartureDetails.Create(x, plane, crew);
      }).ToList();
    }

    public DepartureDetails GetDetails(int id)
    {
      var departure = _unitOfWork.Set<Departure>()
        .Details(x => x.Id == id).FirstOrDefault();
      if (departure == null)
        throw new NotFoundException("Departure with such id was not found");

      var crew = _unitOfWork.Set<Crew>()
        .Details(x => x.Id == departure.CrewId)
        .Select(CrewDetails.Create).First();
      var plane = _unitOfWork.Set<Plane>()
        .Details(x => x.Id == departure.PlaneId)
        .Select(PlaneDetails.Create).First();

      return DepartureDetails.Create(departure, plane, crew);
    }

    public IList<DepartureModel> GetByCrewId(int crewId)
    {
      var departures = _unitOfWork.Set<Departure>().Get(x => x.CrewId == crewId).ToList();
      return Mapper.Map<IList<DepartureModel>>(departures);
    }
  }
}
