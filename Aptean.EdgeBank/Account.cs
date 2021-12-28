using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aptean.EdgeBank.Database;

namespace Aptean.EdgeBank
{
    public class Account
    {
        protected string customerId;
        protected string accountId;
        protected float balance;
        protected string accountType;
        protected float interest;
        protected float maximumCapacity;
        protected DateTime dateopened;

        public float CreateAccount()
        {
            if (balance > maximumCapacity)
                throw new ArgumentException("Balance is greater than maximum capacity");
            AccountDB account = new AccountDB();
            account.CreateAccount(customerId, accountId, balance, accountType, interest, maximumCapacity, dateopened);
            return float.Parse(accountId);
        }

        public void DeleteAccount(string accountId)
        {
            AccountDB account = new AccountDB();
            account.DeleteAccount(accountId);
        }

        public float Deposit(string accountId, string balance)
        {
            AccountDB account = new AccountDB();
            if (account.GetBalance(accountId) + float.Parse(balance) > account.BalanceLimit(accountId))
            {
                throw new ArgumentException("Account limit exceded please put valid amount");
            }
            return account.Deposit(accountId, balance);
        }

        public float Withdraw(string accountId, string balance)
        {
            AccountDB account = new AccountDB();
            account.BalanceLimit(accountId);
            if (float.Parse(balance) > account.GetBalance(accountId))
            {
                throw new ArgumentException("Not enough balance");
            }
            return account.Withdraw(accountId, balance);
        }

        public float GetBalance(string accountId)
        {
            AccountDB account = new AccountDB();
            account.BalanceLimit(accountId);
            return account.GetBalance(accountId);
        }

    }
}
