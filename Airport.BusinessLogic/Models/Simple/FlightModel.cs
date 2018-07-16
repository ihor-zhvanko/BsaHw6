using System;

namespace Airport.BusinessLogic.Models
{
  public class FlightModel
  {
    public int Id { get; set; }
    public string Number { get; set; }
    public string DeparturePlace { get; set; }
    public DateTime DepartureTime { get; set; }
    public string ArrivalPlace { get; set; }
    public DateTime ArrivalTime { get; set; }

  }
}