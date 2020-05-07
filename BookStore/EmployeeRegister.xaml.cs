using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for EmployeeRegister.xaml
    /// </summary>
    public partial class EmployeeRegister : Window
    {
        public EmployeeRegister()
        {
            InitializeComponent();
        }
        public EmployeeRegister(string admin)
        {
            InitializeComponent();
            txtUsername.IsEnabled = false;
            txtUsername.Text = admin;
            txtEmployeeName.IsEnabled = false;
            txtEmployeeName.Text = admin;
        }

        private void btnEmployeeRegis_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == txtPassword2.Password)
            {
                bool regisStatus = LoginSystem.AddData(txtUsername.Text, txtPassword.Password, txtEmployeeName.Text);
                if (regisStatus)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("This username already Exists.");
                }
            }
            else
            {
                MessageBox.Show("Password is not matched.");
            }
        }
    }
}
