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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        private string username;
        public static bool bookMenuBtnEnable;
        public MainMenu(string username)
        {
            InitializeComponent();
            usernameLabel.Content = "User : " + username;
            this.username = username;
            if (username == "admin")
            {
                btnAdmin.Visibility = Visibility.Visible;
            }
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginMenu loginMenu = new LoginMenu();
            this.NavigationService.Navigate(loginMenu);
        }

        private void BooksMgtBtn_Click(object sender, RoutedEventArgs e)
        {
            BooksMenu booksMenu = new BooksMenu();
            booksMenu.Show();
            bookMenuBtnEnable = false;
            BooksMgtBtn.IsEnabled = bookMenuBtnEnable;
        }

        private void customersMgtBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomersMenu customersMenu = new CustomersMenu();
            customersMenu.Show();
            customersMgtBtn.IsEnabled = false;
        }

        private void buyBtn_Click(object sender, RoutedEventArgs e)
        {
            BookPurchase bookPurchase = new BookPurchase(username);
            bookPurchase.Show();
        }

        public static void changeBookBtn(bool isEnable)
        {
            //customersMgtBtn.IsEnabled = isEnable;
            bookMenuBtnEnable = true;
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            EmployeeRegister employeeRegister = new EmployeeRegister();
            employeeRegister.Show();
        }
    }


}
