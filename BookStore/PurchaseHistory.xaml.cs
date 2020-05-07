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
    /// Interaction logic for PurchaseHistory.xaml
    /// </summary>
    public partial class PurchaseHistory : Window
    {
        public PurchaseHistory()
        {
            InitializeComponent();
            createTable();

        }
        private void createTable()
        {
            List<TransactionList> purchaseHistory = new List<TransactionList>();
            List<List<string>> searchResult = DataAccess.SearchItem("Transactions", "Purchase_Id", "");
            int numberOfList = searchResult.Count();
            for (int j = 0; j < numberOfList; j++)
            {
                purchaseHistory.Add(new TransactionList(int.Parse(searchResult[j][0]), searchResult[j][1], searchResult[j][2],
                    int.Parse(searchResult[j][3]), float.Parse(searchResult[j][4]), searchResult[j][5], searchResult[j][6]));
            }
            tablePurchaseHistory.ItemsSource = purchaseHistory;
        }
    }
}
