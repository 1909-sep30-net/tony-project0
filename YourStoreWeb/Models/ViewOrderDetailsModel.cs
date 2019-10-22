using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourStore.Library;

namespace YourStoreWeb.Models
{
    public class ViewOrderDetailsModel    {
        public Dictionary<Product,int> Product { get; set; } = new Dictionary<Product,int>();
        public DateTime Date { get; set; } = new DateTime();
        public float Cost { get; set; }
        public Store store { get; set; }
        public string errorMessage { get; set; }


    }
}
