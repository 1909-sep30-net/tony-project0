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
                _fName = value;
                if (!Char.IsDigit(_fName[0]))
                {
                    Char.ToUpper(_fName[0]);
                }

            }

        }
        /// <summary>
        /// The Last name of the customer
        /// </summary>
        public String LastName
        {
            get => _lName; set
            {
                _lName = value;
                if (!Char.IsDigit(_lName[0]))
                {
                    Char.ToUpper(_lName[0]);
                }

            }
        }

        public int Zip { get; set; }
     

        public List<Orders> Order { get; set; }



    }
}
