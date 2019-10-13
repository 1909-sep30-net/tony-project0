using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class Stores
    {
        public Stores()
        {
            Customers = new HashSet<Customers>();
            Inventories = new HashSet<Inventories>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public int Zip { get; set; }
        public string StoreName { get; set; }

        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Inventories> Inventories { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
