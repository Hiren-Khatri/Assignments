using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public interface IHotelRepository
    {
        Hotel GetHotel(int Id);
        List<Hotel> GetAllHotels();
        string CreateHotel(Hotel HotemModel);
        string UpdateHotel(Hotel HotemModel);
        string DeleteHotel(int Id);
    }
}
