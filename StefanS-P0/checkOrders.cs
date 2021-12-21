using System.Data.SqlClient;

namespace DigitalStore
{
    public class checkOrders
    {
        public void history(string user)
        {
            string connect = File.ReadAllText("C:/Users/schwe/Revature/StefanS-P0/connect.txt");           
            SqlConnection connection = new(@connect);

            string userData = $"SELECT * FROM ExistingCustomers WHERE Username = '{user}'";

            connection.Open();
            SqlCommand step1 = new(userData, connection);
            SqlDataReader reader = step1.ExecuteReader();

            reader.Read();
            int CusID = reader.GetInt32(1);
            reader.Close();

            string history = $"SELECT * FROM OrderedItems WHERE CustomerID = {CusID} ORDER BY _TimeDate DESC";
            SqlCommand step2 = new(history, connection);
            reader = step2.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine($"\tStore: {reader.GetString(4)} | Item: {reader.GetString(5)} | Price: {reader.GetDecimal(7)} | Quantity: {reader.GetInt32(6)} | Date: {reader.GetDateTime(8)}");
            }
            reader.Close();
            connection.Close();

        }
    }
}