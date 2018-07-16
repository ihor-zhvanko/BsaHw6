using System;
using FluentValidation;

using Airport.Api.Models;
using Airport.BusinessLogic.Models;

namespace Airport.Api.Validation
{
  public class PilotModelValidator : AbstractValidator<PilotModel>
  {
    public PilotModelValidator()
    {
      RuleFor(x => x.BirthDate).NotEmpty();
      RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name should not be empty");
      RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name should not be empty");
      RuleFor(x => x.Experience).GreaterThan(0).WithMessage("Experience should be greater than 0");
    }
  }
}