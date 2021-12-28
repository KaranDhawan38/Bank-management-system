using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptean.EdgeBank
{
    public class Bank : User
    {
        public Bank()
        {
            //Empty constructor
        }

        public Bank(string customerId,float balance,string accountType,float interest,float maximumCapacity,DateTime dateopened)
        {
            this.customerId = customerId;
            Random random = new Random();
            accountId = random.Next(100000,999999).ToString();
            this.balance = balance;
            this.accountType = accountType;
            this.interest = interest;
            this.maximumCapacity = maximumCapacity;
            this.dateopened = dateopened;
        }

        public Bank(string firstName, string LastName, DateTime dateOfBirth, string address, string adhar, string gender, string phoneNumber)
        {
            this.firstName = firstName;
            this.LastName = LastName;
            this.dateOfBirth = dateOfBirth;
            this.address = address;
            this.adhar = adhar;
            this.gender = gender;
            this.phoneNumber = phoneNumber;
        }
    }
}
