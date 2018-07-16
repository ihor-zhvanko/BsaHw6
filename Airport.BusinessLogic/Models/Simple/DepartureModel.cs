using System;

namespace Airport.BusinessLogic.Models
{
  public class DepartureModel
  {
    public int Id { get; set; }
    public int FlightId { get; set; }
    public DateTime Date { get; set; }
    public int CrewId { get; set; }
    public int PlaneId { get; set; }
  }
}