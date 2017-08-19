using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omnom.Models
{
  public class VotableItem
  {
    public string Name { get; set; }
    public List<string> Voters { get; set; }

    public VotableItem()
    {
      Voters = new List<string>();
    }
  }
}