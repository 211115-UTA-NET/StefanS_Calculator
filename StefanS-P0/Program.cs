using System;
using System.IO;
using System.Data.SqlClient;

namespace DigitalStore
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            //Initial system start, displays user options and grabs response as int
            Console.WriteLine("\n---------------------------------");
            Console.WriteLine("Welcome To 3d Refills Online Store");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("New or Returning User?\n\t(1) = New User\n\t(2) = Returning User\n");

            int input = Int32.Parse(Console.ReadLine());
            Login userEntry = new Login();
            
            //checks for user input. (integers only)
            if (input == 1)
            {
                userEntry.NewUser();
            }
            else if (input == 2)
            {
                userEntry.ExistingUser();
            }
        }
    }
}
