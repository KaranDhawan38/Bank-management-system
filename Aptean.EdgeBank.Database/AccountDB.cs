using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Aptean.EdgeBank.Database
{
    public class AccountDB
    {

        public void CreateAccount(string customerId, string accountId, float balance, string accountType, float interest, float maximumCapacity, DateTime dateopened)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "exec CheckUser '" + customerId + "'";
                    SqlCommand command1 = new SqlCommand(query, sqlConnection);
                    int count = int.Parse(command1.ExecuteScalar().ToString());
                    if (count > 0)
                    {
                        query = "exec CreateAccount '" + customerId + "' , '" + accountId + "' , '" + balance + "' , '" + accountType + "' , '" + interest + "' , '" + maximumCapacity + "' , '" + dateopened + "'";
                        SqlCommand command2 = new SqlCommand(query, sqlConnection);
                        command2.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                    else
                    {
                        throw new ArgumentException("No user found with customer id : " + customerId);
                    }
                }
            }
        }

        public void DeleteAccount(string accountId)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "exec CheckAccount '" + accountId + "'";
                    SqlCommand command1 = new SqlCommand(query, sqlConnection);
                    int count = int.Parse(command1.ExecuteScalar().ToString());
                    if (count > 0)
                    {
                        query = "exec DeleteAccount '" + accountId + "'";
                        SqlCommand command2 = new SqlCommand(query, sqlConnection);
                        command2.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                    else
                    {
                        throw new ArgumentException("No account found with account id : " + accountId);
                    }
                }
            }
        }

        public float BalanceLimit(string accountId)
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            float balance = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "exec CheckAccount '" + accountId + "'";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    int count = int.Parse(command.ExecuteScalar().ToString());
                    if (count > 0)
                    {
                        query = "exec CheckAccountLimit '" + accountId + "'";
                        SqlCommand command1 = new SqlCommand(query, sqlConnection);
                        balance = float.Parse(command1.ExecuteScalar().ToString());
                        sqlConnection.Close();
                    }
                    else
                    {
                        throw new ArgumentException("No account found with account id : " + accountId);
                    }
                }
            }
            return balance;
        }

        public float GetBalance(string accountId)
        {
            float finalBalance = 0;
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    string query = "exec GetBalance '" + accountId + "'";
                    SqlCommand command = new SqlCommand(query, sqlConnection);
                    finalBalance = float.Parse(command.ExecuteScalar().ToString());
                    sqlConnection.Close();
                }
            }
            return finalBalance;
        }

        public float Deposit(string accountId, string balance)
        {
            float finalBalance = 0;
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    finalBalance = GetBalance(accountId) + float.Parse(balance);
                    string query = "exec UpdateBalance '" + accountId + "' , '" + finalBalance + "'";
                    SqlCommand command1 = new SqlCommand(query, sqlConnection);
                    command1.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            return finalBalance;
        }

        public float Withdraw(string accountId, string balance)
        {
            float finalBalance = 0;
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    finalBalance = GetBalance(accountId) - float.Parse(balance);
                    string query = "exec UpdateBalance '" + accountId + "' , '" + finalBalance + "'";
                    SqlCommand command1 = new SqlCommand(query, sqlConnection);
                    command1.ExecuteNonQuery();
                    sqlConnection.Close();
                }
            }
            return finalBalance;
        }

    }
}
