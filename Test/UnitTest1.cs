using Logic;
using NUnit.Framework;
using System;
using Xunit;

namespace Test
{
    public class UnitTest1
    {
        /// <summary>
        /// Checking if the customer name is first letter cap and correcting them
        /// </summary>
        [Theory]
        public void checkCustomerFirstandLastName(string a)
        {

            string b = "tony";
            Customer customer = new Customer();
            customer.First = b;

            Assert.Equals("Tony", customer.First);
        }
    }
}
