using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using MaterialDesignThemes.Wpf;

namespace StockControl
{
    public static class Data
    {
        static public ObservableDictionary<int, Department> Departments { get; set; } = new ObservableDictionary<int, Department>();
        static public ObservableDictionary<int, Product> Products { get; set; } = new ObservableDictionary<int, Product>();
        static public ObservableDictionary<int, Employee> Employees { get; set; } = new ObservableDictionary<int, Employee>();
        static public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public enum EmployeeTypes : byte
        {
            WarehouseWorker, WarehousePacker, MaterialHandler
        };
        static public readonly Regex NumRegex = new Regex("[^0-9]+");
        static public readonly TimeSpan SnackbarMessageTime = TimeSpan.FromMilliseconds(2000);


        public static void LoadAll()
        {
            //Loads all the data from the csv files to the instances above (Departments,Products,Employees).
            //csv files --> application
        }
        public static void StoreAll()
        {
            //Stores all the data from instances above (Departments,Products,Employees) to the csv files.
            //application --> csv files
        }
        public static void ResetAll()
        {
            //Resets all the data from instances above (Departments,Products,Employees) and the csv files.
            //application --> null
            //csv files --> null
        }
    }
}
