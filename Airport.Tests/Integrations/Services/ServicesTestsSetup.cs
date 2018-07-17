using System;
using NUnit.Framework;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Airport.Common.Mappers;
using Airport.Data.UnitOfWork;
using Airport.Data.DatabaseContext;

namespace Airport.Tests.Integrations.Services
{
  [SetUpFixture]
  public class ServicesTestsSetup
  {
    public static IUnitOfWork UnitOfWork { get; private set; }

    [OneTimeSetUp]
    public void GlobalSetup()
    {
      MapperConfig.InitMappers();

      var options = new DbContextOptionsBuilder();
      options.UseSqlServer("Server=.\\SQLEXPRESS;Database=AirportDevelopment;Trusted_Connection=True;", b => b.MigrationsAssembly("Airport.Data"));
      //AirportDbContext = new AirportDbContext(options.Options);
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
      Mapper.Reset();
    }
  }
}