using System;
using FluentValidation;

using Airport.Api.Models;
using Airport.BusinessLogic.Models;

namespace Airport.Api.Validation
{
  public class PlaneTypeModelValidator : AbstractValidator<PlaneTypeModel>
  {
    public PlaneTypeModelValidator()
    {
      RuleFor(x => x.Model).NotEmpty().WithMessage("Model should not be empty");
      RuleFor(x => x.Seats).GreaterThan(0).WithMessage("Seats count should be greater than 0");
      RuleFor(x => x.Carrying).GreaterThan(0).WithMessage("Carrying should be greater than 0");
    }
  }
}