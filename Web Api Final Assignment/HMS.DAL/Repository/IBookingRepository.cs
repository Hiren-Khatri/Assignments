using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBokkings();
        string CreateBooking(Booking BookingModel);
        string UpdateBooking(Booking BookingModel);
        //string UpdateBookingDate(int Id,DateTime UpdateDate);
        //string UpdateBookingStatus(int Id,int UpdateStatus);
        string DeleteBooking(int Id);
    }
}
