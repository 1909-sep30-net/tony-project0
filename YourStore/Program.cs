using System;
using YourStore.Library.Repo;
using YourStore.Library.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NLog;


namespace YourStore
{
    class UserExperienceConsole
    {
     

        static int result;

        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();


        static void Main(string[] args)
        {

             while (true)
             {
                try
                {
                    DefaultMessage();

                }catch(NullReferenceException ex)
                {
                    Console.WriteLine( ex);

                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("There is an a index out of range exception: " + ex);

                }
                catch (OverflowException ex)
                {
                    Console.WriteLine("There is an a exception: " + ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There is an a exception: " + ex);
                }
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
                Console.WriteLine($"{DataAccess.CurrentCustomer.FirstName} Welcome to YourMarketPlace");

                Console.WriteLine("Press button 6 to view all your order history");

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
                    Console.WriteLine("Please enter the Customer id:");
                    var idString = Console.ReadLine();
                    int x;
                    if(int.TryParse(idString, out x))
                    {
                        if (!(DataAccess.ChangeLogin(fName, lName, x)))
                        {
                            goto StartOfChangeLogin;
                        }
                    }
                    else
                    {
                        goto StartOfChangeLogin;
                    }
                  
                }else
                if (result == 6)
                {
                   getAllCustomerOrderDetails();
                }
                else
                if (result == 5)
                {
                    StoreManagmentFun();


                }





            }
            else
            {
                Console.Clear();
                goto Start;
            }





        }
        public static void StoreManagmentFun()
        {
        StoreManagement:
            Console.Clear();
            Console.WriteLine("Welcome to Management");

            Console.WriteLine("Press button 1 to view all customers");
            Console.WriteLine("Press button 2 to view store History");
            Console.WriteLine("Press button 4 to search a customer by id");
            Console.WriteLine("Press button 5 to list all orders");
            Console.WriteLine("Press button 3 to go Customer Page.");

            var input2 = Console.ReadLine();
            if (int.TryParse(input2, out result))
            {
                if (result == 1)
                {
                    Console.Clear();
                    string x = null;
                    do
                    {
                       

                        Console.WriteLine($"FirstName\tLastName\tZip\tPreferLocation.Name\tPreferProduct.Name");

                        List<Customers> cList=DataAccess.ViewAllCustomer();
                        foreach (Customers c in cList)
                        {
                            Console.WriteLine($"{c.FirstName,-10:G}\t\t{c.LastName,-10:G}\t\t{c.Id,-5:G}\t\t{c.Zip,-5:G}\t\t{c.PreferLocation.StoreID ,- 20:G}\t\t{c.ProferProduct.Name,-20:G}");
                        }




                        Console.WriteLine("Type quit to exit.");
                        x = Console.ReadLine();

                    } while (x != "quit");
                    Console.Clear();
                    goto StoreManagement;
                }
                else
                if (result == 2)
                {
                    Console.Clear();
                    string x = null;
                    var stores = DataAccess.GetAllStore();
                    do
                    {
                        try
                        {
                            Console.WriteLine("Which store: ");
                            foreach (Stores st in stores)
                            {
                                Console.WriteLine($"{ st.StoreID}\t{st.Name}");
                            }
                            x = Console.ReadLine();
                            Console.WriteLine(DataAccess.StoreOrderHistory(x));

                        }
                        catch (FormatException ex)
                        {
                            s_logger.Info(ex);
                            Console.WriteLine("Please retry to input correct format");
                        }

                        Console.WriteLine("Please type quit to exit.");
                        x = Console.ReadLine();

                    } while (x == "quit");
                    goto StoreManagement;

                }else if (result == 3)
                {
                    goto done;
                }else if( result == 4)
                {
                    SearchCustomer();
                }
                else if (result == 5)
                {
                    DisplayOrder() ;
                }
                else
                {
                    goto StoreManagement;
                }
            done:
                Console.Clear();
              
            }
        }

        private static void DisplayOrder()
        {
        DisplayRestart:
            Console.Clear();
          //  DataAccess.UpdateObjects();
            List<Orders> orders = null;
            orders =DataAccess.DisplayAllOrders();
            Console.WriteLine($"OrderID\tFirstName\tLastName\tCID\tStore.Name\tStoreID\tTimer\tTotalCost");
            foreach (Orders o in orders)
            {
                Console.WriteLine($"{o.Id,-5:G}{o.Customer.FirstName,-15:G}\t{o.Customer.LastName,-15:G}\t{o.Customer.Id,-5:G}\t{o.Store.Name,-15:G}\t{o.Store.StoreID,-7:G}\t{o.Timer,-20:D}\t{o.TotalCost,-5:c}");
            }
                Console.WriteLine("\n\nType 1 to search sort by price\n Type 2 to sort by time\n type 3 to sort by location\nType 4 to display the order details\n Type Quit to exit");
                String OrderChoice = Console.ReadLine();
                if (OrderChoice == "1")
                {
                    SortPriceChoice:
                    Console.WriteLine("Do you want to sort by Highest(1) or lowest(2). Type quit to exit");
                    string orderPrice =Console.ReadLine();
                    if (orderPrice=="2")
                    {
                        orders.Sort((x, y) => x.TotalCost.CompareTo(y.TotalCost));
                        goto DisplayRestart;
                    }else if (orderPrice == "1")
                    {
                        orders.Sort((x, y) => y.TotalCost.CompareTo(x.TotalCost));
                        goto DisplayRestart;

                    }
                    else if (orderPrice == "quit")
                    {
                        goto done;
                    }
                    else
                    {
                        goto SortPriceChoice;

                    }
                }else if( OrderChoice=="2" )
                {
                SortTimeChoice:
                    Console.WriteLine("Do you want to sort by Earliest(1) or Latest(2). Type quit to exit");
                    string orderTime = Console.ReadLine();
                    if (orderTime == "2")
                    {
                        orders.Sort((x, y) => x.Timer.CompareTo(y.Timer));
                        goto DisplayRestart;
                    }
                    else if (orderTime == "1")
                    {
                        orders.Sort((x, y) => y.Timer.CompareTo(x.Timer));
                        goto DisplayRestart;

                    }
                    else if (orderTime == "quit")
                    {
                        goto done;
                    }
                    else
                    {
                        goto SortTimeChoice;
;

                    }
                }
                else if (OrderChoice == "4")
                {
                OrderChoice4:
                    Console.WriteLine("Which order(id) details would you like to see or type quit to exit");                   
                    string orderchoice1 = Console.ReadLine();
                    int re;
                    if (int.TryParse(orderchoice1, out re))
                    {
                       Orders ord= orders.Where(a => a.Id == re).FirstOrDefault();
                        if(ord == null)
                        {
                            Console.WriteLine("There is no such order");
                            goto DisplayRestart;
                        }
                        else
                        {
                            Console.WriteLine("Order " + ord.Id + "contains: ");
                            foreach(Products p in ord.Product.Keys)
                            {
                                Console.WriteLine($"{p.Name,-15:G}\t{ord.Product[p],-5:G}");
                            }
                        }
                    }else if (orderchoice1 == "quit")
                    {
                        goto done;
                    }
                    else
                    {
                        goto OrderChoice4;
                    }

   
                }



                Console.WriteLine("Type quit to exit.");
              string x=  Console.ReadLine();
            
        done:
            Console.ReadLine();
        }
    

        private static void SearchCustomer()
        {
            Customers c=null;
            int x;

            RestartID:
            Console.WriteLine("Please type in the customer by id");
            string code = Console.ReadLine();
            if( int.TryParse(code, out x))
            {
                c=  DataAccess.SearchCustomerById(x);
                if (c == null)
                {
                    Console.WriteLine("There is no customer with the matching id.");
                    Console.ReadLine();
                    goto RestartID;
                }
                else
                {
                    Console.WriteLine("There is a customer with the matching id.");
                    Console.WriteLine($"FirstName\tLastName\tZip\tPreferLocation.Name\tProferProduct.Name");

                    Console.WriteLine($"{c.FirstName,-10:G}\t{c.LastName,-10:G}\t{c.Zip,-5:G}\t{c.PreferLocation.Name,-10:G}\t{c.ProferProduct.Name}");
                    Console.WriteLine("\n\nType anything to exit");
                    Console.ReadLine();
                }


            }
            else { goto RestartID; }


            
        }

        public static void ListAllProductsByStore()
        {
            DataAccess.ResetOrder();
            DataAccess.UpdateObjects();
            List<Stores> st = DataAccess.GetAllStore();
            Customers c = new Customers();
            c = DataAccess.CurrentCustomer;
   
                if (DataAccess.GetCustomer() == null)
                {

                    Console.WriteLine("Please Tell me your first name");
                    var fname = Console.ReadLine();
                    Console.WriteLine("Please Tell me your last name");
                    var lname = Console.ReadLine();
                    Console.WriteLine("Please Tell me your zip");
                    var zip = Console.ReadLine();
                    try
                    {
                        DataAccess.CreateCustomer(fname, lname, zip);
                    }catch( FormatException ex)
                    {
                        Console.WriteLine("You entered information incorrectly: " + ex);
                    }

                }
                Console.Clear();
            Console.WriteLine($" To Leave this screen type quit");

            foreach (Stores s in st)
            {
                Console.WriteLine();
                Console.WriteLine($" From:{ s.Name}------------------ Store ID:{ s.StoreID}");
                foreach (Products p in s.ItemInventory.Keys)
                {
                    Console.WriteLine($"Product ID:{p.ID,-0:G} Name:{p.Name,-30:D}  Price: {p.Cost,-15:c}  Quantity#: {s.ItemInventory[p],-5:G}");
                  
                }
              
            }
            Console.WriteLine("Please choose a store to buy from");
            string storeID = Console.ReadLine();
           // Console.Clear();

            int result = 0;
            while (int.TryParse(storeID, out result))
            {
                Stores s = st.Where(a => a.StoreID == result).FirstOrDefault();
   

                if (s!=null)
                {
                StoreItems:
                    Console.Clear();
                    foreach (Products p in s.ItemInventory.Keys)
                    {
                        Console.WriteLine($"Product ID:{p.ID,-0:G} Name:{p.Name,-30:D}  Price: {p.Cost,-15:c}  Quantity#: {s.ItemInventory[p],-5:G}");
                    }
                    Console.WriteLine("\n Please Type the product id to add your itme to your order");
                    string code = Console.ReadLine();

                    string message;
                    Orders o;
                    if (code == "quit")
                    {
                        goto done;
                    }
                    else if (!DataAccess.AddToOrder(code, s, out message, out o))
                    {
                        Console.WriteLine(message);

                    }
                    else
                    {
                        goto StoreItems;
                    }
                }

            }



        done:
            Console.Clear();
      
            string x = null;
            do
            {
                Console.WriteLine(DataAccess.GetOrderDeatails());
                Console.WriteLine("type quit to exit.");
                x= Console.ReadLine();
            } while (x != "quit");
            DataAccess.FinishOrdering();

            Console.Clear();
        }

        public static string getAllCustomerOrderDetails()
        {
            string s = null;
            var x = DataAccess.GetAllStore();

            foreach (Stores st in x)
            {

                foreach (Orders o in st.UserOrderHistory.Where(x => x.Customer ==DataAccess.CurrentCustomer))
                {
                    Console.WriteLine($"{o.Store.Name}\t\tat: {o.Timer} \t Purchased:");
                    foreach (Products p in o.Product.Keys)
                    {
                        Console.WriteLine($"{p.Name}\t\t{p.Cost}\t\t{o.Product[p]}");

                    }
                }
            }

            return s;
        }


    }
}
