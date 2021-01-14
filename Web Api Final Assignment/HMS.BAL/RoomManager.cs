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
    public class RoomManager:IRoomManager
    {
        private readonly IRoomRepository _RoomRepository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _RoomRepository = roomRepository;
        }

        public List<Room> GetAllRooms(string City, int Pincode = 0, float Price = 0f, int Category = 0)
        {
            return _RoomRepository.GetAllRooms(City,Pincode,Price,Category);
        }

        public Room GetRoom(int Id)
        {
            return _RoomRepository.GetRoom(Id);
        }

        public string CreateRoom(Room RoomModel)
        {
            return _RoomRepository.CreateRoom(RoomModel);
        }

        public string UpdateRoom(Room RoomModel)
        {
            return _RoomRepository.UpdateRoom(RoomModel);
        }

        public string DeleteRoom(int Id)
        {
            return _RoomRepository.DeleteRoom(Id);
        }

       public bool IsAvailableRoom(int Id, string Date)
        {
            string[] dateSplit = Date.Split('-') ;
            DateTime dateTime = new DateTime(Int32.Parse(dateSplit[0]), Int32.Parse(dateSplit[1]), Int32.Parse(dateSplit[2]));
            return _RoomRepository.IsAvailableRoom(Id, dateTime);
        }
    }
}
