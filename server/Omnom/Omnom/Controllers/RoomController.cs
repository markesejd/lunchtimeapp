using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Omnom.Models;

namespace Omnom.Controllers
{
  public class RoomController : ApiController
  {
    [Route("api/room")]
    [HttpGet]
    public Room Get()
    {
      var g = Guid.NewGuid();
      //Create new room and return. 
      return CreateRoom(g.ToString());
    }

    [Route("api/room/{id}")]
    [HttpGet]
    public Room Get(string id)
    {
      var room = RoomsDb.Instance.Get(id) ?? CreateRoom(id);
      return room;
    }

    private Room CreateRoom(string id)
    {
      var room = new Room(id);

      RoomsDb.Instance.Add(room);
      return room;
    }

    [Route("api/rooms")]
    [HttpGet]
    [ActionName("List")]
    public IEnumerable<Room> List()
    {
      return RoomsDb.Instance.GetAll();
    }

    [HttpPost]
    [Route("api/room/{id}/venue/{venue}")]
    public HttpResponseMessage AddVenue(string id, string venue)
    {
      var userId = GetUserId(Request);
      if (string.IsNullOrWhiteSpace(userId))
      {
        return new HttpResponseMessage(HttpStatusCode.Unauthorized);
      }

      var room = RoomsDb.Instance.Get(id);
      if (room == null)
      {
        return new HttpResponseMessage(HttpStatusCode.NotFound);
      }

      if (room.Venues.Any(a => a.Name.Equals(venue, StringComparison.InvariantCultureIgnoreCase)))
      {
        return new HttpResponseMessage(HttpStatusCode.Conflict);
      }

      room.Venues.Add(new VotableItem
      {
        Name = venue,
        Voters = new List<string>() { userId }
      });

      return new HttpResponseMessage(HttpStatusCode.Created);
    }

    [HttpPost]
    [Route("api/room/{id}/venue/{venue}/vote")]
    public HttpResponseMessage VoteVenue(string id, string venue)
    {
      var userId = GetUserId(Request);
      if (string.IsNullOrWhiteSpace(userId))
      {
        return new HttpResponseMessage(HttpStatusCode.Unauthorized);
      }

      var room = RoomsDb.Instance.Get(id);
      if (room == null)
      {
        return new HttpResponseMessage(HttpStatusCode.NotFound);
      }

      var v = room.Venues.FirstOrDefault(a => a.Name.Equals(venue, StringComparison.InvariantCultureIgnoreCase));

      if (v == null)
      {
        return new HttpResponseMessage(HttpStatusCode.NotFound);
      }

      if (v.Voters.Contains(userId))
      {
        return new HttpResponseMessage(HttpStatusCode.Conflict);
      }

      v.Voters.Add(userId);
      return new HttpResponseMessage(HttpStatusCode.Created);
    }

    // time must replace : with a _ in the URL
    [HttpPost]
    [Route("api/room/{id}/time/{time}")]
    public HttpResponseMessage AddTime(string id, string time)
    {
      var userId = GetUserId(Request);
      if (string.IsNullOrWhiteSpace(userId))
      {
        return new HttpResponseMessage(HttpStatusCode.Unauthorized);
      }

      var room = RoomsDb.Instance.Get(id);
      if (room == null)
      {
        return new HttpResponseMessage(HttpStatusCode.NotFound);
      }

      if (room.Times.Any(a => a.Name.Equals(time, StringComparison.InvariantCultureIgnoreCase)))
      {
        return new HttpResponseMessage(HttpStatusCode.Conflict);
      }

      room.Times.Add(new VotableItem
      {
        Name = time.Replace("_",":"),
        Voters = new List<string>() { userId }
      });

      return new HttpResponseMessage(HttpStatusCode.Created);
    }

    // time must replace : with a _ in the URL
    [HttpPost]
    [Route("api/room/{id}/time/{time}/vote")]
    public HttpResponseMessage VoteTime(string id, string time)
    {
      var userId = GetUserId(Request);
      if (string.IsNullOrWhiteSpace(userId))
      {
        return new HttpResponseMessage(HttpStatusCode.Unauthorized);
      }

      var room = RoomsDb.Instance.Get(id);
      if (room == null)
      {
        return new HttpResponseMessage(HttpStatusCode.NotFound);
      }

      var t = room.Times.FirstOrDefault(a => a.Name.Equals(time.Replace("_",":"), StringComparison.InvariantCultureIgnoreCase));

      if (t == null)
      {
        return new HttpResponseMessage(HttpStatusCode.NotFound);
      }

      if (t.Voters.Contains(userId))
      {
        return new HttpResponseMessage(HttpStatusCode.Conflict);
      }

      t.Voters.Add(userId);
      return new HttpResponseMessage(HttpStatusCode.Created);
    }

    [HttpPost]
    [Route("api/room/{id}/driver")]
    public HttpResponseMessage AddDriver(string id)
    {
      var userId = GetUserId(Request);
      if (string.IsNullOrWhiteSpace(userId))
      {
        return new HttpResponseMessage(HttpStatusCode.Unauthorized);
      }

      var room = RoomsDb.Instance.Get(id);
      if (room == null)
      {
        return new HttpResponseMessage(HttpStatusCode.NotFound);
      }

      if (room.Drivers.Any(a => a.Equals(userId, StringComparison.InvariantCultureIgnoreCase)))
      {
        return new HttpResponseMessage(HttpStatusCode.Conflict);
      }

      room.Drivers.Add(userId);
      return new HttpResponseMessage(HttpStatusCode.Created);
    }

    private static string GetUserId(HttpRequestMessage r)
    {
      IEnumerable<string> userIds = new string[0];
      r.Headers.TryGetValues("X-UserId", out userIds);

      return userIds.FirstOrDefault() ?? string.Empty;
    }
  }
}
