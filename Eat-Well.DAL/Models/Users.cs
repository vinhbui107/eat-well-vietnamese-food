using System;
using System.Collections.Generic;

namespace Eat_Well.DAL.Models
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
