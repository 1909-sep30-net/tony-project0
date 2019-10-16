using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{  /// <summary>
   /// A customer has their first and last name, and their location 
   /// </summary>
    public class Customers
    {
        private string _fName = null;
        private string _lName = null;
        public int Id { get; set; }
        public Stores PreferLocation { get; set; } = new Stores();

        public Products ProferProduct { get; set; } = new Products();


       
        /// <summary>
        /// defaut constructor
        /// </summary>
        public Customers()
        {

        }
        /// <summary>
        /// The first name of the customer
        /// </summary>
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

        public int Zip { get; set; }
     

        public List<Orders> Order { get; set; }



    }
}
