using System;
using System.IO;
using NUnit.Framework;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Airport.Common.Mappers;
using Airport.Data.UnitOfWork;
using Airport.Data.Repositories;
using Airport.Data.DatabaseContext;
using Airport.Data.AirportInitializer;
using Airport.Data.MockData;

namespace Airport.Tests.Integrations.Services
{
  [SetUpFixture]
  public class ServicesTestsSetup
  {
    public static IUnitOfWork GetUnitOfWork(AirportDbContext airportDbContext)
    {
      var unitOfWork = new UnitOfWork(airportDbContext);
      return unitOfWork;
    }

    public static AirportDbContext GetAirportDbContext()
    {
      var options = new DbContextOptionsBuilder(new DbContextOptions<AirportDbContext>());
      options.UseSqlServer(ConnectionString).EnableSensitiveDataLogging();
      return new AirportDbContext(options.Options as DbContextOptions<AirportDbContext>);
    }

    public static AirportInitializer GetAirportInitializer(AirportDbContext airportDbContext)
    {
      return new AirportInitializer(airportDbContext, new DataSource());
    }

    protected static string ConnectionString => "Server=.\\SQLEXPRESS;Database=AirportDevelopmentTests;Trusted_Connection=True;";

    [OneTimeSetUp]
    public void GlobalSetup()
    {
      MapperConfig.InitMappers();

      // Migrate database if needed
      var optionsBuilder = new DbContextOptionsBuilder(new DbContextOptions<AirportDbContext>());
      optionsBuilder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("Airport.Data"));
      var options = optionsBuilder.Options as DbContextOptions<AirportDbContext>;
      using (var airportDbContext = new AirportDbContext(options))
      {
        airportDbContext.Database.Migrate();
      }
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
      Mapper.Reset();
    }
  }
}