using System;
using System.Collections.Generic;
using System.Text;
using DB.Entities;
using YourStore.Library.Model;
using Products = YourStore.Library.Model.Products;
using Stores = YourStore.Library.Model.Stores;

namespace DB.Repo
{
    /// <summary>
    /// Maps an Entity Framework restaurant entity to a business model,
    /// including all reviews if present.
    /// </summary>
    class Mapper
    {
        public static YourStore.Library.Model.Customers MapCustomer(Entities.Customers c)
        {
            return new YourStore.Library.Model.Customers
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Zip = c.Zip
            };
        }

        public static Entities.Customers MapCustomer(YourStore.Library.Model.Customers c)
        {
            return new Entities.Customers
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Zip = c.Zip

            };
        }
        public static Entities.Stores MapStore(YourStore.Library.Model.Stores st)
        {
            Entities.Stores stor = new Entities.Stores();

            stor.StoreName = st.Name;
            stor.Id = st.StoreID;
            stor.Zip = st.Zip;

            foreach (YourStore.Library.Model.Products p in st.ItemInventory.Keys)
            {
                
                Entities.Products products = Mapper.MapProduct(p);
                foreach (Entities.Inventories inven in stor.Inventories)
                {
                    if (p.ID == inven.ProductId)
                    {
                        inven.Quantity = st.ItemInventory[p];
                    }
                }
            }
            foreach (YourStore.Library.Model.Orders o in st.UserOrderHistory)
            {

                Entities.Orders ord = Mapper.MapOrder(o);
                

            }
            return stor;
           
        }

        public static YourStore.Library.Model.Stores MapStore(Entities.Stores st)
        {
            YourStore.Library.Model.Stores store = new Stores();
            store.Name = st.StoreName;
            store.StoreID = st.Id;
            store.Zip = st.Zip;
            YourStore.Library.Model.Products products = new YourStore.Library.Model.Products();

            foreach (Entities.Inventories inven in st.Inventories)
            {
                 products = Mapper.MapProduct(inven.Product);
                Console.WriteLine($"mapping {products.Name}");
                store.ItemInventory.Add(products, inven.Quantity);
            }

            foreach (Entities.Orders ord in st.Orders)
            {
                store.UserOrderHistory.Add(Mapper.MapOrder(ord));
            }

            return store;
        }
        /// <summary>
        /// Mapping object oder to relational entities
        /// </summary>
        /// <param name="ord"></param>
        /// <returns></returns>

        private static YourStore.Library.Model.Orders MapOrder(Entities.Orders ord)
        {
            YourStore.Library.Model.Orders o = new YourStore.Library.Model.Orders();
            o.Customer = MapCustomer(ord.Customer);
            o.Store = MapStore(ord.Store);
            o.Timer =(DateTime) ord.DateTimeOrder;
            foreach ( Entities.OrderDetail od in ord.OrderDetail)
            {
                o.Product.Add(MapProduct(od.Product), od.Quantity);

            }
            return o;

        }
        private static Entities.Orders MapOrder(YourStore.Library.Model.Orders or)
        {
            Entities.Orders o= new Entities.Orders();
            Entities.OrderDetail od = new Entities.OrderDetail();

            foreach(YourStore.Library.Model.Products p in or.Product.Keys)
            {
                od.Product = MapProduct(p);
                od.Quantity = or.Product[p];

            }

            o.OrderDetail =(ICollection<OrderDetail>) od;
            o.Store = Mapper.MapStore(or.Store);
            o.DateTimeOrder = or.Timer;
            return o;
        } 

        public static YourStore.Library.Model.Products MapProduct( Entities.Products p )
        {
            return new YourStore.Library.Model.Products
            {
                ID = p.Id,
                Cost = p.Cost,
                Name = p.ProudctName        

            };


        }
        public static Entities.Products MapProduct(Products p)
        {
            return new Entities.Products
            {
                Id = p.ID,
                Cost = p.Cost,
                ProudctName = p.Name
            };


        }

    }
}
