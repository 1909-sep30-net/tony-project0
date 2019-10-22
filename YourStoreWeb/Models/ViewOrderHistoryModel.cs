using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourStore.Library;

namespace YourStoreWeb.Models
{
    public class ViewOrderHistoryModel
    {
        public List<Order> Order { get; set; } = new List<Order>();
        public List<Store> Store { get; set; } = new List<Store>();
        public List<Order> CusOrdHistory { get; set; } = new List<Order>();

        public Order  OrderDetail = new Order();
        public int OrderID { get; set; }
        public string SearchOrder { get; set; }
        public string SearchOrderDetail { get; set; }
        public string SearchCustomerHistory { get; set; }

    }
}
