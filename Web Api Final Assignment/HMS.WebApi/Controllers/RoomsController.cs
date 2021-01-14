using HMS.BAL.Interfaces;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HMS.WebApi.Controllers
{
    public class RoomsController : ApiController
    {
        private readonly IRoomManager _RoomManager;

        public RoomsController(IRoomManager roomManager)
        {
            _RoomManager = roomManager;
        }

        //api/Rooms
        public List<Room> GetRoom(string City=null, int Pincode = 0,float Price=0f,int Category=0)
        {
            return _RoomManager.GetAllRooms(City,Pincode,Price,Category); 
        }

        [HttpGet]
        [ResponseType(typeof(Room))]
        [Route("api/Rooms/{Id}")]
        public IHttpActionResult GetRoom(int Id)
        {
            Room Entity = _RoomManager.GetRoom(Id);
            if(Entity != null)
            {
                return Ok(Entity);
            }
            return NotFound();
        }

        [ResponseType(typeof(string))]
        [Route("api/CreateRoom")]
        public IHttpActionResult PostRoom([FromBody]Room RoomModel)
        {
            string Response = _RoomManager.CreateRoom(RoomModel);
            if(Response.Equals("Successfully Added!"))
            {
                return Ok("Room Created!");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/Rooms/{Id}")]
        public bool IsAvailableRoom(int Id,string Date)
        {
            return _RoomManager.IsAvailableRoom(Id, Date);
        }

        [HttpPut]
        [Route("api/UpdateRoom")]
        public IHttpActionResult PutRoom([FromBody]Room RoomModel)
        {
            string Response = _RoomManager.UpdateRoom(RoomModel);
            if (Response.Equals("Updated!"))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/DeleteRoom/{Id}")]
        public IHttpActionResult DelteRoom(int Id)
        {
            string Response = _RoomManager.DeleteRoom(Id);
            if (Response.Equals("Deleted!"))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
