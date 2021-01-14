using HMS.BAL.Interfaces;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HMS.WebApi.Controllers
{
    public class BookingsController : ApiController
    {
        private readonly IBookingManager _BookingManager;

        public BookingsController(IBookingManager bookingManager)
        {
            _BookingManager = bookingManager;
        }

        [HttpGet]
        [Route("api/Bookings")]
        public List<Booking> GetAllBookings()
        {
            return _BookingManager.GetAllBookings();
        }

        [HttpPost]
        [Route("api/CreateBooking")]
        public IHttpActionResult PostBooking([FromBody] Booking BookingModel)
        {
            var Response = _BookingManager.CreateBooking(BookingModel);

            if(Response.Equals("Successfully Added!"))
            {
                return Ok("Room Booked");
            }
            else if(Response.StartsWith("Room is already booked for"))
            {
                return BadRequest("Room Is Already Booked For Date " + BookingModel.BookingDate);
            }
            else if (Response.Equals("Can't Book Room For Pass date!"))
            {
                return BadRequest("Date is passed, can't book room");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Bookings")]
        public IHttpActionResult PutBooking([FromBody] Booking BookingModel)
        {
            var Response = _BookingManager.UpdateBooking(BookingModel);

            if (Response.Equals("Updated!"))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest();
            }
        }

        //[HttpPut]
        //[Route("api/Bookings/{Id}")]
        //public IHttpActionResult PutBooking(int Id,[FromBody]DateTime UpdateDate)
        //{
        //    var Response = _BookingManager.UpdateBookingDate(Id, UpdateDate);

        //    if (Response.Equals("Updated!"))
        //    {
        //        return StatusCode(HttpStatusCode.NoContent);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpPut]
        //[Route("api/Bookings/{Id}")]
        //public IHttpActionResult PutBooking(int Id,[FromBody] int UpdateStatus)
        //{
        //    var Response = _BookingManager.UpdateBookingStatus(Id, UpdateStatus);

        //    if (Response.Equals("Updated!"))
        //    {
        //        return StatusCode(HttpStatusCode.NoContent);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpDelete]
        [Route("api/Bookings/{Id}")]
        public IHttpActionResult DeleteBooking(int Id)
        {
            var Response = _BookingManager.DeleteBooking(Id);

            if (Response.Equals("Deleted!"))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
