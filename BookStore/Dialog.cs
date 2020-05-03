using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace BookStore
{
    class Dialog
    {
        public static bool Confirm(string command)
        {
            var result = MessageBox.Show("Do you want to " + command + "?", command + " Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else return false;
           
        }
    }
}
