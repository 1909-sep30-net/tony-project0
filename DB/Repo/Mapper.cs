using System;
using System.Collections.Generic;
using System.Text;
using DB.Entities;
//using DB.Entities;
using YourStore.Library;
using Products = YourStore.Library.Product;
using Store = YourStore.Library.Store;

namespace DB.Repo
{
    /// <summary>
    /// Maps an Entity Framework restaurant entity to a business model,
    /// including all reviews if present.
    /// </summary>
    class Mapper
    {
        public static YourStore.Library.Customer MapCustomer(Entities.Customers c)
        {
            YourStore.Library.Order ord = new YourStore.Library.Order();
   
            return new YourStore.Library.Customer
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Zip = c.Zip,
                Id = c.Id,
                Username = c.UserName,
                Pass = c.Pass,
            };
        }

        public static Entities.Customers MapCustomer(YourStore.Library.Customer c)
        {
            return new Entities.Customers
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Zip = c.Zip,
                UserName = c.Username,
                Pass = c.Pass,
                
            };
        }
        public static Entities.Stores MapStore(YourStore.Library.Store st)
        {
            Entities.Stores stor = new Entities.Stores();

            stor.StoreName = st.Name;
            stor.Id = st.StoreID;
            stor.Zip = st.Zip;
            
           /* foreach (YourStore.Library.Products p in st.ItemInventory.Keys)
            {
                
                Entities.Products products = Mapper.MapProduct(p);
                Entities.Inventories x = new Entities.Inventories();
                x.Product = products;
                x.Quantity = st.ItemInventory[p];
                stor.Inventories.Add(x);
                
            }*/
      
            return stor;
           
        }

        public static YourStore.Library.Store MapStore(Entities.Stores st)
        {
            YourStore.Library.Store store = new Store();
            store.Name = st.StoreName;
            store.StoreID = st.Id;
            store.Zip = st.Zip;
            YourStore.Library.Product products = new YourStore.Library.Product();

            foreach (Entities.Inventories inven in st.Inventories)
            {
                 products = Mapper.MapProduct(inven.Product);
                store.ItemInventory.Add(products, inven.Quantity);
            }


            return store;
        }

        public static Entities.Orders MapOrder(YourStore.Library.Order or)
        {
            Entities.Orders o= new Entities.Orders();
            if (or.CustomersID == 0)
            {
                o.CustomerId = or.Customer.Id;
            }
            else
            {
                o.CustomerId = or.CustomersID;
            }
            if (or.StoreID == 0)
            {
                o.StoreId = or.Store.StoreID;
            }
            else
            {
                o.StoreId = or.StoreID;
            }
            o.DateTimeOrder = or.Timer;
            return o;
        }
        public static YourStore.Library.Order MapOrder(Entities.Orders or)
        {
            YourStore.Library.Order o = new YourStore.Library.Order();


            o.Id = or.Id;
            
            o.Customer= MapCustomer(or.Customer);
            o.Store = MapStore(or.Store);
            o.Timer = (DateTime)or.DateTimeOrder;
            o.TotalCost = 0;
            foreach(Entities.OrderDetails od in or.OrderDetails)
            {
                o.Product.Add(MapProduct(od.Product), od.Quantity);
                o.TotalCost += (od.Product.Cost * od.Quantity);
            }
            return o;
        }

        public static Entities.OrderDetails MapOrderDetails(int p,int orderID,int Quantity)
        {
            Entities.OrderDetails od = new Entities.OrderDetails();
            Entities.Products prod = new Entities.Products();

       
            od.ProductId = p;
            od.OrderId = orderID;
            od.Quantity = Quantity;

            return od;
        }

        /// <summary>
        /// Creating a Inventory for a store in db if needed 
        /// </summary>
        /// <param name="q"></param>
        /// <param name="inv"></param>
        /// <param name="stores"></param>
        /// <returns></returns>
        public static Entities.Inventories MapInven(int q, YourStore.Library.Product p, Store stores)//old code
        {

          return  new Entities.Inventories
            {
                Quantity = q,

                Product = MapProduct(p),
                Store = MapStore(stores),
            };
              

        }

        /// <summary>
        /// My class library does not have an inventory. Therefore i will return the product and the quanity using the out keyword
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="quantities"></param>
        /// <returns></returns>
        public static YourStore.Library.Product MapInven(Entities.Inventories inv, out int quantities, out YourStore.Library.Store store )//old code
        {

            quantities = inv.Quantity;
            store = MapStore((inv.Store));

            return MapProduct(inv.Product);
        }


        public static YourStore.Library.Product MapProduct(  Entities.Products p )
        {
            return new YourStore.Library.Product
            {
                ID = p.Id,
                Cost = p.Cost,
                Name = p.ProudctName,
                imageLoc = p.ImagePath,
            };


        }
        public static Entities.Products MapProduct(YourStore.Library.Product p)
        {
            return new Entities.Products
            {
                Id = p.ID,
                Cost = p.Cost,
                ProudctName = p.Name,
                ImagePath = p.imageLoc,
                
            };


        }

        public static Employee MapEmployee(Employees emp)
        {
            Employee.RoleType x = (Employee.RoleType)emp.RoleId;
            return new Employee
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                s = MapStore(emp.Store),
                Username = emp.UserName,
                Pass = emp.Pass,
                RoleType1 = x,
                Id = emp.Id,
                Zip = emp.Zip,
            };
        
        }
        public static Entities.Employees MapEmployee(YourStore.Library.Employee emp)
        {
            return new Entities.Employees
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Store = MapStore(emp.s),
                UserName = emp.Username,
                Pass = emp.Pass,
                RoleId = (int)emp.RoleType1,
                Zip = emp.Zip,
                Id = emp.Id,
            };




        }
    }
}
