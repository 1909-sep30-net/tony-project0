using System;
using System.Collections.Generic;

namespace DB.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public DateTime? DateTimeOrder { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Stores Store { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
