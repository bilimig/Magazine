using System;
using System.Collections.Generic;

namespace Magazine.Models
{
    public partial class Uom
    {
        public Uom()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
