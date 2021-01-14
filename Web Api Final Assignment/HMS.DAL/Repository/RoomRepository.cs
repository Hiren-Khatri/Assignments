using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public class RoomRepository:IRoomRepository
    {
        private readonly Database.DbHotelManagementEntities _DbContext;

        public RoomRepository()
        {
            _DbContext = new Database.DbHotelManagementEntities();
        }

        public List<Room> GetAllRooms(string City, int Pincode = 0, float Price = 0f, int Category = 0)
        {
            var Entities = _DbContext.Rooms.ToList();
            var HotelEntities = _DbContext.Hotels;

            List<Room> RoomsList = new List<Room>();

            if (Entities != null)
            {
                foreach (var room in Entities)
                {
                    Room RoomModel = new Room
                    {
                        Id = room.Id,
                        HotelId = room.HotelId,
                        RoomName = room.RoomName,
                        RoomCategory = room.RoomCategory,
                        RoomPrice = room.RoomPrice,
                        IsActive = room.IsActive,
                        CreatedBy = room.CreatedBy,
                        CreatedDate = room.CreatedDate,
                        UpdatedBy = room.UpdatedBy,
                        UpdatedDate = room.UpdatedDate
                    };
                    RoomsList.Add(RoomModel);
                }
            }
            RoomsList.OrderBy(room => room.RoomPrice);
            if((City ==  null || City.Equals("")) && Pincode == 0 && Price == 0 && Category == 0)
            {
                return RoomsList;
            }else if(City != null && !City.Equals(""))
            {
                return RoomsList.Where(room => _DbContext.Hotels.Find(room.HotelId).City.Equals(City)).ToList();
            }
            else if (Pincode !=0)
            {
                return RoomsList.Where(room => _DbContext.Hotels.Find(room.HotelId).Pincode== Pincode).ToList();
            }
            else if (Category != 0)
            {
                return RoomsList.Where(room => room.RoomCategory == Category).ToList();
            }
            else
            {
                return RoomsList;
            }
        }

        public Room GetRoom(int Id)
        {
            var Entity = _DbContext.Rooms.Find(Id);


            Room RoomModel = new Room();

            if (Entity != null)
            {
                RoomModel.Id = Entity.Id;
                RoomModel.HotelId = Entity.HotelId;
                RoomModel.RoomName = Entity.RoomName;
                RoomModel.RoomCategory = Entity.RoomCategory;
                RoomModel.RoomPrice = Entity.RoomPrice;
                RoomModel.IsActive = Entity.IsActive;
                RoomModel.CreatedBy = Entity.CreatedBy;
                RoomModel.CreatedDate = Entity.CreatedDate;
                RoomModel.UpdatedBy = Entity.UpdatedBy;
                RoomModel.UpdatedDate = Entity.UpdatedDate;

                return RoomModel;
            }
            else
            {
                return null;
            }
        }

        public string CreateRoom(Room RoomModel)
        {
            try
            {
                if (RoomModel != null)
                {
                    Database.Room Entity = new Database.Room
                    {
                        Id = RoomModel.Id,
                        HotelId = RoomModel.HotelId,
                        RoomName = RoomModel.RoomName,
                        RoomCategory = RoomModel.RoomCategory,
                        RoomPrice = RoomModel.RoomPrice,
                        IsActive = RoomModel.IsActive,
                        CreatedBy = RoomModel.CreatedBy,
                        CreatedDate = DateTime.Now,
                    };

                    _DbContext.Rooms.Add(Entity);
                    _DbContext.SaveChanges();

                    return "Successfully Added!";
                }
                return "Model Is Null!";

            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

        public string UpdateRoom(Room RoomModel)
        {
            var Entity = _DbContext.Rooms.Find(RoomModel.Id);

            try
            {
                if (RoomModel != null && Entity != null)
                {
                    Entity.Id = RoomModel.Id;
                    Entity.HotelId = RoomModel.HotelId;
                    Entity.RoomName = RoomModel.RoomName;
                    Entity.RoomCategory = RoomModel.RoomCategory;
                    Entity.RoomPrice = RoomModel.RoomPrice;
                    Entity.IsActive = RoomModel.IsActive;
                    Entity.CreatedBy = RoomModel.CreatedBy;
                    Entity.CreatedDate = RoomModel.CreatedDate;
                    Entity.UpdatedBy = RoomModel.UpdatedBy;
                    Entity.UpdatedDate = DateTime.Now;

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

        public string DeleteRoom(int Id)
        {
            try
            {
                var Entity = _DbContext.Rooms.Find(Id);

                if (Entity != null)
                {
                    _DbContext.Rooms.Remove(Entity);
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

        public bool IsAvailableRoom(int Id,DateTime Date)
        {
            var Entity = _DbContext.Bookings.ToList();
            if(Entity != null)
            {
                foreach(var booking in Entity)
                {
                    if(booking.RoomId == Id && booking.BookingDate == Date && (booking.Status != 0 || booking.Status != 2|| booking.Status != 3))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
