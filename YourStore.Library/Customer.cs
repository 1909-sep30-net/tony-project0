using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YourStore.Library
{  /// <summary>
   /// A customer has their first and last name, and their location 
   /// </summary>
    public class Customer:IUser
    {

        public int Id { get; set; }

        /// <summary>
        /// The first name of the customer
        /// </summary>
        [Display(Name = "First Name")]

        [RegularExpression(@"^[a-zA-Z]+$" ,ErrorMessage = "The Input Must be letters"), Required(AllowEmptyStrings =false)]

        public String FirstName { get; set; }

        /// <summary>
        /// The Last name of the customer
        /// </summary>
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The Input Must be letters"), Required(AllowEmptyStrings = false)]
        [Display(Name = "Last Name")]

        public String LastName { get; set; }
        

        [RegularExpression(@"[0-9""'\s-]$", ErrorMessage = "The input must be 0- 9 numbers"), Required]
        public int Zip { get; set; }


        public List<Order> Order { get; set; } = new List<Order>();
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]


        public string Pass { get; set; }
    }
}
