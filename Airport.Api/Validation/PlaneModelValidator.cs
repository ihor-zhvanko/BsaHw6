using System;
using FluentValidation;

using Airport.Api.Models;
using Airport.BusinessLogic.Models;

namespace Airport.Api.Validation
{
  public class PlaneModelValidator : AbstractValidator<PlaneModel>
  {
    public PlaneModelValidator()
    {
      RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty");
      RuleFor(x => x.PlaneTypeId).GreaterThan(0).WithMessage("Plane type id should be greater than 0");
      RuleFor(x => x.ReleaseDate).NotEmpty().WithMessage("Release date should not be empty");
      RuleFor(x => x.ReleaseDate).LessThan(DateTime.Now).WithMessage("Release date should be less than current");
      RuleFor(x => x.ServiceLife).NotEmpty().WithMessage("Service life should not be empty");
      RuleFor(x => x.ServiceLife).Must(x => x.Days > 0).WithMessage("Service life days should be greater than 0");
    }
  }
}