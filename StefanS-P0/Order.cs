using System;
using System.Globalization;
using System.Data.SqlClient;


namespace DigitalStore
{
    public class Order
    {
        public void placeOrder(string user,int order,string location)
        {
            Console.WriteLine("Current Shopping Cart:");

            //Connection for SQL
            string connect = File.ReadAllText("C:/Users/schwe/Revature/StefanS-P0/connect.txt");           
            SqlConnection connection = new(@connect);

            string displayCart = $"SELECT * FROM ShoppingCart WHERE OrderID = {order}";

            connection.Open();
            SqlCommand step1 = new(displayCart, connection);

            SqlDataReader reader = step1.ExecuteReader();

            decimal total = 0;
            while (reader.Read())
            {
                Console.WriteLine($"\t{reader.GetString(3)} | {reader.GetDecimal(4)} | {reader.GetInt32(5)}");
                total += reader.GetDecimal(4);
            }
            Console.WriteLine($"\tTotal: ${total}");
            reader.Close();
            connection.Close(); 

            Console.WriteLine("Ready to checkout?\n\t(1) = Yes\n\t(2) = No");

            int input = Int32.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    checkout(user,order,location);
                    break;
                case 2:
                    break;
                default:
                    break;
            }

        }
        public void checkout(string user, int order, string location)
        {
            string connect = File.ReadAllText("C:/Users/schwe/Revature/StefanS-P0/connect.txt");           
            SqlConnection connection = new(@connect);

            string userData = $"SELECT * FROM ExistingCustomers WHERE Username = '{user}'";
            string cartData = $"SELECT * FROM ShoppingCart WHERE OrderID = {order}";


            connection.Open();
            SqlCommand step1 = new(userData, connection);
            SqlDataReader reader = step1.ExecuteReader();

            reader.Read();
            int CusID = reader.GetInt32(1);
            string fName = reader.GetString(2);
            reader.Close();

            SqlCommand step2 = new(cartData, connection);
            
            reader = step2.ExecuteReader();

            List<string> names = new List<string>();
            List<int> quantity = new List<int>();
            List<decimal> prices = new List<decimal>();

            while (reader.Read())
            {
                //grab cart values
                string iName = reader.GetString(3);
                int n = reader.GetInt32(5);
                decimal money = reader.GetDecimal(4);
                names.Add(iName);
                quantity.Add(n);
                prices.Add(money);
            }
            reader.Close();


            int i = 0;
            foreach (var item in names)
            {
                string uploadData = $"INSERT OrderedItems (CustomerID,FirstName,OrderID,StoreLocation,ItemName,Quantity,Price,_TimeDate) VALUES ({CusID},'{user}',{order},'{location}','{names[i]}',{quantity[i]},{prices[i]},GETDATE())";
                SqlCommand step3 = new(uploadData, connection);
                step3.ExecuteNonQuery();
                i++;
            }
            //Deletes contents from cart
            string clearCart = "DELETE FROM ShoppingCart";
            SqlCommand step4 = new(clearCart,connection);
            step4.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine("Order Placed\n-------------");
        }
    }
}