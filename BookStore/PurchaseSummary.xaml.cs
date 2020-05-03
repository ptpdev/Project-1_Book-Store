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
    /// Interaction logic for PurchaseSummary.xaml
    /// </summary>
    public partial class PurchaseSummary : Window
    {
        private string isbn;
        private string customerId;
        private int bookquantity;
        private float price;
        private float totalPrice;
        private string date;
        private string cashier;
        public PurchaseSummary()
        {

        }
        public PurchaseSummary(string isbn, string customerId, int bookquantity, float price, string date, string cashier)
        {
            InitializeComponent();
            this.isbn = isbn;
            this.customerId = customerId;
            this.bookquantity = bookquantity;
            this.price = price;
            this.date = date;
            this.cashier = cashier;
            //totalPrice = bookquantity * price;
        }

        public PurchaseSummary(List<PurchaseList> purchaseList, PurchaseList order)
        {
            InitializeComponent();
            summaryListView.ItemsSource = purchaseList;
            customerNameTxt.Content = order.CustomerName;
            cashierTxt.Content = order.Cashier;
            totalPriceTxt.Content = order.TotalPrice.ToString();
        }
        private void purchaseConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
