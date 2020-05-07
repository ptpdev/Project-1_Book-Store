using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Security.Cryptography;
using System.Windows;

namespace BookStore
{
    public class LoginSystem
    {
        static string dbpath = "DB_UserLogin.db";
        private const int saltSize = 24;     // Number of bytes of the salt
        private const int iterations = 1000;  // Number of times we iterate the function
                                              // The more we iterate, the more it is gonna take time.
                                              //     The advantage of a great iterations number is to 
                                              //     make brutforce attack more painful.
        private const int hashSize = 20;      // Number of bytes of the hash (the output)
        public static void InitializeLoginSystem()
        {
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS UserLogin (Username nvarchar(100) PRIMARY KEY, " +
                    "Salt NVARCHAR(1000) NOT NULL, " +
                    "Password_Encrypt NVARCHAR(1000) NOT NULL, " +
                    "Name NVARCHAR(100) NOT NULL);";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);
                createTable.ExecuteReader();

                if (hasRows() == false)
                {
                    EmployeeRegister adminRegis = new EmployeeRegister("admin");
                    adminRegis.Show();
                }
            }
        }
        public static bool hasRows()
        {
            List<String> entries = new List<string>();

            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                selectCommand.CommandText = "SELECT * FROM UserLogin;";

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }
                //MessageBox.Show("Number of user = " + entries.Count().ToString());
                
                db.Close();
                
                if (entries.Count() > 0) return true;
                return false;        
            }                       
        }
        public static bool AddData(string username, string password, string name)
        {
            if (hasUser(username) == true)
            {
                return false;
            }
            List<string> saltAndHash = encrypt(password);

            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO UserLogin VALUES (@Username, @Salt, @Hash, @Name);";
                insertCommand.Parameters.AddWithValue("@Username", username);
                insertCommand.Parameters.AddWithValue("@Salt", saltAndHash[0]);
                insertCommand.Parameters.AddWithValue("@Hash", saltAndHash[1]);
                insertCommand.Parameters.AddWithValue("@Name", name);

                insertCommand.ExecuteReader();                
                db.Close();
            }
            return true;
        }

        public static bool hasUser(string username)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                List<string> entries = new List<string>();
                db.Open();
                SqliteCommand checkUserCommand = new SqliteCommand();
                checkUserCommand.Connection = db;

                checkUserCommand.CommandText = "Select * from UserLogin Where Username = @Username;";
                checkUserCommand.Parameters.AddWithValue("@Username", username);
                SqliteDataReader query = checkUserCommand.ExecuteReader();
                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }
                db.Close();

                if (entries.Count() > 0) return true;
                return false;
            }
        }
        public static List<string> encrypt(string input)
        {

            List<string> saltAndHash = new List<string>();

            var deriveBytes = new Rfc2898DeriveBytes(input, saltSize, iterations);
            byte[] salt = deriveBytes.Salt;
            byte[] hash = deriveBytes.GetBytes(hashSize);
            saltAndHash.Add(Convert.ToBase64String(salt));
            saltAndHash.Add(Convert.ToBase64String(hash));

            return saltAndHash;
        }

        public static bool checkPassword(string username, string password)
        {
            List<String> entries = new List<string>();

            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;

                selectCommand.CommandText = "SELECT Salt, Password_Encrypt from UserLogin Where Username = @username;";
                selectCommand.Parameters.AddWithValue("@username", username);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                    entries.Add(query.GetString(1));
                }

                db.Close();
            }
            if (entries.Count() == 0) return false;

            byte[] saltCheck = Convert.FromBase64String(entries[0]);
            var deriveBytes = new Rfc2898DeriveBytes(password, saltCheck, iterations);
            byte[] hashCheck = deriveBytes.GetBytes(hashSize);
            if (Convert.ToBase64String(hashCheck) == entries[1]) return true;
            
            return false;
        }

        public static string getEmployee(string username)
        {
            string entries;

            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;
                selectCommand.CommandText = "SELECT Name from UserLogin Where Username = @username;";
                selectCommand.Parameters.AddWithValue("@username", username);

                SqliteDataReader query = selectCommand.ExecuteReader();

                query.Read();
                entries = query.GetString(0);

                db.Close();
            }

            return entries;
        }
    }

    
}
