using System;

namespace Airport.BusinessLogic.Models
{
  public class PlaneModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public TimeSpan ServiceLife { get; set; }
    public int PlaneTypeId { get; set; }

  }
}