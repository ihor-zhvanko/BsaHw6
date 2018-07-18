using System;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;
using FluentValidation;
using FluentValidation.Results;
using AutoMapper;

using Airport.Common.DTOs;
using Airport.Common.Exceptions;

using Airport.Data.Repositories;
using Airport.Data.UnitOfWork;
using Airport.Data.Models;
using Airport.Data.DatabaseContext;
using Airport.Data.AirportInitializer;

using Airport.BusinessLogic.Services;
using Microsoft.EntityFrameworkCore;

namespace Airport.Tests.Integrations.Services
{
  [TestFixture]
  public class AirhostessServiceTests
  {
    protected IValidator<AirhostessDTO> AlwaysValidValidator { get; private set; }

    protected AirportDbContext AirportDbContext { get; private set; }
    protected AirportInitializer AirportInitializer { get; private set; }
    protected IUnitOfWork UnitOfWork { get; private set; }

    [SetUp]
    public void Setup()
    {
      AlwaysValidValidator = A.Fake<IValidator<AirhostessDTO>>();
      var validValidationResult = new ValidationResult();
      A.CallTo(() => AlwaysValidValidator.Validate(A<AirhostessDTO>._)).Returns(validValidationResult);

      AirportDbContext = ServicesTestsSetup.GetAirportDbContext();
      AirportInitializer = ServicesTestsSetup.GetAirportInitializer(ServicesTestsSetup.GetAirportDbContext());
      UnitOfWork = ServicesTestsSetup.GetUnitOfWork(AirportDbContext);

      AirportInitializer.Seed().Wait();
    }

    [TearDown]
    public void TearDown()
    {
      AirportInitializer.AntiSeed().Wait();
      UnitOfWork.Dispose();
    }

    [Test]
    public void Create_When_entity_is_created_Then_new_entity_creates_in_db()
    {
      // Arrange
      var newEntity = new AirhostessDTO
      {
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 1, 12),
        CrewId = 1
      };

      var service = new AirhostessService(UnitOfWork, AlwaysValidValidator);

      // Act
      var createdEntity = service.Create(newEntity);

      // Assert
      var fromDbEntity = AirportDbContext.Airhostess.FirstOrDefault(x => x.Id == createdEntity.Id);

      Assert.NotNull(fromDbEntity);
      Assert.AreEqual(newEntity.FirstName, fromDbEntity.FirstName);
      Assert.AreEqual(newEntity.LastName, fromDbEntity.LastName);
      Assert.AreEqual(newEntity.BirthDate, fromDbEntity.BirthDate);
      Assert.AreEqual(newEntity.CrewId, fromDbEntity.CrewId);
    }

    [Test]
    public void Update_When_entity_is_updated_Then_existing_entity_in_db_updates()
    {
      // Arrange
      var entityToUpdate = new AirhostessDTO
      {
        Id = 1,
        FirstName = "Airhostess1",
        LastName = "Airhostess1",
        BirthDate = new DateTime(1970, 1, 12),
        CrewId = 1
      };

      var service = new AirhostessService(UnitOfWork, AlwaysValidValidator);

      // Act
      Console.WriteLine("service.Update");
      var updatedEntity = service.Update(entityToUpdate);

      // Assert
      var fromDbEntity = AirportDbContext.Airhostess.FirstOrDefault(x => x.Id == entityToUpdate.Id);

      Assert.NotNull(fromDbEntity);
      Assert.AreEqual(entityToUpdate.Id, fromDbEntity.Id);
      Assert.AreEqual(entityToUpdate.FirstName, fromDbEntity.FirstName);
      Assert.AreEqual(entityToUpdate.LastName, fromDbEntity.LastName);
      Assert.AreEqual(entityToUpdate.BirthDate, fromDbEntity.BirthDate);
      Assert.AreEqual(entityToUpdate.CrewId, fromDbEntity.CrewId);
    }
  }
}