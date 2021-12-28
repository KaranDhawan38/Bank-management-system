using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Aptean.EdgeBank.Database
{
    public class UserValidation
    {
        public int addUser(string firstName, string LastName, DateTime dateOfBirth, string address, string adhar, string gender, string phoneNumber)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            Random random = new Random();
            int id = random.Next(100000,999999);
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "exec CreateUser '" + id + "' , '" + firstName + "' , '" + LastName + "' , '" + dateOfBirth + "' , '" + address + "' , '" + adhar + "' , '" + gender + "' , '" + phoneNumber + "' , '" + DateTime.Now + "'";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            return id;
        }

        public void DeleteUser(string id)
        {
            int count=0;
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "exec DeleteUser '"+id+"'";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    count=command.ExecuteNonQuery();
                    if (count == 0)
                        throw new ArgumentException("User not found");
                    else
                    {
                        query = "exec DeleteAccountUsingCustomerID '"+id+"'";
                        SqlCommand command1 = new SqlCommand(query, sqlConnection);
                        command1.ExecuteNonQuery();
                    }
                    sqlConnection.Close();
                }
            }
        }

    }
}
