using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DB.Entities
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }



        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int Zip { get; set; }
        public int? PreferLocationId { get; set; }
        public int? PreferProductId { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }

        public virtual Stores PreferLocation { get; set; }
        public virtual Products PreferProduct { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
