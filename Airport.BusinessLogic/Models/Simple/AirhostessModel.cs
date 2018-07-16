using System;

namespace Airport.BusinessLogic.Models
{
  public class AirhostessModel
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public int CrewId { get; set; }
  }
}