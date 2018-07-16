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
  public interface ITicketService : IService<TicketModel>
  {
    IList<TicketDetails> GetAllDetails();
    TicketDetails GetDetails(int id);
  }

  public class TicketService : BaseService<TicketModel, Ticket>, ITicketService
  {
    IFlightService _flightService;
    public TicketService(
      IUnitOfWork unitOfWork,
      IFlightService flightService
    )
      : base(unitOfWork)
    {
      _flightService = flightService;
    }

    public IList<TicketDetails> GetAllDetails()
    {
      var tickets = _unitOfWork.Set<Ticket>().Details();
      return tickets.Select(TicketDetails.Create).ToList();
    }

    public TicketDetails GetDetails(int id)
    {
      var ticket = _unitOfWork.Set<Ticket>()
        .Details(x => x.Id == id).FirstOrDefault();

      if (ticket == null)
        throw new NotFoundException("Ticket with such id was not found");

      return TicketDetails.Create(ticket);
    }
  }
}