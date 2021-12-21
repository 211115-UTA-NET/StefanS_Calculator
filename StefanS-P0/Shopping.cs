using System.Data.SqlClient;

namespace DigitalStore
{
    public class Store : StorePage
    {
        public void Shop(string user, int orderID)
        {
            bool check = false;
            string sqlInput = null;
            
            while (check != true)
            {
                Console.WriteLine("Choose Store Location:\n1 = North St.\n2 = Southern Rd.");
                int input = Int32.Parse(Console.ReadLine());

                //converts input to response for SQL server parsing
                switch(input)
                {
                    case 1:
                        sqlInput = "North St.";
                        check = true;
                        break;
                    case 2:
                        sqlInput = "Southern Rd.";
                        check = true;
                        break;
                    case 3:
                        check = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid input, try again\n");
                        break;
                }
            }

            //Connection for SQL
            string connect = File.ReadAllText("C:/Users/schwe/Revature/StefanS-P0/connect.txt");           
            SqlConnection connection = new(@connect);


            string grabItems = $"Select * FROM _Catalog WHERE StoreLocation = '{sqlInput}'";

            connection.Open();
            SqlCommand step1 = new(grabItems, connection);
            SqlDataReader reader = step1.ExecuteReader();

            while(reader.Read())
            {
                int i = reader.GetInt32(0);
                string a = reader.GetString(1);
                decimal b = reader.GetDecimal(3);
                int c = reader.GetInt32(4);
                Console.WriteLine($"\t({i}) = {a} | ${b} | In Stock: {c}");
            }
            reader.Close();
            connection.Close();

            //while loop to add items to cart
            bool shopping = true;
            while(shopping == true)
            {
                //ask for item to change
                Console.WriteLine("Select items you wish to select or type 'Exit' or 'Checkout'");
                string item = Console.ReadLine();
                item = item.ToLower();
                if(item == "exit")
                {
                    shopping = false;
                    break;
                }
                else if(item == "checkout")
                {
                    Order O = new Order();
                    O.placeOrder(user,orderID,sqlInput);
                    break;
                }
                //Ask for quanity of item order
                Console.WriteLine("Select quantity:");
                int quantity = Int32.Parse(Console.ReadLine());

                string tryCart = $"SELECT * FROM _Catalog WHERE ItemID = {item}";

                connection.Open();
                SqlCommand step2 = new(tryCart, connection);
                reader = step2.ExecuteReader();

                reader.Read();
                string aa = reader.GetString(1);
                decimal bb = reader.GetDecimal(3);
                int n = reader.GetInt32(4);
                reader.Close();

                //checks for inventory quantity
                if (n < quantity)
                {
                    Console.WriteLine("Not enough inventory, select another item or go to another store location"); 
                    connection.Close();               
                }
                else
                {
                    //update cart, update catalog
                    int newQuantity = n - quantity;

                    string addCart = $"INSERT INTO ShoppingCart (OrderID,ItemID,ItemName,Price,Quantity) VALUES ({orderID},{item},'{aa}',{bb},{quantity})";
                    //string addID = $"UPDATE ShoppingCart SET orderID = {orderID} WHERE ItemID = {item}";
                    string updateCatalog = $"UPDATE _Catalog SET Quantity = {newQuantity} WHERE ItemID = {item}";

                    SqlCommand step3 = new(addCart, connection);
                    //SqlCommand step4 = new(addID, connection);
                    SqlCommand step5 = new(updateCatalog, connection);

                    step3.ExecuteNonQuery();
                    //step4.ExecuteNonQuery();
                    step5.ExecuteNonQuery();

                    //Display current cart
                    Console.WriteLine("Your Cart:");

                    string displayCart = $"SELECT * FROM ShoppingCart WHERE OrderID = {orderID}";

                    SqlCommand step6 = new(displayCart, connection);
                    reader = step6.ExecuteReader();

                    decimal total = 0;

                    while (reader.Read())
                    {
                        Console.WriteLine($"\t{reader.GetString(3)} | {reader.GetDecimal(4)} | {reader.GetInt32(5)}");
                        total += reader.GetDecimal(4);
                    }
                    Console.WriteLine($"\tTotal: ${total}");
                    reader.Close();
                    connection.Close();                
                }
            }
        }
    }
}