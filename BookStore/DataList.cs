using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class DataList
    {

        /*public CustomerList(string customerId, string customerName, string email)
        {

        }*/
        private string customerId;
        private string customerName;
        private string address;
        private string email;
        private string isbn;
        private string title;
        private string author;
        private string description;
        private float price;
        private string cashier;
        private int quantity;
        private float sumPrice;
        private float totalPrice;

        /*public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string Cashier { get; set; }*/

        public DataList()
        {

        }

        public DataList(string customerId, string customerName, string address, string email)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            Address = address;
            Email = email;
        }

        public DataList(string isbn, string title, string author, string description, float price)
        {
            Isbn = isbn;
            Title = title;
            Author = author;
            Description = description;
            Price = price;
        }

        public string CustomerId { get => customerId; set => customerId = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }
        public string Isbn { get => isbn; set => isbn = value; }
        public string Title { get => title; set => title = value; }
        public string Author { get => author; set => author = value; }
        public string Description { get => description; set => description = value; }
        public float Price { get => price; set => price = value; }
        public string Cashier { get => cashier; set => cashier = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public float SumPrice { get => sumPrice; set => sumPrice = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
    }

    public class PurchaseList : DataList
    {
        /*public int Quantity { get; set; }
        public float SumPrice { get; set; }
        public float TotalPrice { get; set; }*/
        public PurchaseList()
        {

        }
        
        public PurchaseList(string isbn, string title, float price, int quantity, float sumPrice)
        {
            Isbn = isbn;
            Title = title;
            Price = price;
            Quantity = quantity;
            SumPrice = sumPrice;
        }
    } 
}
