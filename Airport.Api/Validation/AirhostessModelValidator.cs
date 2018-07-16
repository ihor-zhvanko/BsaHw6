using System;
using FluentValidation;

using Airport.Api.Models;
using Airport.BusinessLogic.Models;

namespace Airport.Api.Validation
{
  public class AirhostessModelValidator : AbstractValidator<AirhostessModel>
  {
    public AirhostessModelValidator()
    {
      RuleFor(x => x.CrewId).GreaterThan(0).WithMessage("Crew Id should be greater than 0");
      RuleFor(x => x.BirthDate).NotEmpty();
      RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name should not be empty");
      RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name should not be empty");
    }
  }
}