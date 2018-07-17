using System;
using NUnit.Framework;
using FakeItEasy;
using FluentValidation;
using FluentValidation.Results;
using AutoMapper;

using Airport.Common.DTOs;

using Airport.Data.Repositories;
using Airport.Data.UnitOfWork;
using Airport.Data.Models;

using Airport.BusinessLogic.Services;

namespace Tests.Units.Services
{
  [TestFixture]
  public class AirhostessServiceTests
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

      Mapper.Initialize(cfg =>
      {
        cfg.CreateMap<Airhostess, AirhostessDTO>();
        cfg.CreateMap<AirhostessDTO, Airhostess>();
      });
    }

    [Test]
    public void Create_When_entity_is_created_Then_new_airhostess_with_new_id_is_returned()
    {
      // Arrange
      var airhostessMock = new Airhostess()
      {
        Id = 1,
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1,
        Crew = null
      };

      var airhostessDTOToCreate = new AirhostessDTO()
      {
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1
      };

      var expectedAirhostessDTO = new AirhostessDTO()
      {
        Id = 1,
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1
      };
      var airhostessRepositoryFake = A.Fake<IAirhostessRepository>();
      A.CallTo(() => airhostessRepositoryFake.Create(A<Airhostess>._)).Returns(airhostessMock);

      var unitOfWorkFake = A.Fake<IUnitOfWork>();
      A.CallTo(() => unitOfWorkFake.Set<Airhostess>()).Returns(airhostessRepositoryFake);

      var airhostessService = new AirhostessService(unitOfWorkFake, AlwaysValidValidator);

      // Act
      var result = airhostessService.Create(airhostessDTOToCreate);

      // Assert
      Assert.AreEqual(expectedAirhostessDTO.Id, result.Id);
      Assert.AreEqual(expectedAirhostessDTO.FirstName, result.FirstName);
      Assert.AreEqual(expectedAirhostessDTO.LastName, result.LastName);
      Assert.AreEqual(expectedAirhostessDTO.BirthDate, result.BirthDate);
      Assert.AreEqual(expectedAirhostessDTO.CrewId, result.CrewId);
    }

    [Test]
    public void Create_When_entity_is_created_Then_new_airhostess_with_new_id_is_returned()
    {
      // Arrange
      var airhostessMock = new Airhostess()
      {
        Id = 2,
        FirstName = "",
        LastName = "Airhostess2",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1,
        Crew = null
      };

      var airhostessDTOToCreate = new AirhostessDTO()
      {
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1
      };

      var expectedAirhostessDTO = new AirhostessDTO()
      {
        Id = 1,
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1
      };
      var airhostessRepositoryFake = A.Fake<IAirhostessRepository>();
      A.CallTo(() => airhostessRepositoryFake.Create(A<Airhostess>._)).Returns(airhostessMock);

      var unitOfWorkFake = A.Fake<IUnitOfWork>();
      A.CallTo(() => unitOfWorkFake.Set<Airhostess>()).Returns(airhostessRepositoryFake);

      var airhostessService = new AirhostessService(unitOfWorkFake, AlwaysValidValidator);

      // Act
      var result = airhostessService.Create(airhostessDTOToCreate);

      // Assert
      Assert.AreEqual(expectedAirhostessDTO.Id, result.Id);
      Assert.AreEqual(expectedAirhostessDTO.FirstName, result.FirstName);
      Assert.AreEqual(expectedAirhostessDTO.LastName, result.LastName);
      Assert.AreEqual(expectedAirhostessDTO.BirthDate, result.BirthDate);
      Assert.AreEqual(expectedAirhostessDTO.CrewId, result.CrewId);
    }
  }
}