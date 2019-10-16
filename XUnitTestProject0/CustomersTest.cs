using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using YourStore.Library.Model;
using YourStore.Library.Repo;

namespace XUnitTestProject0
{
    public class ClassCustomerTest
    {
        private readonly Customers c = new Customers();
        /// <summary>
        /// Testing if it set name in the correct format
        /// </summary>
        [Fact]
        public void  NameTestifStoreCorrectly()
        {
            Customers c = new Customers();
            string x = "ssss";

            c.FirstName = x;
            Assert.Equal("ssss", c.FirstName);
        }
        [Fact]
        public void Name_EmptyValue_ThrowsArgumentException()
        {
            Assert.ThrowsAny<ArgumentException>(() => c.FirstName = string.Empty);
        }


    }
}
