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
        static private Model.Orders custOrder = new Model.Orders();



        

        /// <summary>
        /// This is to validate store code.
        /// </summary>
        /// <param name="y"> is the store code</param>
        /// <param name="st"> the store that is going to contain the code</param>
        /// <returns></returns>
        public static bool ValidateStore(char y, out Model.Stores store)
        {
            double y1 = char.GetNumericValue(y);
            
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
        public static bool ValidateProduct(char y, Dictionary<Model.Products, int> dict, out Model.Products p)
        {
            var x = dict.Keys.Where(k => k.ID == Char.GetNumericValue(y));

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

        public static bool  ChangeLogin(string f, string l)
        {
            CurrentCustomer = null;
            CurrentCustomer = _allCustomers.Where(x => x.FirstName == f && x.LastName == l).FirstOrDefault();
            if (CurrentCustomer==null)
            {
                return false;
            }
            return true;
        }

        /*  private static bool validateQuan(string y,Product p ,Dictionary<Product, int> dict, out int q)
          {

              if (int.TryParse(y, out q))
              {
                  if (q > dict[p])
                  {
                      return false;
                  }

              }
              else
              {
                  return false;

              }
              return true;
          }*/

        /// <summary>
        /// Valdating the input string before placing order
        /// </summary>
        /// <param name="j"></param>
        /// <param name="applicationMessage"></param>
        /// <returns></returns>
        static public bool ValidateString(string j, out string applicationMessage, out Model.Stores store, out Model.Products product, out int q)
        {

            if (!(j.Length> 6))
            {
             //  Store store;
               if(!ValidateStore(j[0], out store))
               {

                    applicationMessage = "There is no such store code";
                    product = null;
                    q = 0;
                    return false;

                }


                // Product product;
                if (!ValidateProduct(j[1], store.ItemInventory,out product))
                {
                    applicationMessage = "There is no such item in store";
                  //  product = null;
                    q = 0;
                    return false;
                }
               // int q;
                if (int.TryParse(j.Substring(3), out q))
                {
                    if (q > store.ItemInventory[product])
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
            applicationMessage = ("To add an item to your store please type (StoreCode+ProductCode Quantity) ex: j1 199!");
            product = null;
            q = 0;
            store = null;
            return false;
        }

        public static string ViewAllCustomer()
        {
            string s = null;
            foreach (Model.Customers c in _allCustomers) 
            {
                s += $"\n{c.FirstName,20:G}\t\t{c.Zip}\t\t{c.PreferLocation}";
           //    Console.WriteLine( $"{c.Last}\t\t{c.Zip}\t\t{c.PreferLocation}");    
            }
            return s;
        }

    

        static private List<Model.Stores> _allStores = new List<Model.Stores>();

        //static private Dictionary<Boolean, Customer> CurrentCustomer = new Dictionary<bool, Customer>();
        static public Model.Customers CurrentCustomer;
        static private Dictionary<Model.Products, int> GameConsoleList = new Dictionary<Model.Products, int>();
        static private Dictionary<Model.Products, int> GamesList = new Dictionary<Model.Products, int>();
        static private Dictionary<Model.Products, int> GameConsoleList2 = new Dictionary<Model.Products, int>();
        static private Dictionary<Model.Products, int> GamesList2 = new Dictionary<Model.Products, int>();
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



            foreach (DB.Entities.Stores st in dbContext.Stores.Include(a => a.Inventories).ThenInclude(a => a.Product).Include(o=> o.Orders))
            {
                
                _allStores.Add(Mapper.MapStore(st));

            }

          
            foreach (DB.Entities.Customers customers in dbContext.Customers)
            {
                _allCustomers.Add(Mapper.MapCustomer(customers));
            }

      




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
        public static bool AddToOrder(string x, out string applicationMessage, out Model.Orders curO)
        {
            Model.Stores st;
            Model.Products product;
            int q = 0;
            Model.Orders o;

            if (DataAccess.ValidateString(x, out applicationMessage, out st, out product, out q))
            {
                st.ItemInventory[product] = st.ItemInventory[product] - q;
                if (CurrentCustomer == null)
                    custOrder.Customer = null;
                else
                    custOrder.Customer = CurrentCustomer;
                custOrder.Product.Add(product, q);
                custOrder.Store = st;
                //Console.WriteLine(product.Name);
                curO = custOrder;
                return true;
            }
            else
            {
                curO = custOrder;
                return false;
            }
        }
        public static void FinishOrdering()
        {
            custOrder.Timer = DateTime.Now;
            Model.Stores s = custOrder.Store;
            s.UserOrderHistory.Add(custOrder);
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
            string y=null;
            foreach(Model.Products p in custOrder.Product.Keys)
            {
                y += $"\n{ p.Name, 10:G}\t Quantity{custOrder.Product[p]}";
            }

            string s = $"The order for {custOrder.Customer} in {custOrder.Store} at {custOrder.Timer}. Your have { custOrder.Product.Count} item(s) purchased. Your item(s): {y}";
            return s;
        }

        public static string getAllCustomerOrderDetails()
        {
            string s = null;
            string prod = null;

           foreach(Model.Stores st in _allStores)
            {
                ;
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
        
    }
}
