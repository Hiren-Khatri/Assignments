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
    public class HotelManager:IHotelManager
    {
        private readonly IHotelRepository _HotelRepository;

        public HotelManager(IHotelRepository hotelRepository)
        {
            _HotelRepository = hotelRepository;
        }

        public List<Hotel> GetAllHotels()
        {
            return _HotelRepository.GetAllHotels();
        }

        public Hotel GetHotel(int Id)
        {
            return _HotelRepository.GetHotel(Id);
        }

        public string CreateHotel(Hotel HotelModel)
        {
            return _HotelRepository.CreateHotel(HotelModel);
        }

        public string UpdateHotel(Hotel HotelModel)
        {
            return _HotelRepository.UpdateHotel(HotelModel);
        }

        public string DeleteHotel(int Id)
        {
            return _HotelRepository.DeleteHotel(Id);
        }

    }
}
