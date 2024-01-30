using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgazineInterface.Models
{
    class OrderItemsHelper
    {
        public OrderItemsHelper() { }
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public int? OrderId { get; set; }
    }
}
