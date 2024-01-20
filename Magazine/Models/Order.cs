using System;
using System.Collections.Generic;

namespace Magazine.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ClientId { get; set; }
        public DateTime? Date { get; set; }
        public int? StatusId { get; set; }
        public decimal? TotalValue { get; set; }
        public int? TypeId { get; set; }

        public virtual Client? Client { get; set; }
        public virtual OrderStatus? Status { get; set; }
        public virtual OrderType? Type { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
