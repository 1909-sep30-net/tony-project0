using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using YourStore.Library.Model;
//using YourStore.Library.Repo;

namespace XUnitTestProject0
{/*
    public class UnitTest1
    {
        /// <summary>
        /// Making sure the input for the store name and 9 is in the correct format
        /// Current store code are D L J C
        /// </summary>
        [Fact]
        public void ValidateStoreTest( )
        {
            string s = "d";
            Stores st;
            bool actual = DataAccess.ValidateStore(s[0], out st);  

            
            Assert.True(actual);
        }
        /// <summary>
        /// Testing if the item is correct from the store 
        /// the test input is: D1 xxx
        /// </summary>
        [Fact]
        public void ValidateProductTest()
        {

            Products st;
            bool actual = DataAccess.ValidateProduct('0', DataAccess.GetAllStore()[0].ItemInventory, out st);
            Dictionary<Products, int> inven = DataAccess.GetAllStore()[0].ItemInventory;
            var p = inven.Keys.Where(k => k.ID == Char.GetNumericValue('0'));
            Assert.Equal(p.First(), st);
            Assert.True(actual);
        }
        /// <summary>
        /// Testing the whole string as a whole before placing the order: 2 parts 1 for valid purchase and another invalid purchase
        /// </summary>
        [Fact]
        public void ValidatePurchase()
        {
            string s = "D0 3";//valid purchase
           
            Stores st;
            string applicationMessage;
            Products product;
            int q = 0;
    
            Dictionary<Products, int> inven = DataAccess.GetAllStore()[0].ItemInventory; 
            Dictionary<Products, int> inven2 = DataAccess.GetAllStore()[1].ItemInventory;
            var p = inven.Keys.Where(k => k.ID == Char.GetNumericValue('0'));


            bool correct = DataAccess.ValidateString(s, out applicationMessage, out st, out product, out q);
            Console.WriteLine(applicationMessage);


            Assert.Equal(DataAccess.GetAllStore()[0], st);
            Assert.Equal(p.First(), product);
            Assert.Equal(inven[p.First()], q);
            Assert.True(correct);

            string s1 = "L1 5"; //cant purchase due to quanlity is over

            Stores st1;
            string applicationMessage1;
            Products product1;
            int q1 = 0;
            var p1 = inven2.Keys.Where(k => k.ID == Char.GetNumericValue('1'));

            bool correct2 = DataAccess.ValidateString(s1, out applicationMessage1, out st1, out product1, out q1);

            Assert.True(!correct2);
            Assert.Equal(DataAccess.GetAllStore()[1], st1);
            Assert.Equal(p1.First(), product1);


        }
        /// <summary>
        /// Testing if we added the order successfully
        /// </summary>
        [Fact]
        public void AddToOrderTest()
        {
            string x = "d0 1";
            Orders curO = null;

            string applicationMessage = null;
            bool correct = DataAccess.AddToOrder(x, out applicationMessage,out curO);

            Assert.True(correct);
            Assert.Equal("Your item is placed in the order!", applicationMessage);

        }
        /// <summary>
        /// Testing if we can get all store.
        /// </summary>
        [Fact]
        public void GettingAllStoreListTest()
        {
            List<Stores> x = DataAccess.GetAllStore();

            Assert.NotNull(x);
            Assert.Equal((int)4, x.Count());
        }
        

        /// <summary>
        /// Testing if i can create/ get a customer
        /// </summary>
        [Fact]
        public void GetCustomerTest()
        {
            Assert.Null(DataAccess.GetCustomer());
            DataAccess.CreateCustomer("f", "l", "3");
            Assert.NotNull(DataAccess.GetCustomer());
            Assert.Equal("f", DataAccess.GetCustomer().First);
        }

        /// <summary>
        /// Customer order are finished when there is a timer in the order. 
        /// This test if the order has a timer type in the order. 
        /// </summary>
        [Fact]
        public void FinishOrdering()
        {
            string message1= null;
            Orders curO = null;
            DataAccess.AddToOrder("d1 1", out message1,out curO);
            DataAccess.FinishOrdering();
            Assert.IsType<DateTime>(curO.Timer);

        }



    }*/
}
