using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aptean.EdgeBank.Database;
using System.IO;

namespace Aptean.EdgeBank
{
    public class Employee
    {

        string userName;
        string passWord;

        public Employee(string username,string password)
        {
            userName = username;
            passWord = password;
        }

        public bool Validate()
        {
            if (userName == "")
                throw new ArgumentException("Username cannot be empty");
            if (passWord == "")
                throw new ArgumentException("Password cannot be empty");
            EmployeeVerification employeeVerify = new EmployeeVerification(userName,passWord);
            return employeeVerify.connectAndCheck();
        }

        public void CheckIfTablesExist()
        {
            string script = File.ReadAllText(@"..\Database Files\Required.sql");
        }

    }
}
