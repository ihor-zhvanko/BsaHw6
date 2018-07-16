using System;
using System.Linq;
using System.Collections.Generic;
using Airport.BusinessLogic.Models;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

using AutoMapper;

namespace Airport.BusinessLogic.Services
{
  public interface IAirhostessService : IService<AirhostessModel>
  {
    IList<AirhostessModel> GetByCrewId(int crewId);
    void AssignToCrew(IList<int> airhostessIds, int crewId);
  }

  public class AirhostessService : BaseService<AirhostessModel, Airhostess>, IAirhostessService
  {
    public AirhostessService(IUnitOfWork unitOfWork)
      : base(unitOfWork)
    { }

    public IList<AirhostessModel> GetByCrewId(int crewId)
    {
      var airhostesses = _unitOfWork.Set<Airhostess>().Get((x) => x.CrewId == crewId);

      return Mapper.Map<IList<AirhostessModel>>(airhostesses);
    }

    public void AssignToCrew(IList<int> airhostessIds, int crewId)
    {
      var toUpdate = _unitOfWork.Set<Airhostess>()
        .Get(x => airhostessIds.Any(y => x.Id == y) || x.CrewId == crewId).ToList();
      foreach (var item in toUpdate)
      {
        if (airhostessIds.Any(x => x == item.Id))
          item.CrewId = crewId;
        else
          item.CrewId = null;

        _unitOfWork.Set<Airhostess>().Update(item);
        _unitOfWork.SaveChanges();
      }
    }
  }
}