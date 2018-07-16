using System;

namespace Airport.BusinessLogic.Models
{
  public class PlaneTypeModel
  {
    public int Id { get; set; }
    public string Model { get; set; }
    public int Seats { get; set; }
    public double Carrying { get; set; }
  }
}