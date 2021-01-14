using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomName { get; set; }
        public int RoomCategory { get; set; }
        public double RoomPrice { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }

        public enum Category
        {
            Category1,
            Category2,
            Category3
        }
    }
}
