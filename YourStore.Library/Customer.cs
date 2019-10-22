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
        private string _fName = null;
        private string _lName = null;
        public int Id { get; set; }
        public Store PreferLocation { get; set; } = new Store();

        public Product ProferProduct { get; set; } = new Product();



        /// <summary>
        /// defaut constructor
        /// </summary>
        public Customer()
        {

        }
        /// <summary>
        /// The first name of the customer
        /// </summary>
        [RegularExpression(@"^[a-zA-Z]+$" ,ErrorMessage = "The Input Musssat be letters"), Required]

        public String FirstName
        {
            get => _fName; set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Name must not be empty ", nameof(value));
                }
                _fName = value;


            }

        }
        /// <summary>
        /// The Last name of the customer
        /// </summary>
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "The Input Musssat be letters"), Required]

        public String LastName
        {
            get => _lName; set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Name must not be empty ", nameof(value));
                }
                _lName = value;

            }
        }

        [RegularExpression(@"[0-9""'\s-]$", ErrorMessage = "The input must be 10 numbers"), Required]
        public int Zip { get; set; }


        public List<Order> Order { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
    }
}
