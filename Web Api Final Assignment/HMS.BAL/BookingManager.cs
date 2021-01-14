using HMS.BAL.Interfaces;
using HMS.DAL.Repository;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL
{
    public class BookingManager:IBookingManager
    {
        private readonly IBookingRepository _BookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            _BookingRepository = bookingRepository;
        }

        public List<Booking> GetAllBookings()
        {
            return _BookingRepository.GetAllBokkings();
        }

        public string CreateBooking(Booking BookingModel)
        {
            return _BookingRepository.CreateBooking(BookingModel);
        }

        public string UpdateBooking(Booking BookingModel)
        {
            return _BookingRepository.UpdateBooking(BookingModel);
        }

        //public string UpdateBookingDate(int Id, DateTime UpdateDate)
        //{
        //    return _BookingRepository.UpdateBookingDate(Id, UpdateDate);
        //}

        //public string UpdateBookingStatus(int Id, int UpdateStatus)
        //{
        //    return _BookingRepository.UpdateBookingStatus(Id, UpdateStatus);
        //}

        public string DeleteBooking(int Id)
        {
            return _BookingRepository.DeleteBooking(Id);
        }
    }
}
