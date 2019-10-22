using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YourStore.Library;
using static YourStore.Library.Employee;

namespace YourStoreWeb.Models
{
    public class LoginModel
    {

      
        [Required]
        public string Username { get; set; }
        [Required]

        public string Pass { get; set; }
        public bool logged { get; set; } = false;
    }
}
