using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace Aptean.EdgeBank.Database
{
    public class EmployeeVerification
    {
        string userName;
        string passWord;
        public EmployeeVerification(string username,string password)
        {
            userName = username;
            passWord = password;
        }
        public bool connectAndCheck()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "exec GetEmployees '" + userName + "' , '" + passWord + "'";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    int count = int.Parse(command.ExecuteScalar().ToString());
                    if (count > 0)
                        return true;
                    else
                        throw new ArgumentException("Invalid Credentials");
                }
            }
            return false;
        }

        public void MakeDatabase()
        {

        }
    }
}
