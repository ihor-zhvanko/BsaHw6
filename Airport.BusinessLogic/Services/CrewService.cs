using System;
using System.Linq;
using System.Collections.Generic;

using AutoMapper;

using Airport.Common.DTOs;
using Airport.Common.Exceptions;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

namespace Airport.BusinessLogic.Services
{
  public interface ICrewService : IService<CrewDTO>
  {
    IList<CrewDetailsDTO> GetAllDetails();
    CrewDetailsDTO GetDetails(int id);
  }

  public class CrewService : BaseService<CrewDTO, Crew>, ICrewService
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

    public IList<CrewDetailsDTO> GetAllDetails()
    {
      var crews = _unitOfWork.Set<Crew>().Details();
      return crews.Select(CrewDetailsDTO.Create).ToList();
    }

    public CrewDetailsDTO GetDetails(int id)
    {
      var crew = _unitOfWork.Set<Crew>()
        .Details(x => x.Id == id).FirstOrDefault();

      if (crew == null)
      {
        throw new NotFoundException("Crew with such id was not found");
      }
      return CrewDetailsDTO.Create(crew);
    }
  }
}