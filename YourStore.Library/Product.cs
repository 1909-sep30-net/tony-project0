using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YourStore.Library
{/// <summary>
/// This class describle the product that is for sale
/// </summary>
    public class Product
    {
        [Display(Name = "Product Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The Input Must be letters"), Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Display(Name = "Product Price")]
        [RegularExpression(@"[0-9""'\s-]$", ErrorMessage = "The input must be 0- 9 numbers")]
        public decimal Cost { get; set; }
        public int ID { get; set; }
        public string imageLoc { get; set; }

       

    }
}
