using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace president_snow.Controllers
{
  [Route("api/[controller]")]
  public class RoomController : Controller
  {

    private readonly IRoomsDb rDb;

    public RoomController() : this(new RoomsDb())
    {   
    }

    private RoomController(IRoomsDb roomsDb)
    {
      rDb = roomsDb ?? new RoomsDb();
    }

    // GET api/rooms
    [HttpGet]
    public Room Get()
    {
      var g = Guid.NewGuid();
      //Create new room and return. 
      return CreateRoom(g.ToString());
    }

    private Room CreateRoom(string id)
    {
      var room = new Room(id);

      rDb.Add(room);
      return room;
    }


    [HttpGet]
    [Route("list")]
    public IEnumerable<Room> GetAll()
    {
      return rDb.GetAll();
    }

    // GET api/rooms/5
    [HttpGet("{id}")]
    public object Get(string id)
    {
      var room = rDb.Get(id) ?? CreateRoom(id);
      return room;
    }

    // POST api/rooms
    [HttpPost]
    [Route("{id}/venue/{venue}")]
    public void AddVenue(string id, string venue, [FromHeader]string xOmnomUserid)
    {
      var room = rDb.Get(id);
      room?.Venues.Add(venue, xOmnomUserid);
    }

    [HttpPost]
    [Route("{id}/time/{time}")]
    public void AddTime(string id, string time, [FromHeader]string xOmnomUserid)
    {
      var room = rDb.Get(id);
      room?.Times.Add(time, xOmnomUserid);
    }

    [HttpPost]
    [Route("{id}/driver")]
    public void AddDriver(string id, [FromHeader]string xOmnomUserid)
    {
      var room = rDb.Get(id);
      room?.Drivers.Add(xOmnomUserid);
    }
  
  }
}
