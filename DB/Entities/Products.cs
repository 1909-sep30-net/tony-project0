using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class Products
    {
        public Products()
        {
            Customers = new HashSet<Customers>();
            Inventories = new HashSet<Inventories>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int Id { get; set; }
        public string ProudctName { get; set; }
        public decimal Cost { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Inventories> Inventories { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
