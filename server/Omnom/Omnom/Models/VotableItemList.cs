using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Omnom.Models
{
  public class VotableItemList
  {
    private readonly List<VotableItem> _internalList = new List<VotableItem>();

    public void Add(string item, string voter)
    {
      var listItem = _internalList.SingleOrDefault(a => a.Name.Equals(item, StringComparison.InvariantCultureIgnoreCase));
      if (listItem == null)
      {
        listItem = new VotableItem();
        _internalList.Add(listItem);
      }

      if (listItem.Voters.Contains(voter))
      {
        return;
      }

      listItem.Name = item;
      listItem.Voters.Add(voter);
    }

    public List<VotableItem> GetList()
    {
      return _internalList;
    }

  }
}