using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{
    public class Orders
    {
       
        public Customers Customer { get; set; }
        public int CustomersID { get; set; }
        public Stores Store{get;set;}
        public int StoreID { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime Timer { get; set; }
        public int Id { get; set; }
        public Dictionary<Products, int> Product { get; set; } = new Dictionary<Products, int>();
        public Rules Rules { get; set; } = new Rules();
    }
}

