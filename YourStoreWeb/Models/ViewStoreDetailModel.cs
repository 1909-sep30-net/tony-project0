using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YourStore.Library;

namespace YourStoreWeb.Models
{
    public class ViewStoreDetailModel
    {
        public Store Store { get; set; } = new Store();
        public ViewOrderDetailsModel ViewOrderModel { get; set; } = new ViewOrderDetailsModel();

        [Required]
        [RegularExpression(@"[0-9""'\s-]$", ErrorMessage = "The input must be positive numbers")]
        public int ProductID { get; set; }
        [Required]
        [RegularExpression(@"[0-9""'\s-]$", ErrorMessage = "The input must be positive numbers")]
        public int Quantity { get; set; }
        [Required]
        public string  errorMessage { get; set; }
    }
}
