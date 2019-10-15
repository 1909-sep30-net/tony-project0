using DB;
using DB.Entities;
using DB.Repo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YourStore.Library.Model;
using Stores = DB.Entities.Stores;

namespace YourStore.Library.Repo
{
   public class DataAccess
    {
        static private List<Model.Customers> _allCustomers = new List<Model.Customers>();
        static private Model.Orders custOrder;
        static private List<Model.Stores> _allStores = new List<Model.Stores>();
        static private List<Model.Orders> _allOrders = new List<Model.Orders>();


        //static private Dictionary<Boolean, Customer> CurrentCustomer = new Dictionary<bool, Customer>();
        static public Model.Customers CurrentCustomer;
        static private Proj0Context dbContext;




        /// <summary>
        /// setting up store and products
        /// </summary>
        static DataAccess()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Proj0Context>();
            optionsBuilder.UseSqlServer(SecretConfiguration.connectionString);

            dbContext = new Proj0Context(optionsBuilder.Options);

            //Mapper.InventoryHelper(dbContext.Inventories.Include("p*"));
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
            var x = dict.Keys.Where(k => k.ID == int.Parse(y));

            if (x == null)
            {
                p = null;
                return false;

            }
            p = x.First();
            return true;
        }

        public static void ClearCurrentCustomer()
        {
            CurrentCustomer = null;
        }

        public static bool  ChangeLogin(string f, string l, int id)
        {
            CurrentCustomer = null;
            CurrentCustomer = _allCustomers.Where(x => x.FirstName == f && x.LastName == l && x.Id==id).FirstOrDefault();
            if (CurrentCustomer==null)
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
        static public bool ValidateString(string j, YourStore.Library.Model.Stores st ,out string applicationMessage, out Model.Products product, out int q)
        {
            Console.WriteLine("dest");
            string[] ssize = j.Split(null);
            if ((ssize.Length<3))
            {
                // Product product;
                if (!ValidateProduct(ssize[0],st.ItemInventory,out product))
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

        public static List<Model.Orders> DisplayAllOrders()
        {
            return _allOrders;
        }

        public static YourStore.Library.Model.Customers SearchCustomerById(int code)
        {
        
            return _allCustomers.Where(a => a.Id == code).FirstOrDefault();
        }



        /// <summary>
        /// viewing all customers 
        /// </summary>
        /// <returns></returns>
        public static List<YourStore.Library.Model.Customers> ViewAllCustomer()
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
            Model.Customers c = new Model.Customers();
            c.FirstName = f;
            c.LastName = l;
            if (c.Id == 0)
            {
               c.Id= _allCustomers.Count+1;
            }
            
            int result;

            if (!int.TryParse(zip, out result))
            {
                return false;
            }
            c.Zip = result;
            _allCustomers.Add(c);

            CurrentCustomer = c;
            if (custOrder.Customer != null)
            {
                custOrder.Customer = c;
            }

            dbContext.Add(Mapper.MapCustomer(c));
            dbContext.SaveChanges();
            return true;
        }
        /// <summary>
        /// Get the Current customer name
        /// </summary>
        /// <returns></returns>
        public static Model.Customers GetCustomer()
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
        public static bool AddToOrder(string x, YourStore.Library.Model.Stores st, out string applicationMessage, out Model.Orders curO)
        {
            Model.Products product;
            int q = 0;
            Model.Orders o;
            if (DataAccess.ValidateString(x, st, out applicationMessage, out product, out q))
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
        public static void ResetOrder()
        {

            custOrder = new YourStore.Library.Model.Orders();
            custOrder.Id = _allOrders.Count;
        }
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

            foreach (YourStore.Library.Model.Products products in custOrder.Product.Keys)
            {
                var oldinv = dbContext.Inventories.Where(i => i.ProductId == products.ID && i.StoreId == s.StoreID).FirstOrDefault() ;
                var newinv = oldinv;
                newinv.Quantity = s.ItemInventory[products];
                   dbContext.Entry(oldinv).CurrentValues.SetValues(newinv);
            }
            //adding a new order 
            foreach (YourStore.Library.Model.Products products in custOrder.Product.Keys)
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
       public static string  StoreOrderHistory(string id)
        {
            
            int temp = int.Parse(id);

            string c = null;string s = null; ;
            var st =  GetAllStore().Where(x => x.StoreID == temp).First();
            foreach(Model.Orders o in st.UserOrderHistory)
            {
               c = $"{o.Customer, -14 } purchased:\n";
                foreach (Model.Products p in o.Product.Keys)
                    s = $"{ p.Name, -20:G}\t {o.Product[p]: -3:G}\t {p.Cost, -5:C} ( total: {p.Cost* o.Product[p], -5:C})";
            }

            string y = c + s;
            return y;
            
        }



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
            }else
            return null;
        }

        public static string getAllCustomerOrderDetails()
        {
            string s = null;

           foreach(Model.Stores st in _allStores)
            {
                
                foreach (Model.Orders o in st.UserOrderHistory.Where(x=> x.Customer == CurrentCustomer))
                {
                    Console.WriteLine($"{o.Store.Name}\t\tat: {o.Timer} \t Purchased:");
                    foreach (Model.Products p in o.Product.Keys)
                    {
                        Console.WriteLine($"{p.Name}\t\t{p.Cost}\t\t{o.Product[p]}");

                    }
                }
            }

            return s;
        }
        
        public static void UpdateObjects()
        {
            _allOrders.Clear();
            _allStores.Clear();
            

            foreach (DB.Entities.Stores st in dbContext.Stores.Include(a => a.Inventories).ThenInclude(a => a.Product))
            {

                _allStores.Add(Mapper.MapStore(st));

            }
            foreach (DB.Entities.Orders order in dbContext.Orders.Include(b => b.OrderDetail).ThenInclude(z => z.Product).Include(c => c.Customer).Include(f => f.Store))
            {

                _allOrders.Add(Mapper.MapOrder(order));

            }

            foreach (YourStore.Library.Model.Stores s in _allStores)
            {
                foreach (YourStore.Library.Model.Orders o in _allOrders)
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

    }
}
