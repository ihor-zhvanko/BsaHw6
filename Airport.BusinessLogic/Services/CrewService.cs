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
  public interface ICrewService : IService<CrewModel>
  {
    IList<CrewDetails> GetAllDetails();
    CrewDetails GetDetails(int id);
  }

  public class CrewService : BaseService<CrewModel, Crew>, ICrewService
  {
    IPilotService _pilotService;
    IAirhostessService _airhostessService;

    public CrewService(
      IUnitOfWork unitOfWork,
      IPilotService pilotService,
      IAirhostessService airhostessService
    )
      : base(unitOfWork)
    {
      this._pilotService = pilotService;
      this._airhostessService = airhostessService;
    }

    public IList<CrewDetails> GetAllDetails()
    {
      var crews = _unitOfWork.Set<Crew>().Details();
      return crews.Select(CrewDetails.Create).ToList();
    }

    public CrewDetails GetDetails(int id)
    {
      var crew = _unitOfWork.Set<Crew>()
        .Details(x => x.Id == id).FirstOrDefault();

      if (crew == null)
      {
        throw new NotFoundException("Crew with such id was not found");
      }
      return CrewDetails.Create(crew);
    }
  }
}