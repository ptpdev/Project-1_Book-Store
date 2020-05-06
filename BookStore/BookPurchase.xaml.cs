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
    /// Interaction logic for BookPurchase.xaml
    /// </summary>
    public partial class BookPurchase : Window
    {

        private bool bookvalid = false;
        private bool customervalid = false;
        private string isbn;
        private string title;
        private string customerId;
        private int bookquantity = 1;
        private float price;
        private float sumPrice;
        private float totalPrice;
        private string date;

        List<PurchaseList> purchaseList = new List<PurchaseList>();
        List<List<string>> searchResult = new List<List<string>>();
        PurchaseList order = new PurchaseList();

        public BookPurchase(string cashier)
        {
            InitializeComponent();
            quantityTxt.Text = "";
            order.Cashier = cashier;
            totalPrice = 0;
        }

        private void isbnSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string searchItem = isbnTxt.Text;
            string searchField = "ISBN";            
            quantityTxt.Text = bookquantity.ToString();

            searchResult = DataAccess.SearchItemExact("Books", searchField, searchItem);
            List<List<string>> dataFound = new List<List<string>>();
            foreach (List<string> item in searchResult)
            {
                dataFound.Add(item);
            }
            if (!dataFound.Any())
            {
                MessageBox.Show("Data Not Found.");
            }
            else
            {
                titleTxt.Content = dataFound[0][1];
                authorTxt.Content = dataFound[0][2];
                descriptionTxt.Content = dataFound[0][3];
                priceTxt.Content = dataFound[0][4];
                bookvalid = true;
                isbn = dataFound[0][0];
                price = float.Parse(dataFound[0][4]);
                bookquantity = 1;
            }
        }

        private void increaseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookquantity < 1000)
            {
                bookquantity++;
                quantityTxt.Text = bookquantity.ToString();
            }
        }

        private void decreaseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookquantity > 1)
            {
                bookquantity--;
                quantityTxt.Text = bookquantity.ToString();
            }
        }

        private void quantityTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bookquantity > 0 && bookquantity <= 999) {

            }
            else if (bookquantity > 999)
            {
                quantityTxt.Text = "999";
            } else
            {
                MessageBox.Show("กรุณาใส่จำนวนหนังสือให้ถูกต้อง");
            }
        }

        private void customerIdSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            string searchItem = customerIdTxt.Text;
            string searchField = "Customer_Id";
            List<List<string>> dataFound = new List<List<string>>();
            searchResult = DataAccess.SearchItemExact("Customers", searchField, searchItem);
            foreach (List<string> item in searchResult)
            {
                dataFound.Add(item);
            }
            if (!dataFound.Any())
            {
                MessageBox.Show("Data Not Found.");
            }
            else
            {
                customerNameTxt.Content = dataFound[0][1];
                order.CustomerName = dataFound[0][1];
                customervalid = true;
                order.CustomerId = dataFound[0][0];

            }
        }

        private void addToCartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookvalid)
            {
                title = titleTxt.Content.ToString();
                sumPrice = bookquantity * price;
                order.SumPrice = sumPrice;
                purchaseList.Add(new PurchaseList(isbn, title, price, bookquantity, sumPrice));
                purchaseListView.ItemsSource = purchaseList;
                purchaseListView.Items.Refresh();
                bookvalid = false;

                totalPrice += order.SumPrice;
                totalPriceTxt.Content = totalPrice;
                order.TotalPrice = totalPrice;

                isbnTxt.Text = "";
                titleTxt.Content = "";
                authorTxt.Content = "";
                descriptionTxt.Content = "";
                priceTxt.Content = "";
                quantityTxt.Text = "";
            }
            else
            {
                MessageBox.Show("กรุณาเลือกหนังสือ");
            }
        }

        public string getDate()
        {
            DateTime dateTime = DateTime.Now;
            string year = dateTime.Year.ToString();
            string month = dateTime.Month.ToString();
            string day = dateTime.Day.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            date = year + "-" + month + "-" + day;

            return date;
        }

        private void purchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            // เอา sumPrice ออกมารวมกันให้ได้
            //PurchaseList purchaseSum = (PurchaseList)purchaseListView;

            //PurchaseSummary purchaseSummary = new PurchaseSummary(isbn, customerId, bookquantity, price, getDate(), cashier);
            //PurchaseSummary purchaseSummary = new PurchaseSummary();
            //purchaseSummary.Show();
            if (customervalid == false)
            {
                MessageBox.Show("กรุณาใส่รหัสสมาชิก");
            }
            else
            {
                MessageBoxResult m = MessageBox.Show("กดตกลงเพื่อยืนยันคำสั่งซื้อ", "ยืนยันคำสั่งซื้อ", MessageBoxButton.OKCancel);
                switch (m)
                {

                    case MessageBoxResult.OK:

                        foreach (var item in purchaseListView.Items)
                        {
                            var a = item as PurchaseList;
                            if (a != null)
                            {
                                string isbn = a.Isbn;
                                int bookquantity = a.Quantity;
                                float sumPrice = a.SumPrice;
                                //MessageBox.Show(isbn + customerId + bookquantity + sumPrice + getDate() + order.Cashier);
                                DataAccess.AddData("Transactions", isbn, order.CustomerId, bookquantity, sumPrice, getDate(), order.Cashier);
                            }
                        }
                        PurchaseSummary purchaseSummary = new PurchaseSummary(purchaseList, order);
                        purchaseSummary.Show();
                        this.Close();
                        break;

                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            

        }


    }
}
