using System;
using System.Collections.Generic;
using System.Text;
using YourStore.Library.Model;

namespace YourStore.Library.Repo
{
   public class DataAccess
    {
        static private List<Customer> _allCustomers = new List<Customer>();

        public static bool ValdateString(string j9)
        {
            throw new NotImplementedException();
        }

        static private List<Store> _allStores = new List<Store>();

        //static private Dictionary<Boolean, Customer> CurrentCustomer = new Dictionary<bool, Customer>();
        static public Customer CurrentCustomer;
        static private Dictionary<Product, int> GameConsoleList = new Dictionary<Product, int>();
        static private Dictionary<Product, int> GamesList = new Dictionary<Product, int>();
        static private Dictionary<Product, int> GameConsoleList2 = new Dictionary<Product, int>();
        static private Dictionary<Product, int> GamesList2 = new Dictionary<Product, int>();



        /// <summary>
        /// setting up store and products
        /// </summary>
        static DataAccess()
        {

            GameConsoleList.Add(new Product("PlayStation 1", 5.00f,0), 3);
            GameConsoleList.Add(new Product("PSP 3000", 500,1), 22);
            GameConsoleList.Add(new Product("GameCube", 10,2), 122);
            GameConsoleList.Add(new Product("PS2", 45,3), 100);


            GamesList.Add(new Product("Dummy for Life", 59.99f,1), 2);
            GamesList.Add(new Product("How to be Anti-Hitler", 59.99f,2), 0);
            GamesList.Add(new Product("Sam Love Edition", 59.99f,3), 300);

            GameConsoleList2.Add(new Product("PlayStation 1", 323.00f,5), 3);
            GameConsoleList2.Add(new Product("PSP 3000", 323.00f,6), 22);
            GameConsoleList2.Add(new Product("GameCube", 323.00f,4), 122);
            GameConsoleList2.Add(new Product("PS2", 323.00f,7), 100);

            GamesList2.Add(new Product("LOL", 49.99f,8), 100);
            GamesList2.Add(new Product("Dummy for Life", 49.99f,9), 20);
            GamesList2.Add(new Product("How to be Anti-Hitler", 49.99f,6), 0);
            GamesList2.Add(new Product("Sam Love Edition", 39.99f,4), 50);
            GamesList2.Add(new Product("Sam Love Edition 2", 49.99f,3), 50);
            GamesList2.Add(new Product("Sam Love Edition Final Chapter", 59.99f,2), 50);
            GamesList2.Add(new Product("Gangster", 29.99f,1), 50);

            _allStores.Add(new Store("Dom's WareHouse", 7, GameConsoleList,"D"));
            _allStores.Add(new Store("Luke's GameLife", 2, GamesList,"L"));
            _allStores.Add(new Store("John vs Dom", 1, GameConsoleList2,"J"));
            _allStores.Add(new Store("Cheap Games For Life", 99, GamesList2,"C"));





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
            Customer c = new Customer();
            c.First = f;
            c.Last = l;
            int result;

            if (!int.TryParse(zip, out result))
            {
                return false;
            }
            c.Zip = result;
            _allCustomers.Add(c);

            CurrentCustomer = c;

            return true;
        }
        /// <summary>
        /// Get the Current customer name
        /// </summary>
        /// <returns></returns>
        public static Customer GetCustomer()
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

        public static List<Store> GetAllStore()
        {

            return _allStores;

        }


        
    }
}
