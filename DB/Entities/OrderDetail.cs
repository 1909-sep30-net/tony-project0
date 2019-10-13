using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class OrderDetail
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int Id { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Products Product { get; set; }
    }
}
