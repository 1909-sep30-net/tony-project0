using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{
    public class Order
    {
        public Customer Customer { get; set; }
        public float Timer { get; set; }
        public List<Product> product { get; set; }
    }
}
