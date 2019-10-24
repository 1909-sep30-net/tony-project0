using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YourStore.Library;

namespace YourStoreWeb.Models
{
    public class ViewStoreProductModel
    {
        public List<Store> Store { get; set; } = new List<Store>();
        //public List<Dictionary<Product, int>> Inven { get; set; } = new List< Dictionary<Product, int>>();

        [Required]
        [RegularExpression(@"[1-99""'\s-]$", ErrorMessage = "The input must be 1 -xx numbers")]
        public int StoreID { get; set; }

        public string errormessage;

    }
}
