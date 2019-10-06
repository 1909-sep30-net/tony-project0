using System;
using YourStore.Library.Repo;
using YourStore.Library.Model;
using System.Collections.Generic;

namespace YourStore
{
    class UserExperienceConsole
    {

        static int result;
        static Boolean login = false;


        static void Main(string[] args)
        {

            while (true)
            {

                if (!login)
                    DefaultMessage();
                else
                {

                }

            }





        }
        /// <summary>
        /// This display what you will see if you dont register in the console;
        /// </summary>
        static public void DefaultMessage()
        {
        Start:
            Console.WriteLine("Welcome to YourMarketPlace");
            Console.WriteLine("Press button 1 to add customer.");
            Console.WriteLine("Press button 2 to find your closest store near you.");
            Console.WriteLine("Press button 3 to list all the item for sales.");
            Console.WriteLine("Press button 4 to look for secret sales.");
            Console.WriteLine("Press button 5 to refill stock");


            var input = Console.ReadLine();
            if (int.TryParse(input, out result))
            {
                Console.Clear();
                if (result == 1)
                {
                    Console.WriteLine("Please enter your First name:");
                    var fName = Console.ReadLine();
                    Console.WriteLine("Please enter your Last name:");
                    var lName = Console.ReadLine();

                ZipRestart:
                    Console.WriteLine("What is your 5 digit zip code");
                    var zip = Console.ReadLine();
                    if (DataAccess.CreateCustomer(fName, lName, zip))
                    {
                        goto ZipRestart;
                    }
                    login = true;
                }
                if (result == 3)
                {
                    ListAllProductsByStore();
                }




            }
            else
            {
                Console.Clear();
                goto Start;
            }





        }

        public static void LoggedinMessageAsCust()
        {
        LoggedinMessage:
            Console.WriteLine($"{DataAccess.GetCustomer().First + " " + DataAccess.GetCustomer().Last} Welcome to YourMarketPlace!");
            Console.WriteLine("Press button 1 to review your order.");
            Console.WriteLine("Press button 2 to visit your favorite store");
            Console.WriteLine("Press button 3 to list all the item for sales.");
            Console.WriteLine("Press button 4 to look for promotion sales.");
            var num = Console.ReadLine();

            if (num == "4")
            {

            }







        }


        public static void ListAllProductsByStore()
        {
            List<Store> st = DataAccess.GetAllStore();
            Customer c = new Customer();
            c = DataAccess.CurrentCustomer;
            Console.WriteLine($" To add to cart for order type (Product ID + Store ID)");
            foreach (Store s in st)
            {
                Console.WriteLine();
                Console.WriteLine($" From:{ s.Name}------------------ Store ID:{ s.StoreID}");

                int i = 1;
                foreach (Product p in s.ItemInventory.Keys)
                {
                    Console.WriteLine($"Product ID:{p.ID,-0:G} Name:{p.Name,-30:D}  Price: {p.Cost,-15:c}  Qualinty: {s.ItemInventory[p],-5:G}");
                }

            }
            var code = Console.ReadLine();


        }



    }
}
