using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{  /// <summary>
   /// A customer has their first and last name, and their location 
   /// </summary>
    public class Customer
    {
        private string _fName;
        private string _lName;


        /// <summary>
        /// For quick initalization of data
        /// </summary>
        /// <param name="f"> first name</param>
        /// <param name="l">last name</param>
        /// <param name="zip"> zip code</param>
        public Customer(string f, string l, int zip)
        {
            this.First = f;
            this.Last = l;
            this.Zip = zip;
        }
        /// <summary>
        /// defaut constructor
        /// </summary>
        public Customer()
        {

        }
        /// <summary>
        /// The first name of the customer
        /// </summary>
        public String First
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
        public String Last
        {
            get => _lName; set
            {
                _fName = value;
                if (!Char.IsDigit(_lName[0]))
                {
                    Char.ToUpper(_lName[0]);
                }

            }
        }

        public int Zip { get; set; }
        public String PreferLocation { get; set; }

        public List<Product> FavoriteProductList = new List<Product>();




    }
}
