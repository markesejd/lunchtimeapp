using System.Collections.Generic;

namespace Omnom.Models
{
  public class Room
  {
    public string Name { get; set; }
    public List<VotableItem> Times { get; set; }
    public List<VotableItem> Venues { get; set; }
    public List<string> Drivers { get; set; }

    public Room() : this("")
    {
    }

    public Room(string name)
    {
      Name = name;
      Times = new List<VotableItem>();
      Venues = new List<VotableItem>();
      Drivers = new List<string>();
    }
  }
}
