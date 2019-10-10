using System;
using YourStore.Library.Repo;
using YourStore.Library.Model;
using System.Collections.Generic;

namespace YourStore
{
    class UserExperienceConsole
    {

        static int result;

   

        static void Main(string[] args)
        {

             while (true)
             {

                     DefaultMessage();


             }





        }
        
        /// <summary>
        /// This display what you will see if you dont register in the console;
        /// </summary>
        static public void DefaultMessage()
        {
        Start:
            if (DataAccess.CurrentCustomer != null)
            {
                Console.WriteLine($"{DataAccess.CurrentCustomer.First} Welcome to YourMarketPlace");

                //Console.WriteLine("Press button 6 to view your history");

            }
            Console.WriteLine("Welcome to YourMarketPlace");
            Console.WriteLine("Press button 1 to add customer.");
            Console.WriteLine("Press button 3 to list all the item for sales.");
            Console.WriteLine("Press button 2 clear current customer");
            Console.WriteLine("Press button 4 login differnt customer");
            Console.WriteLine("Press button 5 to go Stores Management.");


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
                    if (!DataAccess.CreateCustomer(fName, lName, zip))
                    {
                        goto ZipRestart;
                    }
                }else
                if (result == 3)
                {
                    ListAllProductsByStore();
                }else
                if (result == 2)
                {
                    DataAccess.ClearCurrentCustomer();   
                }
                else
                if (result == 4)
                {
                    StartOfChangeLogin:
                    Console.WriteLine("Please enter your First name:");
                    var fName = Console.ReadLine();
                    Console.WriteLine("Please enter your Last name:");
                    var lName = Console.ReadLine();
                    if( !(DataAccess.ChangeLogin(fName, lName))){
                        goto StartOfChangeLogin;
                    }
                }else
                if (result == 6)
                {

                }
                else
                if (result == 5)
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to Management");
                  
                    Console.WriteLine("Press button 1 to view list of customers");
                    Console.WriteLine("Press button 2 to view store History");
                    Console.WriteLine("Press button 3 to go Customer Page.");

                    var input2 = Console.ReadLine();
                    if (int.TryParse(input2, out result))
                    {
                        if (result == 1)
                        {
                            Console.Clear();
                            string x=null;
                            do
                            {
                                Console.WriteLine($"first\t\tzip,\t\tprefer location\t\tFavorite Items");

                                DataAccess.ViewAllCustomer();
                                Console.WriteLine("Please type quit to exit.");

                            } while (x == "quit");
                        }
                        if (result == 2)
                        {
                            Console.Clear();
                            string x = null;
                            do
                            {
                                Console.WriteLine("Which store: ");
                                foreach(Store st in DataAccess.GetAllStore())
                                {
                                    Console.Write("Type the store id for the store: "+ st.StoreID+ " or type all for all ");
                                    x = Console.ReadLine();
                                }
                                    DataAccess.StoreOrderHistory(x);
                                Console.WriteLine("Please type quit to exit.");

                            } while (x == "quit");
                        }
                    }


                }





            }
            else
            {
                Console.Clear();
                goto Start;
            }





        }

        public static void ListAllProductsByStore()
        {

            List<Store> st = DataAccess.GetAllStore();
            Customer c = new Customer();
            c = DataAccess.CurrentCustomer;
            ListofItems:
            Console.WriteLine($" To add to cart for order type: (StoreID+ProductID Quantity#) Ex: D1 100.  type done to complete order" );
            Console.WriteLine($" To Leave this screen type quit");

            foreach (Store s in st)
            {
                Console.WriteLine();
                Console.WriteLine($" From:{ s.Name}------------------ Store ID:{ s.StoreID}");
                foreach (Product p in s.ItemInventory.Keys)
                {
                    Console.WriteLine($"Product ID:{p.ID,-0:G} Name:{p.Name,-30:D}  Price: {p.Cost,-15:c}  Quantity#: {s.ItemInventory[p],-5:G}");
                }

            }
            

            var code = Console.ReadLine();
            string message;
            Order o;
            if(code =="quit")
            {
                goto done;
            } else if(!DataAccess.AddToOrder(code, out message, out o))
            {
                Console.WriteLine(message);
            }
            else
            {
                goto ListofItems;
            }

        done:
            DataAccess.FinishOrdering();
            Console.Clear();
           if(DataAccess.GetCustomer() == null)
            {

                Console.WriteLine("Please Tell me your first name");
                var fname = Console.ReadLine();
                Console.WriteLine("Please Tell me your last name");
                var lname = Console.ReadLine();
                Console.WriteLine("Please Tell me your zip");
                var zip = Console.ReadLine();
                DataAccess.CreateCustomer(fname, lname, zip);

            }
            Console.Clear();
            string x = null;
            do
            {
                Console.WriteLine(DataAccess.GetOrderDeatails());
                Console.WriteLine("type quit to exit.");
                x= Console.ReadLine();
            } while (x != "quit");
            Console.Clear();
        }



    }
}
