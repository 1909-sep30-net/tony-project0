using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library
{
    public class Order
    {
       
        public Customer Customer { get; set; }
        public int CustomersID { get; set; }
        public Store Store{get;set;}
        public int StoreID { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime Timer { get; set; }
        public int Id { get; set; }
        public Dictionary<Product, int> Product { get; set; } = new Dictionary<Product, int>();
        public Rule Rules { get; set; } = new Rule();
    }
}

