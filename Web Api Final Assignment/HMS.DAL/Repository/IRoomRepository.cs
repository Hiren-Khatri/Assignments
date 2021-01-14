using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public interface IRoomRepository
    {
        Room GetRoom(int Id);
        List<Room> GetAllRooms(string City, int Pincode = 0, float Price = 0f, int Category = 0);
        string CreateRoom(Room RoomModel);
        string UpdateRoom(Room RoomModel);
        string DeleteRoom(int Id);
        bool IsAvailableRoom(int Id, DateTime Date);
    }
}
