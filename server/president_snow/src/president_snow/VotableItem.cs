using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace president_snow
{
    public class VotableItem
    {
      public string Name { get; set; }

      public List<string> Voters { get; set; }
    }

    public class VotableItemList
    {
      
      private readonly List<VotableItem> _internalList = new List<VotableItem>();

      public void Add(string item, string voter)
      {
        this._internalList.ForEach(rec =>
        {
          if (rec.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase) && (rec.Voters.Contains(voter)))
          {
              rec.Voters.Add(voter);
          }
        });
      }

      public List<VotableItem> GetList()
      {
        return _internalList;
      }
      
    }
}
