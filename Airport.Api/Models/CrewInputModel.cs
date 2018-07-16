using System;
using System.Collections.Generic;
using Airport.BusinessLogic.Models;

namespace Airport.Api.Models
{
  public class CrewInputModel : CrewModel
  {
    public IList<int> AirhostessIds { get; set; }
  }
}