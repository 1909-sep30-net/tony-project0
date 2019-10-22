using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using YourStore.Library;
using YourStore.Library.Repo;

namespace XUnitTestProject0
{
    public class OrderTest
    {
        private readonly Order o = new Order();
        /// <summary>
        /// Testing if the dictionary is null(aka inventory is null) Fail if it is null;
        /// </summary>
        [Fact]
      
        public void OrdersTestDictionaryIs_Empty()
        {
            Assert.NotNull(o.Product);
        }
        /// <summary>
        /// Testing the rule class,which it isnt should be null;
        /// </summary>
        [Fact]
        public void OrdersTestRulesIs_Empty()
        {
            Assert.NotNull(o.Product);
        }



    }
}
