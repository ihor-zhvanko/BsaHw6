using System;
using System.Collections.Generic;
using NUnit.Framework;
using FakeItEasy;
using FluentValidation;
using FluentValidation.Results;
using AutoMapper;

using Airport.Common.DTOs;
using Airport.Common.Exceptions;

using Airport.BusinessLogic.Services;

using Airport.Api.Controllers;

namespace Airport.Tests.Units.Controllers
{
  [TestFixture]
  public class AirhostessControllerTests
  {
    protected IValidator<AirhostessDTO> AlwaysValidValidator { get; private set; }
    protected IValidator<AirhostessDTO> AlwaysInValidValidator { get; private set; }

    [SetUp]
    public void Setup()
    {
      AlwaysValidValidator = A.Fake<IValidator<AirhostessDTO>>();
      var validValidationResult = new ValidationResult();
      A.CallTo(() => AlwaysValidValidator.Validate(A<AirhostessDTO>._)).Returns(validValidationResult);

      AlwaysInValidValidator = A.Fake<IValidator<AirhostessDTO>>();
      var validationFailure = new ValidationFailure("Property", "Is Invalid");
      var invalidValidationResult = new ValidationResult(new[] { validationFailure });
      A.CallTo(() => AlwaysInValidValidator.Validate(A<AirhostessDTO>._)).Returns(invalidValidationResult);
    }

    [Test]
    public void Get_When_is_called_Then_all_dtos_are_returned()
    {
      // Arrange
      var airhostessDTO1 = new AirhostessDTO()
      {
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1
      };

      var airhostessDTO2 = new AirhostessDTO()
      {
        FirstName = "Airhostess2",
        LastName = "Airhostess2",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 2
      };

      var airhostessDTO3 = new AirhostessDTO()
      {
        FirstName = "Airhostess3",
        LastName = "Airhostess3",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 3
      };

      var airhostessServiceFake = A.Fake<IAirhostessService>();
      A.CallTo(() => airhostessServiceFake.GetAll()).Returns(new List<AirhostessDTO> { airhostessDTO1, airhostessDTO2, airhostessDTO3 });

      var airhostessController = new AirhostessesController(airhostessServiceFake, AlwaysValidValidator);

      // Act
      var result = airhostessController.Get();

      // Assert
      Assert.AreEqual(expectedAirhostessDTO.Id, result.Id);
      Assert.AreEqual(expectedAirhostessDTO.FirstName, result.FirstName);
      Assert.AreEqual(expectedAirhostessDTO.LastName, result.LastName);
      Assert.AreEqual(expectedAirhostessDTO.BirthDate, result.BirthDate);
      Assert.AreEqual(expectedAirhostessDTO.CrewId, result.CrewId);
    }
  }