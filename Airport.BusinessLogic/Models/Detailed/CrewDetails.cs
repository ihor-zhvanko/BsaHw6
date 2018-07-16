using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Airport.Data.Models;

namespace Airport.BusinessLogic.Models
{
  public class CrewDetails
  {
    public int Id { get; set; }
    public PilotModel Pilot { get; set; }
    public IList<AirhostessModel> Airhostesses { get; set; }

    public static CrewDetails Create(Crew crew)
    {
      return new CrewDetails
      {
        Id = crew.Id,
        Pilot = Mapper.Map<PilotModel>(crew.Pilot),
        Airhostesses = Mapper.Map<IList<AirhostessModel>>(crew.Airhostesses)
      };
    }
  }
}