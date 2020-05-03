using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    class DataAccess
    {
        private static string dbpath = "DataStorage.db";
        private static string searchResult = "";
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                //Create Books table
                String createBooksTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Books (ISBN NVARCHAR(14) PRIMARY KEY, " +
                    "Title NVARCHAR(50) NOT NULL, " +
                    "Author NVARCHAR(100) NOT NULL, " +
                    "Description NVARCHAR(200) NULL, " +
                    "Price smallmoney NOT NULL)";
                SqliteCommand createBooksTable = new SqliteCommand(createBooksTableCommand, db);
                createBooksTable.ExecuteReader();

                //Create Customers table
                String createCustomersTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Customers (Customer_Id NVARCHAR(10) PRIMARY KEY, " +
                    "Customer_Name NVARCHAR(100) NOT NULL, " +
                    "Address NVARCHAR(200) NOT NULL, " +
                    "Email NVARCHAR(50) NOT NULL UNIQUE)";
                SqliteCommand createCustomersTable = new SqliteCommand(createCustomersTableCommand, db);
                createCustomersTable.ExecuteReader();

                //Create Transactions table
                String createTransactionsTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Transactions (Purchase_Id INTEGER PRIMARY KEY, " +
                    "ISBN NVARCHAR(14) NOT NULL, " +
                    "Customer_Id NVARCHAR(10) NOT NULL, " +
                    "Quantity INTEGER NOT NULL, " +
                    "Total_Price smallmoney NOT NULL, " +
                    "Date DATE NOT NULL, " +
                    "Cashier NVARCHAR(100) NOT NULL)";
                SqliteCommand createTransactionsTable = new SqliteCommand(createTransactionsTableCommand, db);
                createTransactionsTable.ExecuteReader();
            }

        }
        public static void AddData(string tableName, string key, string input1, string input2, string input3, string input4)
        {
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertDataCommand = new SqliteCommand();
                insertDataCommand.Connection = db;

                if (tableName == "Books")
                {
                    string ISBN = key;
                    string title = input1;
                    string author = input2;
                    string desscription = input3;
                    float price = float.Parse(input4);

                    insertDataCommand.CommandText = "INSERT INTO Books (ISBN, Title, Author, Description, Price) " + 
                        "VALUES (@ISBN, @Title, @Author, @Description, @Price);";
                    insertDataCommand.Parameters.AddWithValue("@ISBN", ISBN);
                    insertDataCommand.Parameters.AddWithValue("@Title", title);
                    insertDataCommand.Parameters.AddWithValue("@Author", author);
                    insertDataCommand.Parameters.AddWithValue("@Description", desscription);
                    insertDataCommand.Parameters.AddWithValue("@Price", price);
                }
                else if (tableName == "Customers")
                {
                    string customerId = key;
                    string customerName = input1;
                    string address = input2;
                    string email = input3;

                    insertDataCommand.CommandText = "INSERT INTO Customers (Customer_Id, Customer_Name, Address, Email) " + 
                        "VALUES (@Customer_Id, @Customer_Name, @Address, @Email);";
                    insertDataCommand.Parameters.AddWithValue("@Customer_Id", customerId);
                    insertDataCommand.Parameters.AddWithValue("@Customer_Name", customerName);
                    insertDataCommand.Parameters.AddWithValue("@Address", address);
                    insertDataCommand.Parameters.AddWithValue("@Email", email);
                }
                insertDataCommand.ExecuteReader();
                db.Close();
            }
        }

        // AddData Transactions
        /*String createTransactionsTableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS Transactions (Purchase_Id INTEGER PRIMARY KEY, " +
                    "ISBN VARCHAR(14) NOT NULL, " + "Customer_Id INTEGER NOT NULL, " +
                    "Quantity INTEGER NOT NULL, " + "Total_Price smallmoney NOT NULL, " +
                    "Date DATE NOT NULL, " + "Cashier NVARCHAR(100) NOT NULL)";*/
        public static void AddData(string tableName, string isbn, string customerId, int quantity, float totalPrice, string date, string cashier)
        {
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertDataCommand = new SqliteCommand();
                insertDataCommand.Connection = db;

                if (tableName == "Transactions")
                {

                    insertDataCommand.CommandText = "INSERT INTO Transactions (ISBN, Customer_Id, Quantity, Total_Price, Date, Cashier) " +
                        "VALUES (@ISBN, @Customer_Id, @Quantity, @Total_Price, @Date, @Cashier);";
                    //insertDataCommand.Parameters.AddWithValue("@Purchase_Id", purchaseId);
                    insertDataCommand.Parameters.AddWithValue("@ISBN", isbn);
                    insertDataCommand.Parameters.AddWithValue("@Customer_Id", customerId);
                    insertDataCommand.Parameters.AddWithValue("@Quantity", quantity);
                    insertDataCommand.Parameters.AddWithValue("@Total_Price", totalPrice);
                    insertDataCommand.Parameters.AddWithValue("@Date", date);
                    insertDataCommand.Parameters.AddWithValue("@Cashier", cashier);

                }
                insertDataCommand.ExecuteReader();
                db.Close();
            }
        }
            public static List<String> SearchItem(string tableName, string searchField, string searchItem)
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand searchCommand = new SqliteCommand();

                if (searchField == "All")
                {
                    string item1 = searchItem;
                    string item2 = searchItem;
                    string item3 = searchItem;
                    if (tableName == "Books")
                    {
                        //select* from table_name where name like '%value%'
                        //searchCommand.CommandText = "SELECT * FROM " + tableName + " WHERE (ISBN = @item1 Or Title = @item2 Or Author = @item3);";
                        searchCommand.CommandText = "SELECT * FROM " + tableName + " WHERE (ISBN like '%" + @item1 + "%' Or " +
                            "Title like '%" + @item2 + "%' Or Author like '%" + @item3 + "%');";

                    }
                    else if (tableName == "Customers")
                    {
                        searchCommand.CommandText = "SELECT * FROM " + tableName + " WHERE (Customer_Id = @item1 Or Customer_Name = @item2 Or Email = @item3);";
                        searchCommand.CommandText = "SELECT * FROM " + tableName + " WHERE (Customer_Id like '%" + @item1 + "%' Or " +
                            "Customer_Name like '%" + @item2 + "%' Or Email like '%" + @item3 + "%');";
                    }
                    searchCommand.Parameters.AddWithValue("@item1", searchItem);
                    searchCommand.Parameters.AddWithValue("@item2", searchItem);
                    searchCommand.Parameters.AddWithValue("@item3", searchItem);
                }
                else
                {
                    searchCommand.CommandText = "SELECT * FROM " + tableName + " WHERE " + searchField + " like '%" + @searchItem + "%';";
                    //searchCommand.CommandText = "SELECT * FROM " + tableName + " WHERE " + searchField + " = @searchItem;";
                    searchCommand.Parameters.AddWithValue("@searchItem", searchItem);
                }
                searchCommand.Connection = db;
                SqliteDataReader query = searchCommand.ExecuteReader();

                while (query.Read())
                {
                    int i = 0;
                    searchResult = "";
                    while (i < query.FieldCount)
                    {
                        if (i == 0)
                        {
                            searchResult += query.GetString(i);
                        }
                        else
                        {
                            searchResult += "," + query.GetString(i);
                        }
                        i++;
                    }
                    //entries.Add(query.GetString(0) + "," + query.GetString(1) + "," + query.GetString(2) + "," + query.GetString(3) + "," + query.GetString(4));
                    entries.Add(searchResult);
                }
                db.Close();
            }
            return entries;
        }

        public static void UpdateData(string tableName, string key, string input1, string input2, string input3, string input4)
        {
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand updateDataCommand = new SqliteCommand();
                updateDataCommand.Connection = db;

                if (tableName == "Books")
                {
                    string ISBN = key;
                    string title = input1;
                    string author = input2;
                    string desscription = input3;
                    float price = float.Parse(input4);

                    updateDataCommand.CommandText = "Update Books SET Title = @Title, Author = @Author, Description = @Description, Price = @Price Where ISBN = @ISBN;";
                    updateDataCommand.Parameters.AddWithValue("@ISBN", ISBN);
                    updateDataCommand.Parameters.AddWithValue("@Title", title);
                    updateDataCommand.Parameters.AddWithValue("@Author", author);
                    updateDataCommand.Parameters.AddWithValue("@Description", desscription);
                    updateDataCommand.Parameters.AddWithValue("@Price", price);
                }
                if (tableName == "Customers")
                {
                    string customerId = key;
                    string customerName = input1;
                    string address = input2;
                    string email = input3;
                    updateDataCommand.CommandText = "UPDATE Customers SET Customer_Name = @Customer_Name, Address = @Address, Email = @Email WHERE Customer_Id = @Customer_Id;";
                    updateDataCommand.Parameters.AddWithValue("@Customer_Id", customerId);
                    updateDataCommand.Parameters.AddWithValue("@Customer_Name", customerName);
                    updateDataCommand.Parameters.AddWithValue("@Address", address);
                    updateDataCommand.Parameters.AddWithValue("@Email", email);
                }

                updateDataCommand.ExecuteReader();
                db.Close();
            }
        }
        public static void DeleteData(string tableName, string deleteKey)
        {
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();
                SqliteCommand deleteDataCommand = new SqliteCommand();
                deleteDataCommand.Connection = db;

                if (tableName == "Books")
                {
                    deleteDataCommand.CommandText = "Delete From Books Where ISBN = @ISBN;";
                    deleteDataCommand.Parameters.AddWithValue("@ISBN", deleteKey);
                }
                if (tableName == "Customers")
                {
                    deleteDataCommand.CommandText = "Delete From Customers Where Customer_Id = @Customer_Id);";
                    deleteDataCommand.Parameters.AddWithValue("@Customer_Id", deleteKey);
                }
                deleteDataCommand.ExecuteReader();
                db.Close();
            }
        }
    }
}
