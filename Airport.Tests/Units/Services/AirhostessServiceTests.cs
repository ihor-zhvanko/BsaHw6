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

    [SetUp]
    public void Setup()
    {
      AlwaysValidValidator = A.Fake<IValidator<AirhostessDTO>>();
      var validValidationResult = new ValidationResult();
      A.CallTo(() => AlwaysValidValidator.Validate(A<AirhostessDTO>._)).Returns(validValidationResult);

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
      var airhostess1 = new Airhostess()
      {
        Id = 1,
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1,
        Crew = null
      };

      var airhostess1DTO = new AirhostessDTO()
      {
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1
      };

      var expectedDTO = new AirhostessDTO()
      {
        Id = 1,
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 10, 1),
        CrewId = 1
      };
      // var airhostess2 = new Airhostess()
      // {
      //   Id = 1,
      //   FirstName = "Airhostess2",
      //   LastName = "Airhostess2",
      //   BirthDate = new DateTime(1970, 10, 1),
      //   CrewId = 2,
      //   Crew = null
      // };
      // var airhostess3 = new Airhostess()
      // {
      //   Id = 1,
      //   FirstName = "Airhostess3",
      //   LastName = "Airhostess3",
      //   BirthDate = new DateTime(1970, 10, 1),
      //   CrewId = 3,
      //   Crew = null
      // };
      var airhostessRepositoryFake = A.Fake<IAirhostessRepository>();
      A.CallTo(() => airhostessRepositoryFake.Create(A<Airhostess>._)).Returns(airhostess1);

      var unitOfWorkFake = A.Fake<IUnitOfWork>();
      A.CallTo(() => unitOfWorkFake.Set<Airhostess>()).Returns(airhostessRepositoryFake);

      var airhostessService = new AirhostessService(unitOfWorkFake, AlwaysValidValidator);

      // Act
      var result = airhostessService.Create(airhostess1DTO);
      Assert.AreEqual(expectedDTO.Id, result.Id);
      Assert.AreEqual(expectedDTO.FirstName, result.FirstName);
      Assert.AreEqual(expectedDTO.LastName, result.LastName);
      Assert.AreEqual(expectedDTO.BirthDate, result.BirthDate);
      Assert.AreEqual(expectedDTO.CrewId, result.CrewId);
    }
  }
}