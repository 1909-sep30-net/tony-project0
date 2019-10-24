using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YourStore.Library
{
   public class Store
    {
        [Display(Name = "Store Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The Input Must be letters"), Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public int StoreID { get; set; }
        public int Zip { get; set; }

        public Dictionary<Product, int> ItemInventory { get; set; } = new Dictionary<Product, int>();

        public List<Order> UserOrderHistory { get; set; } = new List<Order>();


    }
}
