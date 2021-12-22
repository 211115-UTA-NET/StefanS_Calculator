using Xunit;
using System.Data.SqlClient;

namespace UnitTests
{
    public class Tests
    {
        [Fact]
        public void Test_Connection()
        {
            bool check;

            string connect = File.ReadAllText("C:/Users/schwe/Revature/StefanS-P0/connect.txt");           
            SqlConnection connection = new(@connect);

            string checkExist = $"SELECT COUNT(*) FROM ExistingCustomers";
            SqlCommand step0 = new(checkExist,connection);

            connection.Open();
            int isData = (int) step0.ExecuteScalar();
            connection.Close();

            if(isData > 0)
            {
                check = true;
            }
            else
            {
                check = false;
            }

            Assert.True(check);


        }
        [Theory]
        [InlineData("hank","hank")]
        [InlineData("fred","fred")]
        public void Equal_Passwords(string pass, string repeat)
        {
            Assert.Equal(pass,repeat);
        }
    }
}