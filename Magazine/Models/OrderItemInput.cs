﻿namespace Magazine.Models
{
    public class OrderItemInput
    {

        public int? ProductId { get; set; }
        public decimal? Price { get; set; }
        public int? Amount { get; set; }
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
