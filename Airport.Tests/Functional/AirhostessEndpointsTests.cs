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
using System.Net.Http;
using System.Net;

namespace Airport.Tests.Functional
{
  [TestFixture]
  public class AirhostessesEndpointsTests
  {
    protected readonly string BASE_URL = "https://localhost:5001/api/airhostesses";

    protected AirportDbContext AirportDbContext { get; private set; }
    protected AirportInitializer AirportInitializer { get; private set; }

    [SetUp]
    public void Setup()
    {
      AirportDbContext = EndpointsTestsSetup.GetAirportDbContext();
      AirportInitializer = EndpointsTestsSetup.GetAirportInitializer(EndpointsTestsSetup.GetAirportDbContext());

      AirportInitializer.Seed().Wait();
    }

    [TearDown]
    public void TearDown()
    {
      AirportInitializer.AntiSeed().Wait();
      AirportDbContext.Dispose();
    }

    [Test]
    public async Task Airhostesses_Get_When_user_request_all_Then_it_returns_all()
    {
      // Arrange
      var url = "";
      var expectedAirhostesses = await AirportDbContext.Airhostess.ToListAsync();
      var expectedAirhostessesDTO = Mapper.Map<IList<AirhostessDTO>>(expectedAirhostesses);

      //Act
      var result = await Get<IList<AirhostessDTO>>(url);

      //Assert
      Assert.AreEqual(expectedAirhostessesDTO.Count, result.Count);

      foreach (var expected in expectedAirhostessesDTO)
      {
        var entityInResult = result.FirstOrDefault(x => x.Id == expected.Id);
        Assert.NotNull(entityInResult);

        Assert.AreEqual(expected.Id, entityInResult.Id);
        Assert.AreEqual(expected.FirstName, entityInResult.FirstName);
        Assert.AreEqual(expected.LastName, entityInResult.LastName);
        Assert.AreEqual(expected.BirthDate, entityInResult.BirthDate);
        Assert.AreEqual(expected.CrewId, entityInResult.CrewId);
      }
    }

    private async Task<T> Get<T>(string url)
    {
      using (var httpClientHandler = new HttpClientHandler())
      {
        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

        using (var client = new HttpClient(httpClientHandler))
        {
          client.BaseAddress = new Uri(BASE_URL);
          var data = await client.GetStringAsync(url);

          return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }
      }


    }
  }
}