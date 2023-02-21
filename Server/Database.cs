using Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Database
    {
        private SqlConnection connection { get; }
        private Database()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "(LocalDB)\\MSSQLLocalDB";
            builder.AttachDBFilename = "C:\\USERS\\BARTO\\SOURCE\\REPOS\\PLANET\\SERVER\\DATABASE.MDF";
            builder.IntegratedSecurity= true;

            connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
        }

        public bool register(LoginObj register)
        {
            SqlCommand Select = new SqlCommand("SELECT[Username] FROM[User] WHERE[Username] = @Username", connection);
            Select.Parameters.AddWithValue("@Username", register.Login);
            SqlDataReader res = Select.ExecuteReader();
            int count = 0;
            while (res.Read())
            {
                count++;
            }
            res.Close();
            if (count > 0)
            {
                return false;
            }

            SqlCommand Command = new SqlCommand("Insert into [User]([Username], [Password], [Email]) values (@Username, Hashbytes('SHA2_256', @Password), @Email)", connection);
            Command.Parameters.AddWithValue("@Username", register.Login);
            Command.Parameters.AddWithValue("@Password", register.Password);
            Command.Parameters.AddWithValue("@Email", register.Email);

            int i = Command.ExecuteNonQuery();
            return i > 0;
        }
        public bool login(LoginObj login)
        {
            SqlCommand Select = new SqlCommand("SELECT[Username] FROM[User] WHERE[Username] = @Username AND [Password]=Hashbytes('SHA2_256', @Password)", connection);
            Select.Parameters.AddWithValue("@Username", login.Login);
            Select.Parameters.AddWithValue("@Password", login.Password);
            SqlDataReader res = Select.ExecuteReader();
            int count = 0;
            while (res.Read())
            {
                count++;
            }
            res.Close();
            return count > 0;
        }

        private static Database _instance;
        public static Database GetDatabase()
        {
            if (_instance == null)
            {
                _instance = new Database();
            }
            return _instance;
        }
    }
}
