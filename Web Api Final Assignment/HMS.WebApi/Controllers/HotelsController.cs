using HMS.BAL.Interfaces;
using HMS.Models;
using HMS.WebApi.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace HMS.WebApi.Controllers
{
    public class HotelsController : ApiController
    {
        private readonly IHotelManager _HotelManager;

        public HotelsController(IHotelManager hotelManager)
        {
            _HotelManager = hotelManager;
        }

        [Route("api/Hotels")]
        [HttpGet]
        public List<Hotel> GetHotels()
        {
            return _HotelManager.GetAllHotels();
        }

        [HttpGet]
        [ResponseType(typeof(Hotel))]
        [Route("api/Hotels/{Id}")]
        public IHttpActionResult GetHotel(int Id)
        {
            Hotel Entity = _HotelManager.GetHotel(Id);
            if(Entity != null)
            {
                return Ok(Entity);
            }
            return NotFound();
        }

        [ResponseType(typeof(Hotel))]
        [Route("api/CreateHotel")]
        public IHttpActionResult PostHotel([FromBody]Hotel HotelModel)
        {
            string Response = _HotelManager.CreateHotel(HotelModel);
            if(Response.Equals("Successfully Added!"))
            {
                return Ok(HotelModel);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/UpdateHotel")]
        public IHttpActionResult PutHotel([FromBody]Hotel HotelModel)
        {
            string Response = _HotelManager.UpdateHotel(HotelModel);
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
        [Route("api/DeleteHotel/{Id}")]
        public IHttpActionResult DelteHotel(int Id)
        {
            string Response = _HotelManager.DeleteHotel(Id);
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
