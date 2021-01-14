using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public class HotelRepository: IHotelRepository
    {
        private readonly Database.DbHotelManagementEntities _DbContext;

        public HotelRepository()
        {
            _DbContext = new Database.DbHotelManagementEntities();
        }

        public List<Hotel> GetAllHotels()
        {
            var Entities = _DbContext.Hotels.ToList();

            List<Hotel> HotelsList = new List<Hotel>();

            if (Entities != null)
            {
                foreach(var hotel in Entities)
                {
                    Hotel HotelModel = new Hotel
                    {
                        Id = hotel.Id,
                        HotelName = hotel.HotelName,
                        City = hotel.City,
                        Pincode = hotel.Pincode,
                        ContactNumber = hotel.ContactNumber,
                        ContactPerson = hotel.ContactPerson,
                        Website = hotel.Website,
                        Facebook = hotel.Facebook,
                        Twitter = hotel.Twitter,
                        IsActive = hotel.IsActive,
                        CreatedBy = hotel.CreatedBy,
                        CreatedDate = hotel.CreatedDate,
                        UpdatedBy = hotel.UpdatedBy,
                        UpdatedDate = hotel.UpdatedDate
                    };

                    HotelsList.Add(HotelModel);
                }
            }
            HotelsList.OrderBy(hotel => hotel.HotelName);
            return HotelsList;
        }

        public Hotel GetHotel(int Id)
        {
            var Entity = _DbContext.Hotels.Find(Id);


            Hotel HotelModel = new Hotel();

            if (Entity != null)
            {
                HotelModel.Id = Entity.Id;
                HotelModel.HotelName = Entity.HotelName;
                HotelModel.City = Entity.City;
                HotelModel.Pincode = Entity.Pincode;
                HotelModel.ContactNumber = Entity.ContactNumber;
                HotelModel.ContactPerson = Entity.ContactPerson;
                HotelModel.Website = Entity.Website;
                HotelModel.Facebook = Entity.Facebook;
                HotelModel.Twitter = Entity.Twitter;
                HotelModel.IsActive = Entity.IsActive;
                HotelModel.CreatedBy = Entity.CreatedBy;
                HotelModel.CreatedDate = Entity.CreatedDate;
                HotelModel.UpdatedBy = Entity.UpdatedBy;
                HotelModel.UpdatedDate = Entity.UpdatedDate;
                
                return HotelModel;
            }
            else
            {
                return null;
            }
        }

        public string CreateHotel(Hotel HotelModel)
        {
            try
            {
                if (HotelModel != null)
                {
                    Database.Hotel Entity = new Database.Hotel
                    {
                        HotelName = HotelModel.HotelName,
                        City = HotelModel.City,
                        Pincode = HotelModel.Pincode,
                        ContactNumber = HotelModel.ContactNumber,
                        ContactPerson = HotelModel.ContactPerson,
                        Website = HotelModel.Website,
                        Facebook = HotelModel.Facebook,
                        Twitter = HotelModel.Twitter,
                        IsActive = HotelModel.IsActive,
                        CreatedBy = HotelModel.CreatedBy,
                        CreatedDate = DateTime.Now
                    };

                    if (HotelModel.Rooms != null && HotelModel.Rooms.Count >= 1)
                    {
                        foreach (var room in HotelModel.Rooms)
                        {
                            Database.Room hotelroom = new Database.Room();

                            hotelroom.HotelId = HotelModel.Id;
                            hotelroom.RoomName = room.RoomName;
                            hotelroom.RoomPrice = room.RoomPrice;
                            hotelroom.IsActive = room.IsActive;
                            hotelroom.CreatedBy = room.CreatedBy;
                            hotelroom.CreatedDate = DateTime.Now;
                            hotelroom.RoomCategory = room.RoomCategory;

                            _DbContext.Rooms.Add(hotelroom);
                        }
                    }

                    _DbContext.Hotels.Add(Entity);
                    _DbContext.SaveChanges();
                    
                    return "Successfully Added!";
                }
                return "Model Is Null!";

            }catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

        public string UpdateHotel(Hotel HotelModel)
        {
            var Entity = _DbContext.Hotels.Find(HotelModel.Id);

            try
            {
                if (HotelModel != null)
                {
                    Entity.HotelName = HotelModel.HotelName;
                    Entity.City = HotelModel.City;
                    Entity.Pincode = HotelModel.Pincode;
                    Entity.ContactNumber = HotelModel.ContactNumber;
                    Entity.ContactPerson = HotelModel.ContactPerson;
                    Entity.Website = HotelModel.Website;
                    Entity.Facebook = HotelModel.Facebook;
                    Entity.Twitter = HotelModel.Twitter;
                    Entity.IsActive = HotelModel.IsActive;
                    Entity.UpdatedBy = HotelModel.UpdatedBy;
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

        public string DeleteHotel(int Id)
        {
            try
            {
                var Entity = _DbContext.Hotels.Find(Id);

                if(Entity != null)
                {
                    _DbContext.Hotels.Remove(Entity);
                    _DbContext.SaveChanges();

                    return "Deleted!";
                }
                return "No Data Found!";
            }catch (Exception Ex)
            {
                return Ex.Message;
            }
        }
    }
}
