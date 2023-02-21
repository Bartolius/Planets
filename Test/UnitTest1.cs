using Library;
using Microsoft.Win32;
using Server;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string asciiColor = Color.ForegroundColor(new Color(12, 34, 211));

            Assert.Equal("\x1b[38;2;12;34;211m", asciiColor);
        }
        [Fact]
        public void Test2()
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "(LocalDB)\\MSSQLLocalDB";
            builder.AttachDBFilename = "C:\\USERS\\BARTO\\SOURCE\\REPOS\\PLANET\\SERVER\\DATABASE.MDF";
            builder.IntegratedSecurity = true;

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                SqlCommand Select = new SqlCommand("SELECT[Username] FROM[User] WHERE[Username] = @Username", connection);
                Select.Parameters.AddWithValue("@Username", "Bartolius");

                SqlDataReader res = Select.ExecuteReader();



                int i = 0;
                while (res.Read())
                {
                    i++;
                }
                Assert.Equal(i,2);
            }


            /*SqlCommand Select = new SqlCommand("SELECT count(*) FROM [User] WHERE Username=@Username", connection);
            Select.Parameters.AddWithValue("@Username", "Bartolis");*/

            /*SqlCommand Select = new SqlCommand("SELECT count(*) FROM [User] WHERE [Username] = 'Bartolius'", connection);

            int res = Select.ExecuteNonQuery();
            Console.WriteLine(res);

            Assert.Equal(res, 2);*/


        }
    }
}