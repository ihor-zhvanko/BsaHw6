using System;
using FluentValidation;

using Airport.Api.Models;
using Airport.BusinessLogic.Models;

namespace Airport.Api.Validation
{
  public class FlightModelValidator : AbstractValidator<FlightModel>
  {
    public FlightModelValidator()
    {
      RuleFor(x => x.Number).NotEmpty().Length(6).WithMessage("Number length should be equal 6");
      RuleFor(x => x.ArrivalPlace).NotEmpty().WithMessage("Arrival place should not be empty");
      RuleFor(x => x.ArrivalTime).NotEmpty().WithMessage("Arrival time should not be empty");
      RuleFor(x => x.DeparturePlace).NotEmpty().WithMessage("Departure place should not be empty");
      RuleFor(x => x.DepartureTime).NotEmpty().WithMessage("Departure time should not be empty");
    }
  }
}