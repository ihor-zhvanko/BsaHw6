using System;
using AutoMapper;

using Airport.Data.Models;
using Airport.BusinessLogic.Models;

namespace Airport.BusinessLogic.Mappers
{
  public static class MapperConfig
  {
    public static void InitMappers()
    {
      Mapper.Initialize(cfg =>
      {
        cfg.CreateMap<Airhostess, AirhostessModel>();
        cfg.CreateMap<Crew, CrewModel>();
        cfg.CreateMap<Departure, DepartureModel>();
        cfg.CreateMap<Flight, FlightModel>();
        cfg.CreateMap<Pilot, PilotModel>();
        cfg.CreateMap<Plane, PlaneModel>();
        cfg.CreateMap<PlaneType, PlaneTypeModel>();
        cfg.CreateMap<Ticket, TicketModel>();

        cfg.CreateMap<AirhostessModel, Airhostess>();
        cfg.CreateMap<CrewModel, Crew>();
        cfg.CreateMap<DepartureModel, Departure>();
        cfg.CreateMap<FlightModel, Flight>();
        cfg.CreateMap<PilotModel, Pilot>();
        cfg.CreateMap<PlaneModel, Plane>();
        cfg.CreateMap<PlaneTypeModel, PlaneType>();
        cfg.CreateMap<TicketModel, Ticket>();
      });
    }
  }
}