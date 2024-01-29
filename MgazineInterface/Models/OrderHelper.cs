using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgazineInterface.Models
{
    class OrdersHelper
    {
        public OrdersHelper() { }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ClientId { get; set; }
        public DateTime? Date { get; set; }
        public int? StatusId { get; set; }
        public decimal? TotalValue { get; set; }
        public int? TypeId { get; set; }

    }
}
