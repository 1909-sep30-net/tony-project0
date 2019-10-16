using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using YourStore.Library.Model;
using YourStore.Library.Repo;

namespace XUnitTestProject0
{
    public class DataAccessTest
    {
        /// <summary>
        /// Making sure the input for the store name and 9 is in the correct format
        /// Current store code are D L J C
        /// </summary>
        [Fact]
        public void ValidateStoreTest( )
        {
            string s = "1";
            Stores st;
            bool actual = DataAccess.ValidateStore(s, out st);


   
            Assert.True(actual);
        }

        /// <summary>
        /// Testing the whole string as a whole before placing the order: 2 parts 1 for valid purchase and another invalid purchase
        /// </summary>
        [Fact]
        public void ValidatePurchase()
        {
            string s = "1 3";//valid purchase
           
            Stores st =DataAccess.GetAllStore().FirstOrDefault();
            string applicationMessage;
            Products product;
            int q;
            Dictionary<Products, int> inven = DataAccess.GetAllStore()[0].ItemInventory;
            Dictionary<Products, int> inven2 = DataAccess.GetAllStore()[1].ItemInventory;
            var p = inven.Keys.Where(k => k.ID == Char.GetNumericValue('0'));


            bool correct = DataAccess.ValidateString(s,DataAccess.GetAllStore().FirstOrDefault(),out applicationMessage,out product,out q);

        }


  
       





    }
}
