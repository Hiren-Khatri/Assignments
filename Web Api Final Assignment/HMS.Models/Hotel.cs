using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Models
{
    public partial class Hotel
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public string ContactNumber { get; set; }
        public string ContactPerson { get; set; }
        public string Website { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
