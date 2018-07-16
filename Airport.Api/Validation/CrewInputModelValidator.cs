using System;
using FluentValidation;

using Airport.Api.Models;

namespace Airport.Api.Validation
{
  public class CrewInputModelValidator : AbstractValidator<CrewInputModel>
  {
    public CrewInputModelValidator()
    {
      RuleFor(x => x.PilotId).GreaterThan(0).WithMessage("Pilot Id should be greater than 0");
      RuleFor(x => x.AirhostessIds).NotEmpty().WithMessage("Unable to create crew without airhostesses");
    }
  }
}