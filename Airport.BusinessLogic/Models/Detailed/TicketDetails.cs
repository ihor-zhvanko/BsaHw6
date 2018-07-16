using System;
using AutoMapper;

using Airport.Data.Models;

namespace Airport.BusinessLogic.Models
{
  public class TicketDetails
  {
    public int Id { get; set; }
    public double Price { get; set; }
    public FlightModel Flight { get; set; }

    public static TicketDetails Create(Ticket ticket)
    {
      return new TicketDetails
      {
        Id = ticket.Id,
        Price = ticket.Price,
        Flight = Mapper.Map<FlightModel>(ticket.Flight)
      };
    }
  }
}