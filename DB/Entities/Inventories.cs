using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class Inventories
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int StoreId { get; set; }

        public virtual Products Product { get; set; }
        public virtual Stores Store { get; set; }
    }
}
