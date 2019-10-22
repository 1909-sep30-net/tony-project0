using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library
{
   public class Store
    {
        public Store()
        {
        }

        public Store(string v1, int v2, Dictionary<Product, int> pList, int v)
        {
            this.Name = v1;
            this.Zip = v2;
            this.ItemInventory = pList;
            this.StoreID = v;
        }

        public string Name { get; set; }
        public int StoreID { get; set; }
        public int Zip { get; set; }

        public Dictionary<Product, int> ItemInventory { get; set; } = new Dictionary<Product, int>();

        public List<Order> UserOrderHistory { get; set; } = new List<Order>();


    }
}
