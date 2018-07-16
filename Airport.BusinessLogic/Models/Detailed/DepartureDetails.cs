using System;
using Airport.Data.Models;

using AutoMapper;

namespace Airport.BusinessLogic.Models
{
  public class DepartureDetails
  {
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public FlightModel Flight { get; set; }
    public PlaneDetails Plane { get; set; }
    public CrewDetails Crew { get; set; }

    public static DepartureDetails Create(
      Departure departure, PlaneDetails plane, CrewDetails crew
    )
    {
      return new DepartureDetails
      {
        Id = departure.Id,
        Date = departure.Date,
        Flight = Mapper.Map<FlightModel>(departure.Flight),
        Plane = plane,
        Crew = crew
      };
    }

  }
}