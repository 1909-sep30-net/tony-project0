using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{
   public class Stores
    {
        public Stores()
        {
        }

        public Stores(string v1, int v2, Dictionary<Products, int> pList, int v)
        {
            this.Name = v1;
            this.Zip = v2;
            this.ItemInventory = pList;
            this.StoreID = v;
        }

        public string Name { get; set; }
        public int StoreID { get; set; }
        public int Zip { get; set; }

        public Dictionary<Products, int> ItemInventory { get; set; } = new Dictionary<Products, int>();

        public List<Orders> UserOrderHistory { get; set; }


    }
}
