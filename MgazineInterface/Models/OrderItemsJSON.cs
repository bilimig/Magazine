using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MgazineInterface.Models
{
    class OrderItemsJSON
    {
        public OrderItemsJSON() { }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public int? OrderId { get; set; }

        public virtual OrdersJSON? Order { get; set; }
        public virtual ProductsJSON? Product { get; set; }
    }
}
