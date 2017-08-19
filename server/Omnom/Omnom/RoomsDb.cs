using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Omnom.Models;

namespace Omnom
{
  public interface IRoomsDb
  {
    void Add(Room room);
    void Update(string id, Room room);
    Room Get(string id);
    List<Room> GetAll();
  }

  public class RoomsDb : IRoomsDb
  {

    public void Add(Room room)
    {
      roomStorage[room.Name] = room;
    }

    public void Update(string id, Room room)
    {
      roomStorage[id] = room;
    }

    public Room Get(string id)
    {
      return roomStorage.ContainsKey(id) ? roomStorage[id] : null;
    }

    public List<Room> GetAll()
    {
      return roomStorage.Values.ToList();
    }

    private Dictionary<string, Room> roomStorage { get; } = new Dictionary<string, Room>();

    private static RoomsDb _instance;
    public static IRoomsDb Instance => _instance ?? (_instance = new RoomsDb());
  }

}
