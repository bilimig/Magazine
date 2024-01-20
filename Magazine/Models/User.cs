using System;
using System.Collections.Generic;

namespace Magazine.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int? DetailsId { get; set; }
        public string? Password { get; set; }
        public bool? IsAdmin { get; set; }

        public virtual ContactDetail? Details { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
