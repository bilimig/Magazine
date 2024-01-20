using System;
using System.Collections.Generic;

namespace Magazine.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? UomId { get; set; }
        public string? BaseUnit { get; set; }
        public int? Amount { get; set; }

        public virtual Uom? Uom { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
