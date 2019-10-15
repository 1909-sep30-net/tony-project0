using System;
using System.Collections.Generic;
using System.Text;
//using DB.Entities;
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
            YourStore.Library.Model.Orders ord = new YourStore.Library.Model.Orders();
   
            return new YourStore.Library.Model.Customers
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Zip = c.Zip,
                Id = c.Id,
            };
        }

        public static Entities.Customers MapCustomer(YourStore.Library.Model.Customers c)
        {
            return new Entities.Customers
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Zip = c.Zip,
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
                Entities.Inventories x = new Entities.Inventories();
                x.Product = products;
                x.Quantity = st.ItemInventory[p];
                stor.Inventories.Add(x);
                
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
                store.ItemInventory.Add(products, inven.Quantity);
            }
            /*
            foreach (Entities.Orders ord in st.Orders)
            {
                YourStore.Library.Model.Orders o = new Orders();
                if(ord.Customer!=null)
                o.Customer = MapCustomer(ord.Customer);
                foreach( Entities.OrderDetail ode in ord.OrderDetail)
                {
                    o.Product.Add(MapProduct(ode.Product), ode.Quantity);
                }

                store.UserOrderHistory.Add(o);
            }*/

            return store;
        }
        /// <summary>
        /// Mapping object oder to relational entities
        /// </summary>
        /// <param name="ord"></param>
        /// <returns></returns>

      /*  public static YourStore.Library.Model.Orders MapOrder(Entities.Orders ord)
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

        }*/
        public static Entities.Orders MapOrder(YourStore.Library.Model.Orders or)
        {
            Entities.Customers c = MapCustomer(or.Customer);
            Entities.Orders o= new Entities.Orders();
            
            o.Customer = c;
            o.StoreId = or.Store.StoreID;
            o.DateTimeOrder = or.Timer;
            return o;
        }
        public static YourStore.Library.Model.Orders MapOrder(Entities.Orders or)
        {
            YourStore.Library.Model.Orders o = new YourStore.Library.Model.Orders();


            o.Id = or.Id;
            
            o.Customer= MapCustomer(or.Customer);
            o.Store = MapStore(or.Store);
            o.Timer = (DateTime)or.DateTimeOrder;
            o.TotalCost = 0;
            foreach(Entities.OrderDetail od in or.OrderDetail)
            {
                o.Product.Add(MapProduct(od.Product), od.Quantity);
                o.TotalCost += (od.Product.Cost * od.Quantity);
            }
            return o;
        }

        public static Entities.OrderDetail MapOrderDetails(YourStore.Library.Model.Products p, YourStore.Library.Model.Orders o, int Quantity)
        {
            Entities.OrderDetail od = new Entities.OrderDetail();
            Entities.Products prod = new Entities.Products();

            prod.Cost = p.Cost;
            prod.ProudctName = p.Name;
            od.Product = prod;
            od.OrderId = o.Id;
            od.Quantity = Quantity;

            return od;
        }


        public static Entities.Inventories MapInven(int q, Entities.Inventories inv)
        {
            
            
           inv.Quantity= q;           

            return inv;
        }
     


        public static YourStore.Library.Model.Products MapProduct(  Entities.Products p )
        {
            return new YourStore.Library.Model.Products
            {
                ID = p.Id,
                Cost = p.Cost,
                Name = p.ProudctName        

            };


        }
        public static Entities.Products MapProduct(YourStore.Library.Model.Products p)
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
