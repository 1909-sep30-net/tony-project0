using System;
using Xunit;
using YourStore.Library.Repo;

namespace XUnitTestProject0
{
    public class UnitTest1
    {
        /// <summary>
        /// Making sure the input for the store name and 9 is in the correct format
        /// </summary>
        [Fact]
        public void ValidateStringInput()
        {
            string input="j9";
            string input1 = "nn";
           
            Assert.True(DataAccess.ValdateString(input));
          //  Assert

        }
    }
}
