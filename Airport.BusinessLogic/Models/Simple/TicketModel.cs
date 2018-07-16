using System;

namespace Airport.BusinessLogic.Models
{
  public class TicketModel
  {
    public int Id { get; set; }
    public double Price { get; set; }
    public int FlightId { get; set; }
  }
}