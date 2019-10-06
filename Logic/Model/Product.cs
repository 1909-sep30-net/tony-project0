using System;
using System.Collections.Generic;
using System.Text;

namespace YourStore.Library.Model
{
    public class Product
    {
        public string Name { get; set; }
        public float Cost { get; set; }

        public int ID { get; set; }
        /// <summary>
        /// This constructor helps create massive database of items
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost"></param>
        /// <param name="ID"></param>
        public Product(string name, float cost,int ID)
        {
            Name = name;
            Cost = cost;
            this.ID = ID;
        }


        public bool Restockable()
        {
            return true;
        }
        public void Refill()
        {

        }



    }
}
