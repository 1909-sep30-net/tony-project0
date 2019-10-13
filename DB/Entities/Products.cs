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
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string ProudctName { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Inventories> Inventories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
