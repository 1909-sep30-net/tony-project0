using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourStore.Library
{
   
    public class Employee : IUser
    {
        public enum RoleType
        {
            Ceo = 1,
            Admin = 2,
            StoreManager = 3
        }
        [Display(Name ="First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The Input Must be letters"), Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        public int Id { get; set; }
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The Input Must be letters"), Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required]     
        public Store Store { get; set; } = new Store();
        [Required]
        [RegularExpression(@"[0-9""'\s-]$", ErrorMessage = "The input must be 0- 9 numbers")]

        public int Zip { get; set; }
        [Required]

        public RoleType RoleType1{ get; set;}
        [Required]

        public string Username { get; set; }
        [Required]

        public string Pass { get ; set; }
    }
}
