using System;
using System.Collections.Generic;

namespace Magazine.Models
{
    public partial class ContactDetail
    {
        public ContactDetail()
        {
            Clients = new HashSet<Client>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
