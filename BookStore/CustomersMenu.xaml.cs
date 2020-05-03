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
using System.Xml;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for CustomersMenu.xaml
    /// </summary>
    public partial class CustomersMenu : Window
    {
        //List<CustomerList> customerList;

        private List<String> searchResult;
        private string tableName = "Customers";
        private string customerId;
        public CustomersMenu()
        {
            InitializeComponent();
        }
        private void CustomersMenu_Loaded(object sender, RoutedEventArgs e)
        {
            searchList.Items.Add("ทั้งหมด");
            searchList.Items.Add("รหัสสมาชิก");
            searchList.Items.Add("ชื่อลูกค้า");
            searchList.Items.Add("Email");
            searchList.Text = "ทั้งหมด";

            /*customerList = new List<CustomerList>();

            customerList.Add(new CustomerList("001", "Piti", "asdasd@gmail.com"));
            customerList.Add(new CustomerList("005", "Upza", "za007@gmail.com"));

            customersListView.ItemsSource = customerList;*/

        }
        public class Table
        {
            public string customerId { get; set; }

            public string customerName { get; set; }

            public string email { get; set; }

        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            searchResult = DataAccess.SearchItem(tableName, "Customer_Id", customerIdTextBox.Text);
            if (!searchResult.Any())
            {
                DataAccess.AddData(tableName, customerIdTextBox.Text, customerNameTextBox.Text, addressTextBox.Text, emailTextBox.Text, "");
            }
            else MessageBox.Show("Customer Id : " + customerIdTextBox.Text + " already existed.");

            customerIdTextBox.Text = "";
            customerNameTextBox.Text = "";
            addressTextBox.Text = "";
            emailTextBox.Text = "";
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            string searchItem = searchTextBox.Text;
            string searchField = searchList.Text;
            List<string> searchResult = new List<string>();

            if (searchField == "รหัสสมาชิก")
            {
                searchField = "Customer_Id";
            }
            else if (searchField == "ชื่อลูกค้า")
            {
                searchField = "Customer_Name";
            }
            else if (searchField == "ทั้งหมด")
            {
                searchField = "All";
            }
            if (searchField == "All")
            {
                //MessageBox.Show("Go all search.");
                searchResult = DataAccess.SearchItem(tableName, searchField, searchItem);
                dataSearchShow(searchResult);
            }
            else if (searchField == "Customer_Id" || searchField == "Customer_Name" || searchField == "Email")
            {
                searchResult = DataAccess.SearchItem(tableName, searchField, searchItem);
                dataSearchShow(searchResult);
            }
            else { MessageBox.Show("Search Error."); }
        }

        public void dataSearchShow(List<string> searchResult)
        {
            List<String> dataFound = new List<string>();
            int i = 0;
            foreach (string ee in searchResult)
            {
                dataFound.Add(ee);
                i++;
            }
            if (dataFound.Count == 0)
            {
                MessageBox.Show("Data Not Found.");
            }
            else
            {
                List<DataList> customerList = new List<DataList>(); 
                List<Table> items = new List<Table>();
                int numberOfList = dataFound.Count();
                for (int j = 0; j < numberOfList; j++)
                {
                    string[] dataShow = dataFound[j].Split(',');
                    /*customerIdTextBox.Text = dataShow[0];
                    customerNameTextBox.Text = dataShow[1];
                    addressTextBox.Text = dataShow[2];
                    emailTextBox.Text = dataShow[3];*/
                    //items.Add(new Table() { customerId = dataShow[0], customerName = dataShow[1], email = dataShow[3] });
                    dataShow[0] = dataShow[0].PadLeft(6, '0');
                    customerList.Add(new DataList(dataShow[0], dataShow[1], dataShow[2], dataShow[3]));
                }
                customersListView.ItemsSource = customerList;
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            List<String> searchResult;
            searchResult = DataAccess.SearchItem(tableName, "Customer_Id", customerIdTextBox.Text);
            if (!searchResult.Any())
            {
                MessageBox.Show("Unable to update data.\nCustomer ID : " + customerIdTextBox.Text + " not existed.");
            }
            else
            {
                foreach (string ee in searchResult)
                {
                    MessageBox.Show(ee);
                }
                if (Dialog.Confirm("Edit"))
                {
                    DataAccess.UpdateData(tableName, customerIdTextBox.Text, customerNameTextBox.Text, addressTextBox.Text, emailTextBox.Text, "");
                    MessageBox.Show("Update succesfully");
                }
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Dialog.Confirm("Delete"))
            {
                DataAccess.DeleteData(tableName, customerIdTextBox.Text);
                MessageBox.Show("Delete succesfully");
            }
        }

        private void customersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataList selectedCustomer = (DataList)customersListView.SelectedItem;
            if (selectedCustomer != null)
            {
                this.customerId = selectedCustomer.CustomerId;
                customerIdTextBox.Text = selectedCustomer.CustomerId;
                customerNameTextBox.Text = selectedCustomer.CustomerName;
                addressTextBox.Text = selectedCustomer.Address;
                emailTextBox.Text = selectedCustomer.Email;
            }

        }

        private void customerIdTextBox_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            customerIdTextBox.Text = customerIdTextBox.Text.PadLeft(6, '0');
        } 
    }

}
