using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using FakeItEasy;
using FluentValidation;
using FluentValidation.Results;
using AutoMapper;

using Airport.Api;

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

    [Test]
    public void Update_When_entity_is_updated_Then_other_entities_are_not_changed()
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

      var airhostessesBeforeUpdate = AirportDbContext.Airhostess
        .Where(x => x.Id != entityToUpdate.Id).ToList();

      var service = new AirhostessService(UnitOfWork, AlwaysValidValidator);

      // Act
      var updatedEntity = service.Update(entityToUpdate);

      var airhostessesAfterUpdate = AirportDbContext.Airhostess
        .Where(x => x.Id != entityToUpdate.Id).ToList();

      // Assert
      Assert.AreEqual(airhostessesBeforeUpdate.Count, airhostessesAfterUpdate.Count);
      foreach (var beforeUpdate in airhostessesBeforeUpdate)
      {
        var afterUpdate = airhostessesAfterUpdate.FirstOrDefault(x => x.Id == beforeUpdate.Id);
        Assert.NotNull(afterUpdate);

        Assert.AreEqual(beforeUpdate.Id, afterUpdate.Id);
        Assert.AreEqual(beforeUpdate.FirstName, afterUpdate.FirstName);
        Assert.AreEqual(beforeUpdate.LastName, afterUpdate.LastName);
        Assert.AreEqual(beforeUpdate.BirthDate, afterUpdate.BirthDate);
        Assert.AreEqual(beforeUpdate.CrewId, afterUpdate.CrewId);
      }
    }

    [Test]
    public void AssignToCrew_When_airhostesses_are_assigned_to_crew_Then_it_updates_CrewId_field_in_db()
    {
      // Arrange
      var airhostessesIdsToAssign = new List<int>() {
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
      };
      var newCrewId = 10;

      var service = new AirhostessService(UnitOfWork, AlwaysValidValidator);

      // Act
      service.AssignToCrew(airhostessesIdsToAssign, newCrewId);

      // Assert
      var airhostessesFromDb = AirportDbContext.Airhostess.Where(
        x => airhostessesIdsToAssign.Any(y => y == x.Id)
      ).ToList();

      Assert.AreEqual(airhostessesFromDb.Count, airhostessesIdsToAssign.Count);
      foreach (var airhostess in airhostessesFromDb)
      {
        Assert.NotNull(airhostess.CrewId);
        Assert.AreEqual(airhostess.CrewId, newCrewId);
      }
    }

    [Test]
    public void Delete_by_id_When_airhostess_is_deleted_Then_it_deletes_in_db()
    {
      // Arrange
      var airhostessToDeleteId = 3;

      var service = new AirhostessService(UnitOfWork, AlwaysValidValidator);

      // Act
      service.Delete(airhostessToDeleteId);

      // Assert
      var tryToFindDeletedAirhostess = AirportDbContext.Airhostess
        .FirstOrDefault(x => x.Id == airhostessToDeleteId);

      Assert.IsNull(tryToFindDeletedAirhostess);
    }

    [Test]
    public void Delete_by_id_When_airhostess_is_deleted_Then_other_entities_are_not_changed()
    {
      // Arrange
      var airhostessToDeleteId = 3;

      var service = new AirhostessService(UnitOfWork, AlwaysValidValidator);

      var airhostessesBeforeUpdate = AirportDbContext.Airhostess
        .Where(x => x.Id != airhostessToDeleteId).ToList();

      // Act
      service.Delete(airhostessToDeleteId);

      // Assert
      var airhostessesAfterUpdate = AirportDbContext.Airhostess
        .Where(x => x.Id != airhostessToDeleteId).ToList();

      Assert.AreEqual(airhostessesBeforeUpdate.Count, airhostessesAfterUpdate.Count);
      foreach (var beforeUpdate in airhostessesBeforeUpdate)
      {
        var afterUpdate = airhostessesAfterUpdate.FirstOrDefault(x => x.Id == beforeUpdate.Id);
        Assert.NotNull(afterUpdate);

        Assert.AreEqual(beforeUpdate.Id, afterUpdate.Id);
        Assert.AreEqual(beforeUpdate.FirstName, afterUpdate.FirstName);
        Assert.AreEqual(beforeUpdate.LastName, afterUpdate.LastName);
        Assert.AreEqual(beforeUpdate.BirthDate, afterUpdate.BirthDate);
        Assert.AreEqual(beforeUpdate.CrewId, afterUpdate.CrewId);
      }
    }

  }
}