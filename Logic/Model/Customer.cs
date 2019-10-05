using System;

namespace Logic
{
    /// <summary>
    /// A customer has their first and last name, and their location 
    /// </summary>
    public class Customer
    {
        private string _fName;
        private string _lName;
        private int _zipCode;

        /// <summary>
        /// 
        /// </summary>
        public String First 
        {
            get=> _fName; set {
                _fName= value;
                if (!Char.IsDigit(_fName[0]))
                {
                    Char.ToUpper(_fName[0]);
                }

            }
                
         }
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
public String PreferLocation { get; set; } 



    }
}
