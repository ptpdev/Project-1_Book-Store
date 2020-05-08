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

            if (LoginSystem.checkPassword(username, password))
            {
                MainMenu mainMenu = new MainMenu(username);
                this.NavigationService.Navigate(mainMenu);                    
            }
            else
            {
                MessageBox.Show("Incorrect password");
            }    
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                loginBtn_Click(this, null);
            }
        }

        private void btnCredit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(
                "_______________________CREDIT_______________________\n\n" +
                "                    PITIPHAT  PHETLERTANAN             \n" +
                "                       aongst15@gmail.com\n\n" +
                "Icon made by Freepik from www.flaticon.com\n" +
                "Icon made by iconixar from www.flaticon.com\n" +                
                "Icon made by mynamepong from www.flaticon.com\n\n" +
                "_____________________________________________________");
        }
    }
}
