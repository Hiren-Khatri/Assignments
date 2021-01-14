using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public class BookingRepository:IBookingRepository
    {
        private readonly Database.DbHotelManagementEntities _DbContext;

        public BookingRepository()
        {
            _DbContext = new Database.DbHotelManagementEntities();
        }

        public List<Booking> GetAllBokkings()
        {
            var Entities = _DbContext.Bookings.ToList();
            List<Booking> AllBokings = new List<Booking>();

            if (Entities != null)
            {
                foreach (var booking in Entities)
                {
                    Booking BookingModel = new Booking();
                    BookingModel.BookingDate = booking.BookingDate;
                    BookingModel.Id = booking.Id;
                    BookingModel.RoomId = booking.RoomId;
                    BookingModel.Status = booking.Status;

                    AllBokings.Add(BookingModel);
                }
            }

            return AllBokings;
        }

        public string CreateBooking(Booking BookingModel)
        {
            try
            {
                if (BookingModel != null)
                {
                   if(DateTime.Now.Date > BookingModel.BookingDate.Date)
                    {
                        return "Can't Book Room For Pass date!";
                    }

                    Database.Booking booking = new Database.Booking
                    {
                        BookingDate = BookingModel.BookingDate,
                        RoomId = BookingModel.RoomId,
                        Status = BookingModel.Status
                    };

                    var previousBooking = _DbContext.Bookings.Where(book => book.BookingDate == BookingModel.BookingDate && book.RoomId == BookingModel.RoomId && book.Status != 1).FirstOrDefault();
                    if(previousBooking == null)
                    {
                        _DbContext.Bookings.Add(booking);
                        _DbContext.SaveChanges();
                        return "Successfully Added!";
                    }
                    else
                    {
                        return "Room is already booked for " + BookingModel.BookingDate + " date";
                    }
                }
                return "Null Model";
            }catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

        public string UpdateBooking(Booking BookingModel)
        {

            try
            {
                var Entity = _DbContext.Bookings.Find(BookingModel.Id);
                if (Entity != null && BookingModel != null)
                {
                    if(BookingModel.BookingDate != Entity.BookingDate && BookingModel.BookingDate.Date>=DateTime.Now.Date )
                    {
                        Entity.BookingDate = BookingModel.BookingDate;
                    }

                    if(BookingModel.Status != Entity.Status && BookingModel.Status != 0 && BookingModel.Status != 3 && Entity.Status != 3)
                    {
                        Entity.Status = BookingModel.Status;
                    }
                    _DbContext.SaveChanges();

                    return "Updated!";
                }
                return "No Data Found!";
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

        //public string UpdateBookingDate(int Id,DateTime UpdateDate)
        //{
        //    try
        //    {
        //        var Entity = _DbContext.Bookings.Find(Id);
        //        if(Entity != null && UpdateDate != null)
        //        {
        //            Entity.BookingDate = UpdateDate;
        //            return "Updated!";
        //        }
        //        return "No Data Found!";
        //    }catch (Exception Ex)
        //    {
        //        return Ex.Message;
        //    }
        //}

        //public string UpdateBookingStatus(int Id, int UpdateStatus)
        //{
        //    try
        //    {
        //        var Entity = _DbContext.Bookings.Find(Id);
        //        if (Entity != null && UpdateStatus != 0)
        //        {
        //            Entity.Status = UpdateStatus;
        //            return "Updated!";
        //        }
        //        return "No Data Found!";
        //    }
        //    catch (Exception Ex)
        //    {
        //        return Ex.Message;
        //    }
        //}
        
        public string DeleteBooking(int Id)
        {
            try
            {
                var Entity = _DbContext.Bookings.Find(Id);
                if (Entity != null )
                {
                    Entity.Status = 3;
                    _DbContext.SaveChanges();
                    return "Deleted!";
                }
                return "No Data Found!";
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }
    }
}
