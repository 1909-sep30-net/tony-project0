using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{
    public class Orders
    {

        public Customers Customer { get; set; }
        public Stores Store{get;set;}

        public DateTime Timer { get; set; }
        public Dictionary<Products, int> Product { get; set; } = new Dictionary<Products, int>();
    }
}
