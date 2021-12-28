using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aptean.EdgeBank.Database;

namespace Aptean.EdgeBank
{
    public class User : Account
    {
        protected string firstName;
        protected string LastName;
        protected DateTime dateOfBirth;
        protected string address;
        protected string adhar;
        protected string gender;
        protected string phoneNumber;

        public int createUser()
        {
            UserValidation user = new UserValidation();
            return user.addUser(firstName, LastName, dateOfBirth, address, adhar, gender, phoneNumber);
        }

        public bool IsNull(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return true;
            else
                return false;
        }

        public void DeleteUser(string id)
        {
            UserValidation user = new UserValidation();
            user.DeleteUser(id);
        }

    }
}
