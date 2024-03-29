﻿using DB;
using DB.Entities;
using DB.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourStore.Library;
using Stores = DB.Entities.Stores;

namespace YourStore.Library.Repo
{
    /// <summary>
    /// This class allow us to communicate with database and user interface.
    /// </summary>
    public class RepoBusLogic:IDisposable, IRepoBusLogic //new code
    {
        #region OldCode
        /*
            /// <summary>
            /// storing a list of all important objects from the database for later use;
            /// </summary>
            static private List<Model.Customer> _allCustomers = new List<Model.Customer>();
            static private List<Model.Stores> _allStores = new List<Model.Stores>();
            static private List<Model.Orders> _allOrders = new List<Model.Orders>();

            /// <summary>
            /// this is used to keep track which is the current customer  and order
            /// </summary>       
            static private Model.Orders custOrder;
            static public Model.Customer CurrentCustomer;

            /// <summary>
            /// this is used to push/retrieve information to/from database
            /// </summary>
            static private readonly Proj1Context dbContext;



            public static List<Model.Orders> GetAllOrders()
            {
                return _allOrders;
            }
            /// <summary>
            /// setting up store and products for uses 
            /// </summary>
            static RepoBusLogic()
            {
                var optionsBuilder = new DbContextOptionsBuilder<Proj1Context>();
                optionsBuilder.UseSqlServer(SecretConfiguration.connectionString);

                dbContext = new Proj1Context(optionsBuilder.Options);

                UpdateObjects();

            }

            /// <summary>
            /// This is to validate store code.
            /// </summary>
            /// <param name="y"> is the store code</param>
            /// <param name="st"> the store that is going to contain the code</param>
            /// <returns></returns>
            public static bool ValidateStore(string y, out Model.Stores store)
            {
                int y1 = int.Parse(y);

                var x = from store1 in _allStores where store1.StoreID == y1 select store1;


                if (x == null)
                {

                    store = null;
                    return false;
                }

                store = x.First();

                return true;
            }
            /// <summary>
            /// THis is Valdiate Product code in the store
            /// </summary>
            /// <param name="y"></param>
            /// <param name="dict"></param>
            /// <param name="p"></param>
            /// <returns></returns>
            public static bool ValidateProduct(string y, Dictionary<Model.Products, int> dict, out Model.Products p)
            {
                var x = dict.Keys.Where(k => k.ID == int.Parse(y)).FirstOrDefault();

                if (x == null)
                {
                    p = null;
                    return false;

                }
                p = x;
                return true;
            }
            /// <summary>
            /// Removing current customer 
            /// </summary>
            public static void ClearCurrentCustomer()
            {
                CurrentCustomer = null;
            }
            /// <summary>
            /// change login by searching _allCustomer
            /// </summary>
            /// <param name="f">first name</param>
            /// <param name="l">last name</param>
            /// <param name="id">id </param>
            /// <returns>true and false</returns>
            public static bool ChangeLogin(string f, string l, int id)
            {
                CurrentCustomer = null;
                CurrentCustomer = _allCustomers.Where(x => x.FirstName == f && x.LastName == l && x.Id == id).FirstOrDefault();
                if (CurrentCustomer == null)
                {
                    return false;
                }
                return true;
            }



            /// <summary>
            /// Valdating the input string before placing order
            /// </summary>
            /// <param name="j"></param>
            /// <param name="applicationMessage"></param>
            /// <returns></returns>
            static public bool ValidateString(string j, YourStore.Library.Stores st, out string applicationMessage, out Model.Products product, out int q)
            {
                Console.WriteLine("dest");
                string[] ssize = j.Split(null);
                if ((ssize.Length < 3))
                {
                    // Product product;
                    if (!ValidateProduct(ssize[0], st.ItemInventory, out product))
                    {
                        applicationMessage = "There is no such item in store";
                        //  product = null;
                        q = 0;
                        return false;
                    }
                    // int q;
                    if (int.TryParse(ssize[1], out q))
                    {
                        if (q > st.ItemInventory[product])
                        {
                            applicationMessage = ("Your quantity is greater than the inventory");
                            return false;
                        }
                    }
                    else
                    {
                        applicationMessage = ("Your quantity is not recognizable as English.");

                        return false;

                    }



                    applicationMessage = ("Your item is placed in the order!");
                    return true;


                }
                applicationMessage = ("To add an item to your store please type (StoreCode + ProductCode Quantity) ex: 5 1 199!");
                product = null;
                q = 0;
                return false;
            }
            /// <summary>
            /// Returning all orders to console for displaying
            /// </summary>
            /// <returns></returns>
            public static List<Model.Orders> DisplayAllOrders()
            {
                return _allOrders;
            }
            /// <summary>
            /// it searches customer by id 
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static YourStore.Library.Customer SearchCustomerById(int code)
            {

                return _allCustomers.Where(a => a.Id == code).FirstOrDefault();
            }



            /// <summary>
            /// viewing all customers 
            /// </summary>
            /// <returns></returns>
            public static List<YourStore.Library.Customer> ViewAllCustomer()
            {

                return _allCustomers;
            }






            /// <summary>
            /// Persist changes to the data source.
            /// </summary>
            public void Save()
            {
                //            s_logger.Info("Saving changes to the database");
                dbContext.SaveChanges();
            }
            /// <summary>
            /// This will create a customer and add it to the list of customers
            /// </summary>
            /// <param name="f"></param>
            /// <param name="l"></param>
            /// <param name="zip"></param>
            /// <returns></returns>
            public static bool CreateCustomer(string f, string l, string zip)
            {
                Model.Customer c = new Model.Customer();
                c.FirstName = f;
                c.LastName = l;
                if (c.Id == 0)
                {
                    c.Id = _allCustomers.Count + 1;
                }

                int result;

                if (!int.TryParse(zip, out result))
                {
                    return false;
                }
                c.Zip = result;

                CurrentCustomer = c;
                if (custOrder.Customer != null)
                {
                    custOrder.Customer = c;
                }
                _allCustomers.Add(c);
                Console.WriteLine("hello");

               // dbContext.Add(Mapper.MapCustomer(c));
                dbContext.SaveChanges();
                return true;
            }
            /// <summary>
            /// Get the Current customer name
            /// </summary>
            /// <returns></returns>
            public static Model.Customer GetCustomer()
            {
                try
                {

                    return CurrentCustomer;
                }
                catch (ArgumentNullException ex)
                {
                    throw (ex);
                }





            }
            /// <summary>
            /// return the all of the stores
            /// </summary>
            /// <returns></returns>
            public static List<Model.Stores> GetAllStore()
            {

                return _allStores;

            }
            /// <summary>
            /// This class will add order to the customer. 
            /// But first it will check if the customer already has an oder if so then we add it to
            /// the order.
            /// If the customer dont have an order yet, then we create the order for the customer even if the customer 
            /// has not register. 
            /// 
            /// </summary>
            /// <param name="x">the input string</param>
            /// <param name="applicationMessage"> display message to the user if the order did not go thru</param>
            public static bool AddToOrder(string x, YourStore.Library.Stores st, out string applicationMessage, out Model.Orders curO)
            {
                Model.Products product;
                int q = 0;
                if (RepoBusLogic.ValidateString(x, st, out applicationMessage, out product, out q))
                {

                    st.ItemInventory[product] = st.ItemInventory[product] - q;
                    if (CurrentCustomer == null)
                        custOrder.Customer = CurrentCustomer;
                    else
                        custOrder.Customer = CurrentCustomer;
                    if (custOrder.Product.ContainsKey(product))
                    {
                        custOrder.Product[product] = custOrder.Product[product] + q;
                    }
                    else
                        custOrder.Product.Add(product, q);
                    custOrder.Store = st;
                    curO = custOrder;
                    return true;
                }
                else
                {
                    curO = custOrder;
                    return false;
                }


            }
            /// <summary>
            /// everytime an order is created, they have to called this method in console
            /// </summary>
            public static void ResetOrder()
            {

                custOrder = new YourStore.Library.Orders();
                custOrder.Id = _allOrders.Count;
            }
            /// <summary>
            /// Finishing order by updating inventories of the product that is purchased
            /// and order details of the what is ordered to the database
            /// Also updating the orders in memory. 
            /// </summary>
            public static void FinishOrdering()
            {
                custOrder.Id += 1;

                custOrder.Timer = DateTime.Now;
                Model.Stores s = custOrder.Store;
                s.UserOrderHistory = new List<Model.Orders>();

                s.UserOrderHistory.Add(custOrder);
                DB.Entities.Orders o = Mapper.MapOrder(custOrder);
                dbContext.Add(o);
                dbContext.SaveChanges();
                //making changes to dbContext inventories

                foreach (YourStore.Library.Products products in custOrder.Product.Keys)
                {
                    var oldinv = dbContext.Inventories.Where(i => i.ProductId == products.ID && i.StoreId == s.StoreID).FirstOrDefault();
                    var newinv = oldinv;
                    newinv.Quantity = s.ItemInventory[products];
                    dbContext.Entry(oldinv).CurrentValues.SetValues(newinv);
                }
                //adding a new order details 
                foreach (YourStore.Library.Products products in custOrder.Product.Keys)
                {
                    //need to get order id
                    dbContext.Add(Mapper.MapOrderDetails(products, custOrder, s.ItemInventory[products]));
                }
                //adding to all orders list
                _allOrders.Add(custOrder);
                Console.WriteLine(custOrder.Product.Keys);
                dbContext.SaveChanges();
            }
            /// <summary>
            /// This prints all the select store order history
            /// </summary>
            /// <param name="st"></param>
            /// <returns></returns>
            public static string StoreOrderHistory(string id)
            {

                int temp = int.Parse(id);

                string c = null; string s = null; ;
                // var st =  GetAllStore().Where(x =>x.StoreID==temp).First();
                var orders = _allOrders.Where(a => a.Store.StoreID == temp);
                foreach (Model.Orders o in orders)
                {
                    c = $"{o.Customer.FirstName,-14 } purchased:\n";
                    foreach (Model.Products p in o.Product.Keys)
                        s = $"{ p.Name,-20:G}\t {o.Product[p],-3:G}\t {p.Cost,-5:C} ( total: {p.Cost * o.Product[p],-5:C})";
                }

                string y = c + s;
                return y;

            }

            /// <summary>
            /// Display the order details after finish order is called
            /// </summary>
            /// <returns></returns>

            public static String GetOrderDeatails()
            {
                if (custOrder != null)
                {
                    string y = null;
                    foreach (Model.Products p in custOrder.Product.Keys)
                    {
                        y += $"\n{ p.Name,10:G}\t Quantity{custOrder.Product[p]}";
                    }

                    string s = $"The order for {custOrder.Customer.FirstName} in {custOrder.Store.Name} at {custOrder.Timer}. Your have { custOrder.Product.Count} item(s) purchased. Your item(s): {y}";
                    return s;
                }
                else
                    return null;
            }

            /// <summary>
            /// This is called in the construtor of DataAccess to get _allOrders  _allStores and _allcustomers
            /// </summary>

            public static void UpdateObjects()
            {
                _allOrders.Clear();
                _allStores.Clear();
                _allCustomers.Clear();

                foreach (DB.Entities.Stores st in dbContext.Stores.Include(a => a.Inventories).ThenInclude(a => a.Product))
                {

                    _allStores.Add(Mapper.MapStore(st));

                }
                foreach (DB.Entities.Orders order in dbContext.Orders.Include(b => b.OrderDetails).ThenInclude(z => z.Product).Include(c => c.Customer).Include(f => f.Store))
                {

                    _allOrders.Add(Mapper.MapOrder(order));

                }

                foreach (YourStore.Library.Stores s in _allStores)
                {
                    foreach (YourStore.Library.Orders o in _allOrders)
                    {
                        if (o.StoreID == s.StoreID)
                        {
                            s.UserOrderHistory.Add(o);
                        }

                    }
                }




                foreach (DB.Entities.Customers customers in dbContext.Customers)
                {
                    _allCustomers.Add(Mapper.MapCustomer(customers));
                }
            }
            public static List<YourStore.Library.Customer> GetAllCusomter()
            {
                return _allCustomers;
            }
            */
        #endregion




        #region Proj1Code

        private readonly Proj1Context _context;

        public RepoBusLogic(Proj1Context context)
        {
            _context = context;
        }

        #endregion
        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }


        #endregion



        #region Proj1 Code
        public IEnumerable<Customer> GetAllCustomer()
        {

            IEnumerable<Customer> allC = new List<Customer>();
            foreach (DB.Entities.Customers customers in _context.Customers)
            {
               ( (List<Customer>) allC).Add(Mapper.MapCustomer(customers));
            }
            return allC;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            IEnumerable<Order> allO = new List<Order>();
            foreach (DB.Entities.Orders ord in _context.Orders.Include(a => a.Customer).Include(a => a.Store).Include(a =>a.OrderDetails).ThenInclude(a => a.Product))
            {
                ((List<Order>)allO).Add(Mapper.MapOrder(ord));
            }
            return allO;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            IEnumerable<Employee> allE = new List<Employee>();
            foreach (DB.Entities.Employees emp in _context.Employees.Include(a => a.Role))
            {
                ((List<Employee>)allE).Add(Mapper.MapEmployee(emp));
            }
            return allE;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            IEnumerable<Product> allP = new List<Product>();
            foreach (DB.Entities.Products p in _context.Products)
            {
                ((List<Product>)allP).Add(Mapper.MapProduct(p));
            }
            return allP;
        }

        public IEnumerable<Store> GetAllStores()
        {
            IEnumerable<Store> stores = new List<Store>();
            foreach (DB.Entities.Stores st in _context.Stores.Include(a=> a.Inventories).ThenInclude(a=> a.Product))
            {
                ((List<Store>)stores).Add(Mapper.MapStore(st));
            }
            return stores;

        }

        public void AddEmployee(Employee emp)
        {
            _context.Add(Mapper.MapEmployee(emp));
           _context.SaveChanges();

        }

        public void AddCustomer(Customer customer)
        {
            _context.Add(Mapper.MapCustomer(customer));
            _context.SaveChanges();

        }

        public void AddOrder(Order order)
        {
            _context.Add(Mapper.MapOrder(order));
            _context.SaveChanges();
        }

        public void AddOrderDetail(int quantity, int product, int orderID)
        {
            _context.Add(Mapper.MapOrderDetails(product, orderID, quantity));
            _context.SaveChanges();
        }

        public void AddInventory(int quantity, Product product, Store store)
        {
            _context.Add(Mapper.MapInven(quantity, product, store));
            _context.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            _context.Add(Mapper.MapProduct(product));
            _context.SaveChanges();
        }

        public void MakeInventoryChanges(int storeid, int productid, int quantity)
        {
           



            var oldinv = _context.Inventories.Where(i => i.ProductId == productid && i.StoreId == storeid).FirstOrDefault();
            oldinv.Quantity = quantity;


            _context.Entry(oldinv).CurrentValues.SetValues(oldinv);
            _context.SaveChanges();
        }


        #endregion
    }
}
