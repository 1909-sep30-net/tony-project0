using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{
    class Inventories
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Stores store { get; set; }
        public Products products { get; set; }
    }
}
