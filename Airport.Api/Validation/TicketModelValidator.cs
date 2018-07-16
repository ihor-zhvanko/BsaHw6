using System;
using FluentValidation;

using Airport.Api.Models;
using Airport.BusinessLogic.Models;

namespace Airport.Api.Validation
{
  public class TicketModelValidator : AbstractValidator<TicketModel>
  {
    public TicketModelValidator()
    {
      RuleFor(x => x.FlightId).NotEmpty().GreaterThan(0).WithMessage("Flight id should be greater than 0");
      RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
    }
  }
}