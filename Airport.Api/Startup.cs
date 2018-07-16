using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

using Airport.Api.Validation;
using Airport.Api.Models;
using Airport.Api.Middleware;

using Airport.Data.MockData;
using Airport.Data.UnitOfWork;
using Airport.Data.DatabaseContext;

using Airport.BusinessLogic.Mappers;
using Airport.BusinessLogic.Services;
using Airport.BusinessLogic.Models;
using Airport.Data.AirportInitializer;

namespace Airport.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      MapperConfig.InitMappers();

      services.AddSingleton<DataSource>();

      string connection = Configuration.GetConnectionString("DefaultConnection");
      services.AddDbContext<AirportDbContext>(options =>
        options.UseSqlServer(connection, b => b.MigrationsAssembly("Airport.Api"))
      , ServiceLifetime.Transient);

      services.AddTransient<AirportInitializer>();

      services.AddSingleton<IValidator<AirhostessModel>, AirhostessModelValidator>();
      services.AddSingleton<IValidator<CrewInputModel>, CrewInputModelValidator>();
      services.AddSingleton<IValidator<DepartureModel>, DepartureModelValidator>();
      services.AddSingleton<IValidator<FlightModel>, FlightModelValidator>();
      services.AddSingleton<IValidator<PilotModel>, PilotModelValidator>();
      services.AddSingleton<IValidator<PlaneModel>, PlaneModelValidator>();
      services.AddSingleton<IValidator<PlaneTypeModel>, PlaneTypeModelValidator>();
      services.AddSingleton<IValidator<TicketModel>, TicketModelValidator>();

      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddScoped<IAirhostessService, AirhostessService>();
      services.AddScoped<ICrewService, CrewService>();
      services.AddScoped<IDepartureService, DepartureService>();
      services.AddScoped<IFlightService, FlightService>();
      services.AddScoped<IPilotService, PilotService>();
      services.AddScoped<IPlaneService, PlaneService>();
      services.AddScoped<IPlaneTypeService, PlaneTypeService>();
      services.AddScoped<ITicketService, TicketService>();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, AirportInitializer airportInitializer)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseErrorHandlingMiddleware();
      app.UseMvc();

      airportInitializer.Seed().Wait();
    }
  }
}
