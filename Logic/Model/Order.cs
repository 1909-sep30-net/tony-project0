using System.Collections.Generic;

namespace Logic
{
    public class Order
    {
        internal Customer Customer{ get; set; }
        public float Timer { get; set; }
        public List<Product> product { get; set; }




    }
}