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
            else
            {
                btnAdmin.Visibility = Visibility.Hidden;
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
        }

        private void customersMgtBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomersMenu customersMenu = new CustomersMenu();
            customersMenu.Show();
        }

        private void buyBtn_Click(object sender, RoutedEventArgs e)
        {
            PurchaseItem bookPurchase = new PurchaseItem(username);
            bookPurchase.Show();
        }


        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            EmployeeRegister employeeRegister = new EmployeeRegister();
            employeeRegister.Show();
        }

        private void btnPurchaseHistory_Click(object sender, RoutedEventArgs e)
        {
            PurchaseHistory purchaseHistory = new PurchaseHistory();
            purchaseHistory.Show();
        }
    }


}
