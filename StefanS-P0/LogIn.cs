using System.Data.SqlClient;

namespace DigitalStore
{
    public class Login
    {
        public void NewUser()
        {
            //Get initial account information
            Console.WriteLine("Input Username:");
            string username = Console.ReadLine();
            Console.WriteLine("Input First Name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Input Last Name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("\nInput Password:");
            string password = Console.ReadLine();
            Console.WriteLine("\nRetype Password:");
            string retype = Console.ReadLine();

            //Verify that passwords are the same
            while (password != retype)
            {
                Console.WriteLine("Passwords Do not Match\nInput Password:");
                password = Console.ReadLine();
                Console.WriteLine("\nRetype Password:");
                retype = Console.ReadLine();
            }
            
            //Send data to SQL Server
            string connect = File.ReadAllText("C:/Users/schwe/Revature/StefanS-P0/connect.txt");           
            SqlConnection connection = new(@connect);


            string inputPass = $"INSERT INTO CustomerPasswords (Password) VALUES ('{password}');";
            string inputUser = $"INSERT INTO ExistingCustomers SELECT '{username}',MAX(CustomerID),'{firstName}','{lastName}' FROM CustomerPasswords";

            try
            {
                 connection.Open();

                SqlCommand step1 = new(inputPass,connection);
                SqlCommand step2 = new(inputUser,connection);

                step1.ExecuteNonQuery();
                step2.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
               connection.Close(); 
            }
            Login L = new Login();
            L.ExistingUser();
        }


        //----------------------------------------------------------------
        public void ExistingUser()
        {
            int ID = 0;
            string SqlPass;
            bool exists = false;

            Console.WriteLine("Enter Username: ");
            string username = Console.ReadLine();

            //Initialize connection
            string connect = File.ReadAllText("C:/Users/schwe/Revature/StefanS-P0/connect.txt");           
            SqlConnection connection = new(@connect);

            //Set command strings
            string checkExist = $"SELECT COUNT(*) FROM ExistingCustomers WHERE Username = '{username}'";
            string grabID = $"SELECT CustomerID FROM ExistingCustomers WHERE Username = '{username}'";

            
           //Set constructors for command strings
            SqlCommand step0 = new(checkExist,connection);
            SqlCommand step1 = new(grabID,connection);
            
            //Open database and begin checks
            connection.Open();
            int isData = (int) step0.ExecuteScalar();

            if (isData > 0)
            {
                try
                {
                    exists = true;
                    SqlDataReader reader = step1.ExecuteReader();
                    reader.Read();
                    ID = reader.GetInt32(0);
                    reader.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    connection.Close();
                }
                
            }
            connection.Close();


            //Checks for user and if to move on or end method
            if (exists != true)
            {
                Console.WriteLine($"User {username} does not exist");
            }
            else
            {

                //begin password check
                Console.WriteLine("Enter Password");
                string password = Console.ReadLine();

                //Selects password using Customer ID
                string getPass = $"SELECT Password FROM CustomerPasswords WHERE CustomerID = {ID}";

                SqlCommand step2 = new(getPass,connection); 
                connection.Open();

                try
                {
                    SqlDataReader readPass =  step2.ExecuteReader();
                    readPass.Read();
                    SqlPass = readPass.GetString(0);
                    readPass.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error Generated. Details: " + e.ToString());
                    SqlPass = null;
                }
                finally
                {
                    connection.Close();
                }

                if (password == SqlPass)
                {
                    Console.WriteLine("Log-In Sucessful");
                    StorePage S = new StorePage();
                    S.Access(username);

                }
                else
                {
                    Console.WriteLine("Log-In Failed");
                }
            }


        }      
    }
}