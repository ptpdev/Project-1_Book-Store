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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for LoginMenu.xaml
    /// </summary>
    public partial class LoginMenu : Page
    {
        private string username;
        private string password;
        private int loginCounter = 0;
        public LoginMenu()
        {
            InitializeComponent();
            usernameBox.Focus();
        }

        public string getUser()
        {
            return username;
        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            username = usernameBox.Text;
            password = passwordBox.Password;
            if (loginCounter >= 3)
            {
                MessageBox.Show("System locked.");
            }
            else
            {
                if (validateUser(username, password))
                {
                    MainMenu mainMenu = new MainMenu(username);
                    //mainMenu.username.Content = "User : " + username;
                    this.NavigationService.Navigate(mainMenu);
                    loginCounter = 0;
                }
            }
            loginCounter++;
        }
        private bool validateUser(string user, string pass)
        {
            if (user == "admin" && pass == "*")
            {
                return true;
            }
            return false;
        }
        private void helpBtn_KeyUp(object sender, KeyEventArgs e)
        {
            loginCounter = 0;
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginBtn_Click(this, null);
            }
        }
    }
}
