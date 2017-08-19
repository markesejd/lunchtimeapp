using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace president_snow
{
  public class Room
  {
    public string Name { get; set; }

    public VotableItemList Times { get; set; }
    public VotableItemList Venues { get; set; }
    public List<string> Drivers { get; set; }

    public Room(string name)
    {
      this.Name = name;
      this.Times = new VotableItemList();
      this.Venues = new VotableItemList();
      this.Drivers = new List<string>();
    }
  }
}
