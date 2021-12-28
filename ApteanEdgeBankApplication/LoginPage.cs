using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Aptean.EdgeBank;

namespace ApteanEdgeBankApplication
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                Employee employee = new Employee(username.Text,password.Text);
                if(employee.Validate())
                {
                    this.Hide();
                    using (MainPage main = new MainPage(username.Text))
                    {
                        main.ShowDialog();
                    }
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            
        }
    }
}
