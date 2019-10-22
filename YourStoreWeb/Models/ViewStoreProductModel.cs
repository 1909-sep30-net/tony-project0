using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourStore.Library;

namespace YourStoreWeb.Models
{
    public class ViewStoreProductModel
    {
        public List<Store> Store { get; set; } = new List<Store>();
        //public List<Dictionary<Product, int>> Inven { get; set; } = new List< Dictionary<Product, int>>();


        public int StoreID { get; set; }


    }
}
