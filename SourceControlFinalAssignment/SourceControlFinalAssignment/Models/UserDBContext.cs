using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SourceControlFinalAssignment.Models
{
    public class UserDBContext:DbContext
    {
        public UserDBContext() : base("DefaultConnection")
        {

        }

        public virtual DbSet<User> userList { get; set; }
    }
}