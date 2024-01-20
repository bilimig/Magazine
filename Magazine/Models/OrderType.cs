using System;
using System.Collections.Generic;

namespace Magazine.Models
{
    public partial class OrderType
    {
        public OrderType()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
