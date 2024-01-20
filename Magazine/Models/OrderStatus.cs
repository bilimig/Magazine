using System;
using System.Collections.Generic;

namespace Magazine.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
