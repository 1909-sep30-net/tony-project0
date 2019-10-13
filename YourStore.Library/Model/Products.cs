using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{/// <summary>
/// This class describle the product that is for sale
/// </summary>
    public class Products 
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// This constructor helps create massive database of items
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost"></param>
        /// <param name="ID"></param>
        public Products(string name, decimal cost, int ID)
        {
            Name = name;
            Cost = cost;
            this.ID = ID;
        }
        public Products()
        {
        
        }


    }
}
