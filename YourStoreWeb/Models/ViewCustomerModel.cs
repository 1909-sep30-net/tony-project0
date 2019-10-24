using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourStore.Library;

namespace YourStoreWeb.Models
{
    public class ViewCustomerModel
    {
        public List<Customer> AllCus { get; set; } = new List<Customer>();

        public Customer Customer { get; set; } = new Customer();
        public string SearchCustomer { get; set; }=null;
        
        public string errorMessage { get; set; }
    }

}


