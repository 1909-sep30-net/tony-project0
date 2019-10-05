using System;


namespace FruitSeller
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

        static public void DefaultMessage()
        {
        Start:
            Console.WriteLine("Welcome to YourMarketPlace");
            Console.WriteLine("Press button 1 to register your account.");
            Console.WriteLine("Press button 2 to find your closest store near you.");
            Console.WriteLine("Press button 3 to list all the item for sales.");
            Console.WriteLine("Press button 4 to look for secret sales.");


            var input = Console.ReadLine();
            if (int.TryParse(input, out result))
            {
                Console.Clear();
                if (result == 1)
                {
                    Console.WriteLine("Please enter your First name:");
                    var FName = Console.ReadLine();
                    Console.WriteLine("Please enter your Last name:");
                    var LName = Console.ReadLine();
                ZipRestart:
                    Console.WriteLine("What is your 5 digit zip code");
                    var zip = Console.ReadLine();
                    if (zip.Length != 5)
                    {
                        Console.WriteLine("length" + zip.Length);

                        goto ZipRestart;
                    }

                    if (!int.TryParse(zip, out result))
                    {
                        goto ZipRestart;
                    }

                    //  Customer cust = new Customer(FName, LName, result);
                }
            }
            else
            {
                Console.Clear();
                goto Start;
            }





        }

    }
}
