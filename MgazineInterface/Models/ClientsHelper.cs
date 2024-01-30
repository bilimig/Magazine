using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MgazineInterface.Models;

namespace MgazineInterface.Models
{
    public partial class ClientsHelper
    {
        public ClientsHelper() { }

        public int Id { get; set; }
        public ContactDetailsJSON details { get; set; }
    }
}
