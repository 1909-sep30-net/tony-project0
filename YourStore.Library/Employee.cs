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
        [Required][Display(Name ="First Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string FirstName { get; set; }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string LastName { get; set; }
        [Required]     
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public Store s { get; set; } = new Store();
        [Required]
        public int Zip { get; set; }
        [Required]

        public RoleType RoleType1{ get; set;}
        [Required]

        public string Username { get; set; }
        [Required]

        public string Pass { get ; set; }
    }
}
