using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using YourStore.Library;
using YourStore.Library.Repo;

namespace XUnitTestProject0
{
    public class StoreTest
    {
        Store s = new Store();
        public void ItemInvenDictionaryIs_Empty()
        {
            Assert.NotNull(s.ItemInventory);
        }

        public void UserOrderHistoryDictionaryIs_Empty()
        {
            Assert.NotNull(s.ItemInventory);
        }


    }
}
