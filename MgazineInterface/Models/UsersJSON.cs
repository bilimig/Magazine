using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgazineInterface.Models
{
    class UsersJSON
    {
        public UsersJSON() { }
        public int? DetailsId { get; set; }
        public string? Password { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
