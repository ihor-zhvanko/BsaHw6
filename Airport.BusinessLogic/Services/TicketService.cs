using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;

using Airport.Common.Exceptions;
using Airport.Common.DTOs;

using Airport.Data.Models;
using Airport.Data.UnitOfWork;

namespace Airport.BusinessLogic.Services
{
  public interface ITicketService : IService<TicketDTO>
  {
    IList<TicketDetailsDTO> GetAllDetails();
    TicketDetailsDTO GetDetails(int id);
  }

  public class TicketService : BaseService<TicketDTO, Ticket>, ITicketService
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

    public IList<TicketDetailsDTO> GetAllDetails()
    {
      var tickets = _unitOfWork.Set<Ticket>().Details();
      return tickets.Select(TicketDetailsDTO.Create).ToList();
    }

    public TicketDetailsDTO GetDetails(int id)
    {
      var ticket = _unitOfWork.Set<Ticket>()
        .Details(x => x.Id == id).FirstOrDefault();

      if (ticket == null)
        throw new NotFoundException("Ticket with such id was not found");

      return TicketDetailsDTO.Create(ticket);
    }
  }
}