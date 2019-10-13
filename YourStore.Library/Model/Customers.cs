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
        
        public Stores PreferLocation { get; set; }

        public Products ProferProduct { get; set; }


        /// <summary>
        /// For quick initalization of data
        /// </summary>
        /// <param name="f"> first name</param>
        /// <param name="l">last name</param>
        /// <param name="zip"> zip code</param>
        public Customers(string f, string l, int zip)
        {
            this.FirstName = f;
            this.LastName = l;
            this.Zip = zip;
        }
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
     





    }
}
