using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{
   public class Store
    {


        public Store(string v1, int v2, Dictionary<Product, int> gameConsoleList2, string v)
        {
            this.Name = v1;
            this.Location = v2;
            this.ItemInventory = gameConsoleList2;
            this.StoreID = v;
        }

        public string Name { get; set; }
        public string StoreID { get; set; }
        public int Location { get; set; }
        //logging 
        public List<Order> UserOrder { get; set; }

        public Dictionary<Product, int> ItemInventory { get; set; }

    }
}
