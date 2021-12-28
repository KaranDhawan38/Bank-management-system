using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aptean.EdgeBank;
using System.Configuration;

namespace ApteanEdgeBankApplication
{
    public partial class MainPage : Form
    {

        string username;
        public MainPage(string name)
        {
            username = name;
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'apteanEdgeBankDataSet1.Account' table. You can move, or remove it, as needed.
            this.accountTableAdapter.Fill(this.apteanEdgeBankDataSet1.Account);
            // TODO: This line of code loads data into the 'apteanEdgeBankDataSet.Users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.apteanEdgeBankDataSet.Users);
            name.Text = name.Text + username;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(44, 44, 47);
            panel1.BackColor = Color.FromArgb(250, 250, 250);
            heading.ForeColor = Color.FromArgb(250, 250, 250);
            name.ForeColor = Color.FromArgb(250, 250, 250);
            apteanBlue.Hide();
            apteanWhite.Visible = true;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(250, 250, 250);
            heading.ForeColor = Color.FromArgb(0, 0, 0);
            name.ForeColor = Color.FromArgb(0, 0, 0);
            apteanWhite.Hide();
            apteanBlue.Visible = true;
        }

        private void CreateUser_MouseHover(object sender, EventArgs e)
        {
            createUser.BackColor = Color.FromArgb(241, 185, 126);
        }

        private void CreateUser_MouseLeave(object sender, EventArgs e)
        {
            createUser.BackColor = Color.FromArgb(255, 235, 205);
        }

        private void CreateUser_Click(object sender, EventArgs e)
        {
            createUserPanel.Visible = true;
            optionsPanel.Hide();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            createUserPanel.Hide();
            optionsPanel.Visible = true;
        }

        private void CreateUserButton_Click(object sender, EventArgs e)
        {

            try
            {
                Bank user = new Bank(firstName.Text, lastName.Text, dateOfBirth.Value, address.Text, adhar.Text, gender.Text, phoneNumber.Text);
                int flag = 0;
                if (user.IsNull(firstName.Text))
                {
                    firstnameError.SetError(firstName, "This field is required");
                    flag = 1;
                }
                else
                    firstnameError.SetError(firstName, null);
                if (user.IsNull(address.Text))
                {
                    addressError.SetError(address, "This field is required");
                    flag = 1;
                }
                else
                    addressError.SetError(address, null);
                if (user.IsNull(adhar.Text))
                {
                    adharError.SetError(adhar, "This field is required");
                    flag = 1;
                }
                else
                    adharError.SetError(adhar, null);
                if (user.IsNull(gender.Text))
                {
                    genderError.SetError(gender, "This field is required");
                    flag = 1;
                }
                else
                    genderError.SetError(gender, null);
                if (user.IsNull(phoneNumber.Text))
                {
                    phoneError.SetError(phoneNumber, "This field is required");
                    flag = 1;
                }
                else
                    phoneError.SetError(phoneNumber, null);
                if (flag == 0)
                {
                    int id = user.createUser();
                    MessageBox.Show("User Created with Id : " + id);
                    createUserPanel.Hide();
                    optionsPanel.Visible = true;
                    firstName.Text = "";
                    lastName.Text = "";
                    dateOfBirth.Text = "";
                    address.Text = "";
                    adhar.Text = "";
                    gender.Text = "";
                    phoneNumber.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            createUserPanel.Hide();
            optionsPanel.Visible = true;
            firstName.Text = "";
            lastName.Text = "";
            dateOfBirth.Text = "";
            address.Text = "";
            adhar.Text = "";
            gender.Text = "";
            phoneNumber.Text = "";
            firstnameError.SetError(firstName, null);
            addressError.SetError(address, null);
            adharError.SetError(adhar, null);
            genderError.SetError(gender, null);
            phoneError.SetError(phoneNumber, null);
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit ?", "Quit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            optionsPanel.Hide();
            DeleteUserPanel.Visible = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            optionsPanel.Visible = true;
            DeleteUserPanel.Hide();
            id.Text = "";
            idError.SetError(id, null);
        }

        private void DeleteUser_MouseHover(object sender, EventArgs e)
        {
            deleteUser.BackColor = Color.FromArgb(241, 185, 126);
        }

        private void DeleteUser_MouseLeave(object sender, EventArgs e)
        {
            deleteUser.BackColor = Color.FromArgb(255, 235, 205);
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            try
            {
                Bank deleteUser = new Bank();
                if (deleteUser.IsNull(id.Text))
                {
                    idError.SetError(id, "This field cannot be empty");
                }
                else
                {
                    idError.SetError(id, null);
                    if (MessageBox.Show("Are you sure you want to delete user with ID : " + id.Text, "Delete user", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        deleteUser.DeleteUser(id.Text);
                        MessageBox.Show("User deleted successfully");
                        optionsPanel.Visible = true;
                        DeleteUserPanel.Hide();
                        id.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateAccount_Click(object sender, EventArgs e)
        {
            optionsPanel.Hide();
            createAccountPanel.Visible = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            optionsPanel.Visible = true;
            createAccountPanel.Hide();
            customerId.Text = "";
            balance.Text = "";
            accountType.Text = "";
            interest.Text = "";
            maximumCapacity.Text = "";
            customerIdError.SetError(customerId, null);
            balanceError.SetError(balance, null);
            accountTypeError.SetError(accountType, null);
            InterestRateError.SetError(interest, null);
            maximumCapacityError.SetError(maximumCapacity, null);
        }

        private void CreateAccount_MouseHover(object sender, EventArgs e)
        {
            createAccount.BackColor = Color.FromArgb(241, 185, 126);
        }

        private void CreateAccount_MouseLeave(object sender, EventArgs e)
        {
            createAccount.BackColor = Color.FromArgb(255, 235, 205);
        }

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                Bank bank = new Bank();
                int flag = 0;
                if (bank.IsNull(customerId.Text))
                {
                    customerIdError.SetError(customerId, "Field cannot be empty");
                    flag = 1;
                }
                else
                    customerIdError.SetError(customerId, null);
                if (bank.IsNull(balance.Text))
                {
                    balanceError.SetError(balance, "Field cannot be empty");
                    flag = 1;
                }
                else
                    balanceError.SetError(balance, null);
                if (bank.IsNull(accountType.Text))
                {
                    accountTypeError.SetError(accountType, "Field cannot be empty");
                    flag = 1;
                }
                else
                    accountTypeError.SetError(accountType, null);
                if (bank.IsNull(interest.Text))
                {
                    InterestRateError.SetError(interest, "Field cannot be empty");
                    flag = 1;
                }
                else
                    InterestRateError.SetError(interest, null);
                if (bank.IsNull(maximumCapacity.Text))
                {
                    maximumCapacityError.SetError(maximumCapacity, "Field cannot be empty");
                    flag = 1;
                }
                else
                    maximumCapacityError.SetError(maximumCapacity, null);
                if (flag == 0)
                {
                    Bank account = new Bank(customerId.Text, float.Parse(balance.Text), accountType.Text, float.Parse(interest.Text), float.Parse(maximumCapacity.Text), DateTime.Now);
                    account.CreateAccount();
                    MessageBox.Show("Account Created");
                    optionsPanel.Visible = true;
                    createAccountPanel.Hide();
                    customerId.Text = "";
                    balance.Text = "";
                    accountType.Text = "";
                    interest.Text = "";
                    maximumCapacity.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveAccount_MouseHover(object sender, EventArgs e)
        {
            removeAccount.BackColor = Color.FromArgb(241, 185, 126);
        }

        private void RemoveAccount_MouseLeave(object sender, EventArgs e)
        {
            removeAccount.BackColor = Color.FromArgb(255, 235, 205);
        }

        private void RemoveAccount_Click(object sender, EventArgs e)
        {
            deleteAccountPanel.Visible = true;
            optionsPanel.Hide();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            deleteAccountPanel.Hide();
            optionsPanel.Visible = true;
            accountId.Text = "";
            accountIdError.SetError(accountId, null);
        }

        private void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                Bank bank = new Bank();
                int flag = 0;
                if (bank.IsNull(accountId.Text))
                {
                    accountIdError.SetError(accountId, "Field cannot be empty");
                    flag = 1;
                }
                else
                    accountIdError.SetError(accountId, null);
                if (flag == 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete account ?", "Delete account", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bank.DeleteAccount(accountId.Text);
                        MessageBox.Show("Account deleted successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Deposit_Click(object sender, EventArgs e)
        {
            depositPanel.Visible = true;
            optionsPanel.Hide();
        }

        private void Deposit_MouseHover(object sender, EventArgs e)
        {
            deposit.BackColor = Color.FromArgb(241, 185, 126);
        }

        private void Deposit_MouseLeave(object sender, EventArgs e)
        {
            deposit.BackColor = Color.FromArgb(255, 235, 205);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            depositPanel.Hide();
            optionsPanel.Visible = true;
            accountIdDeposit.Text = "";
            amount.Text = "";
            amountError.SetError(amount, null);
            accountIdDepositError.SetError(accountIdDeposit, null);
        }

        private void DepositButton_Click(object sender, EventArgs e)
        {
            try
            {
                Bank bank = new Bank();
                int flag = 0;
                if (bank.IsNull(accountIdDeposit.Text))
                {
                    accountIdDepositError.SetError(accountIdDeposit, "Field cannot be empty");
                    flag = 1;
                }
                else
                    accountIdDepositError.SetError(accountIdDeposit, null);
                if (bank.IsNull(amount.Text))
                {
                    amountError.SetError(amount, "Field cannot be empty");
                    flag = 1;
                }
                else
                    amountError.SetError(amount, null);
                if (flag == 0)
                {
                    MessageBox.Show("Amount added Successfully\n" + "Final balance : " + bank.Deposit(accountIdDeposit.Text, amount.Text));
                    depositPanel.Hide();
                    optionsPanel.Visible = true;
                    accountIdDeposit.Text = "";
                    amount.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Withdraw_Click(object sender, EventArgs e)
        {
            withdrawPanel.Visible = true;
            optionsPanel.Hide();
        }

        private void Withdraw_MouseHover(object sender, EventArgs e)
        {
            withdraw.BackColor = Color.FromArgb(241, 185, 126);
        }

        private void Withdraw_MouseLeave(object sender, EventArgs e)
        {
            withdraw.BackColor = Color.FromArgb(255, 235, 205);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            withdrawPanel.Hide();
            optionsPanel.Visible = true;
            accountIdWithdraw.Text = "";
            amountWithdraw.Text = "";
            accountIdWithdrawError.SetError(accountIdWithdraw, null);
            amountWithdrawError.SetError(amountWithdraw, null);
        }

        private void WithdrawButton_Click(object sender, EventArgs e)
        {
            try
            {
                Bank bank = new Bank();
                int flag = 0;
                if (bank.IsNull(accountIdWithdraw.Text))
                {
                    accountIdWithdrawError.SetError(accountIdWithdraw, "Field cannot be empty");
                    flag = 1;
                }
                else
                    accountIdWithdrawError.SetError(accountIdWithdraw, null);
                if (bank.IsNull(amountWithdraw.Text))
                {
                    amountWithdrawError.SetError(amountWithdraw, "Field cannot be empty");
                    flag = 1;
                }
                else
                    amountWithdrawError.SetError(amountWithdraw, null);
                if (flag == 0)
                {
                    MessageBox.Show("Ammount withdrawn Successfully \n Current balance : " + bank.Withdraw(accountIdWithdraw.Text, amountWithdraw.Text));
                    withdrawPanel.Hide();
                    optionsPanel.Visible = true;
                    accountIdWithdraw.Text = "";
                    amountWithdraw.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetBalance_Click(object sender, EventArgs e)
        {
            getBalancePanel.Visible = true;
            optionsPanel.Hide();
        }

        private void GetBalance_MouseHover(object sender, EventArgs e)
        {
            getBalance.BackColor = Color.FromArgb(241, 185, 126);
        }

        private void GetBalance_MouseLeave(object sender, EventArgs e)
        {
            getBalance.BackColor = Color.FromArgb(255, 235, 205);
        }

        private void GetBalanceButton_Click(object sender, EventArgs e)
        {
            try
            {
                Bank bank = new Bank();
                int flag = 0;
                if (bank.IsNull(accountIdGetBalance.Text))
                {
                    accountIdGetBalanceError.SetError(accountIdGetBalance, "Field cannot be empty");
                    flag = 1;
                }
                else
                    accountIdWithdrawError.SetError(accountIdGetBalance, null);
                if (flag == 0)
                {
                    MessageBox.Show("Current balance : " + bank.GetBalance(accountIdGetBalance.Text));
                    accountIdGetBalance.Text = "";
                    accountIdGetBalanceError.SetError(accountIdGetBalance, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            getBalancePanel.Hide();
            optionsPanel.Visible=true;
            accountIdGetBalance.Text = "";
            accountIdGetBalanceError.SetError(accountIdGetBalance, null);
        }

        private void Database_Click(object sender, EventArgs e)
        {
            databasePanel.Visible = true;
            optionsPanel.Hide();
        }

        private void Database_MouseHover(object sender, EventArgs e)
        {
            database.BackColor = Color.FromArgb(241, 185, 126);
        }

        private void Database_MouseLeave(object sender, EventArgs e)
        {
            database.BackColor = Color.FromArgb(255, 235, 205);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            databasePanel.Hide();
            optionsPanel.Visible=true;
        }
    }
}
