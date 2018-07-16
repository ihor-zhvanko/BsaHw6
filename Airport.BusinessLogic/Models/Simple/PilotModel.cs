using System;

namespace Airport.BusinessLogic.Models
{
  public class PilotModel
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public double Experience { get; set; }

  }
}