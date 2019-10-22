using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using YourStore.Library;
using YourStore.Library.Repo;

namespace XUnitTestProject0
{
    public class ClassCustomerTest
    {
        private readonly Customer c = new Customer();
        /// <summary>
        /// Testing if it set name in the correct format
        /// </summary>
        [Fact]
        public void  NameTestifStoreCorrectly()
        {
            Customer c = new Customer();
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
