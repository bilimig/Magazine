using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgazineInterface.Models
{
    class ProductHelper
    {
        public ProductHelper() { }
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? UomId { get; set; }
        public string? BaseUnit { get; set; }
        public int? Amount { get; set; }

    }
}
