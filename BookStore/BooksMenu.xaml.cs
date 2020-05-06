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
using System.ComponentModel; // CancelEventArgs

namespace BookStore
{
    /// <summary>
    /// Interaction logic for BooksMenu.xaml
    /// </summary>
    public partial class BooksMenu : Window
    {
        public BooksMenu()
        {
            InitializeComponent();
            booksMenu_Load();
        }

        private void booksMenu_Load()
        {
            searchList.Items.Add("ทั้งหมด");
            searchList.Items.Add("ISBN");
            searchList.Items.Add("ชื่อหนังสือ");
            searchList.Items.Add("ชื่อผู้เขียน");
            searchList.Text = "ทั้งหมด";
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            List<List<string>> searchResult;
            searchResult = DataAccess.SearchItemExact("Books", "ISBN", isbnTextBox.Text);   //ค้นหาว่ามี ISBN ซ้ำรึยัง
            if (!searchResult.Any())
            {
                DataAccess.AddData("Books", isbnTextBox.Text, titleTextBox.Text, authorTextBox.Text, descriptionTextBox.Text, priceTextBox.Text);
            }
            else MessageBox.Show("ISBN: " + isbnTextBox.Text + " already existed.");
            isbnTextBox.Text = "";
            titleTextBox.Text = "";
            authorTextBox.Text = "";
            descriptionTextBox.Text = "";
            priceTextBox.Text = "";
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            string searchItem = searchTextBox.Text;
            string searchField = searchList.Text;
            List<List<string>> searchResult = new List<List<string>>();

            if (searchField == "ชื่อหนังสือ")
            {
                searchField = "Title";
            }else if ( searchField == "ชื่อผู้เขียน")
            {
                searchField = "Author";
            }else if (searchField == "ทั้งหมด")
            {
                searchField = "All";
            }

            if (searchField == "All")
            {
                searchResult = DataAccess.SearchItem("Books", searchField, searchItem);
                dataSearchShow(searchResult);
            }
            else if (searchField == "ISBN" || searchField == "Title" || searchField == "Author" )
            {
                searchResult = DataAccess.SearchItem("Books", searchField, searchItem);
                dataSearchShow(searchResult);
            }
            else { MessageBox.Show("Search Error."); }
        }

        public void dataSearchShow(List<List<string>> searchResult)
        {
            List<List<string>> dataFound = new List<List<string>>();
            int i = 0;
            foreach (List<string> searchItem in searchResult)
            {
                dataFound.Add(searchItem);
                i++;
            }
            if (dataFound.Count == 0)
            {
                MessageBox.Show("Data Not Found.");
            }
            else
            {
                List<DataList> bookslist = new List<DataList>();
                int numberOfList = dataFound.Count();
                for (int j = 0; j < numberOfList; j++)
                {                    
                    bookslist.Add(new DataList(dataFound[j][0], dataFound[j][1], dataFound[j][2], dataFound[j][3], float.Parse(dataFound[j][4])));
                }
                booksListView.ItemsSource = bookslist;
            }
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            List<List<string>> searchResult = new List<List<string>>();
            searchResult = DataAccess.SearchItemExact("Books", "ISBN", isbnTextBox.Text);   //เช็คว่ามี isbn นี้อยู่จริง ก่อนที่จะแก้ไข
            if (!searchResult.Any())
            {
                MessageBox.Show("Unable to update data.\nISBN: " + isbnTextBox.Text + " not existed.");
            }
            else
            {
                /*foreach(string ee in searchResult)
                {
                    MessageBox.Show(ee);
                }*/

                if (Dialog.Confirm("Edit"))
                {
                    DataAccess.UpdateData("Books", isbnTextBox.Text, titleTextBox.Text, authorTextBox.Text, descriptionTextBox.Text, priceTextBox.Text);
                    MessageBox.Show("Update succesfully");
                }
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Dialog.Confirm("Delete"))
            {
                DataAccess.DeleteData("Books", isbnTextBox.Text);
                MessageBox.Show("Delete succesfully");
            }
        }

        private void booksListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataList selectedBook = (DataList)booksListView.SelectedItem;
            if (selectedBook != null)
            {
                isbnTextBox.Text = selectedBook.Isbn;
                titleTextBox.Text = selectedBook.Title;
                authorTextBox.Text = selectedBook.Author;
                descriptionTextBox.Text = selectedBook.Description;
                priceTextBox.Text = selectedBook.Price.ToString();
            }

        }
        //public event System.ComponentModel.CancelEventHandler Closing;
        public void BooksMenu_Closing(object sender, CancelEventArgs e)
        {
            MainMenu.changeBookBtn(true);
        }

    }
}
