using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using MaterialDesignThemes.Wpf;
using System.Data;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Controls;

namespace StockControl
{
    public static class Data
    {
        static public ObservableDictionary<int, Department> Departments { get; set; } = new ObservableDictionary<int, Department>();//Dictionary for departments
        static public ObservableDictionary<int, Product> Products { get; set; } = new ObservableDictionary<int, Product>();//Dictionary for products
        static public ObservableDictionary<int, Employee> Employees { get; set; } = new ObservableDictionary<int, Employee>();//Dictionary for employees
        static public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();//Dictionary for orders
        public enum EmployeeTypes : byte
        {
            WarehouseWorker, WarehousePacker, MaterialHandler
        };//Enum for employee types
        static public readonly Regex NumRegex = new Regex("[^0-9]+");//Regex for numbers used to check numbers validation
        static public readonly Regex DoubleRegex = new Regex("[^0-9.]");//Regex for doubles used to check doubles validation
        static public readonly Regex NameRegex = new Regex("[,;@#$%^&*()~_=+-]+");//Regex for names used to check names validation
        static public readonly TimeSpan SnackbarMessageTime = TimeSpan.FromMilliseconds(2000);//A global time for all the notifations

        static public readonly string CSVRoot = Directory.GetCurrentDirectory()+ "/CSV_FILE";//A directory where all the csv files will be saved

        //tblSettingsParams
        private static void SaveParams()
        {
            StreamWriter sw = new StreamWriter(CSVRoot + "/settings_data.csv", false);
            sw.Write(SettingsParams.Tax);
            sw.Write(",");
            sw.Write(SettingsParams.MaterialHandlerWage);
            sw.Write(",");
            sw.Write(SettingsParams.WarehouseWorkerWage);
            sw.Write(",");
            sw.Write(SettingsParams.WarehousePackerWage);
            sw.Close();
        }//Saves all the settings params
        private static void ReadParams()
        {
            var st = new StreamReader(CSVRoot + "/settings_data.csv");
            var line = st.ReadLine();
            var values = line.Split(',');
            SettingsParams.Tax = Convert.ToDouble(values[0]);
            SettingsParams.MaterialHandlerWage = Convert.ToDouble(values[2]);
            SettingsParams.WarehouseWorkerWage = Convert.ToDouble(values[3]);
            SettingsParams.WarehousePackerWage = Convert.ToDouble(values[3]);
        }//Reads all the settings params


        //tblProducts
        static private DataTable productsTable = new DataTable();
        public static void BuildProductsTable()
        {
            productsTable.Columns.Add("ID", typeof(int));
            productsTable.Columns.Add("productName", typeof(string));
            productsTable.Columns.Add("departmentID", typeof(int));
            productsTable.Columns.Add("sellingPrice", typeof(double));
            productsTable.Columns.Add("buyingPrice", typeof(double));
        }//Builds the products table
        private static void FillProductsTable()
        {
            BuildProductsTable();
            foreach (var item in Products)
            {
                productsTable.Rows.Add(item.Key, item.Value.Name, item.Value.DepartmentID, item.Value.SellingPrice, item.Value.BuyingPrice);
            }
        }//Fills the products table
        private static void SaveProducts()
        {
            StreamWriter sw = new StreamWriter(CSVRoot + "/products_data.csv", false);
            //headers    
            for (int i = 0; i < productsTable.Columns.Count; i++)
            {
                sw.Write(productsTable.Columns[i]);
                if (i < productsTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in productsTable.Rows)
            {
                for (int i = 0; i < productsTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < productsTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }//Saves the products in the csv file
        private static void ReadProducts()
        {
            var st = new StreamReader(CSVRoot + "/products_data.csv");
            var line1 = st.ReadLine();//Skip Line
            while (!st.EndOfStream)
            {
                var line = st.ReadLine();
                var values = line.Split(',');
                Products.Add(Convert.ToInt32(values[0]), new Product(values[1], Convert.ToInt32(values[2]), Convert.ToDouble(values[3]), Convert.ToDouble(values[4])));
            }
        }//Reads the products in the csv file

        //tblEmployees
        static private DataTable employeesTable = new DataTable();
        public static void BuildEmployeesTable()
        {
            employeesTable.Columns.Add("ID", typeof(int));
            employeesTable.Columns.Add("Name", typeof(string));
            employeesTable.Columns.Add("departmentId", typeof(int));
            employeesTable.Columns.Add("gender", typeof(string));
            employeesTable.Columns.Add("dateOfBirth", typeof(DateTime));
            employeesTable.Columns.Add("employeeType", typeof(Data.EmployeeTypes));
            employeesTable.Columns.Add("special", typeof(int));
        }//Builds the employees table
        private static void FillEmployeesTable()
        {
            BuildEmployeesTable();
            foreach (var item in Employees)
            {
                if (item.Value is WarehousePacker wp)
                {
                    employeesTable.Rows.Add(item.Key, wp.Name, wp.DepartmentID, wp.Gender, wp.DateOfBirth.Date, Data.EmployeeTypes.WarehousePacker, wp.ProductsPacked);
                }
                else if (item.Value is WarehouseWorker ww)
                {
                    employeesTable.Rows.Add(item.Key, ww.Name, ww.DepartmentID, ww.Gender, ww.DateOfBirth.Date, Data.EmployeeTypes.WarehouseWorker, ww.ProductsProcessed);
                }
                else if (item.Value is MaterialHandler mh)
                {
                    employeesTable.Rows.Add(item.Key, mh.Name, mh.DepartmentID, mh.Gender, mh.DateOfBirth, Data.EmployeeTypes.MaterialHandler, mh.MaterialHandled);
                }
                else
                {
                    throw new Exception();
                }
            }

        }//Fills the employees table
        private static void SaveEmployees()
        {
            StreamWriter sw = new StreamWriter(CSVRoot + "/employees_data.csv", false);
            //headers    
            for (int i = 0; i < employeesTable.Columns.Count; i++)
            {
                sw.Write(employeesTable.Columns[i]);
                if (i < employeesTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in employeesTable.Rows)
            {
                for (int i = 0; i < employeesTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < employeesTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }//Saves the employees in the csv file
        private static void ReadEmployees()
        {
            var st = new StreamReader(CSVRoot + "/employees_data.csv");
            var line1 = st.ReadLine();//Skip Line
            while (!st.EndOfStream)
            {
                var line = st.ReadLine();
                var values = line.Split(',');
                if ((Data.EmployeeTypes)Convert.ToInt32(values[5]) == Data.EmployeeTypes.WarehousePacker)
                {
                    var wp = new WarehousePacker(values[1], Convert.ToInt32(values[2]), values[3], Convert.ToDateTime(values[4]), Convert.ToInt32(values[6]));
                    Employees.Add(Convert.ToInt32(values[0]), wp);
                }
                else if ((Data.EmployeeTypes)Convert.ToInt32(values[5]) == Data.EmployeeTypes.WarehouseWorker)
                {
                    var ww = new WarehouseWorker(values[1], Convert.ToInt32(values[2]), values[3], Convert.ToDateTime(values[4]), Convert.ToInt32(values[6]));
                    Employees.Add(Convert.ToInt32(values[0]), ww);
                }
                else if ((Data.EmployeeTypes)Convert.ToInt32(values[5]) == Data.EmployeeTypes.MaterialHandler)
                {
                    var mh = new MaterialHandler(values[1], Convert.ToInt32(values[2]), values[3], Convert.ToDateTime(values[4]), Convert.ToInt32(values[6]));
                    Employees.Add(Convert.ToInt32(values[0]), mh);
                }
                else
                {
                    throw new Exception();
                }
            }

        }//Reads the emplotees in the csv file

        //tblOrders
        static private DataTable ordersTable = new DataTable();
        public static void BuildOrdersTable()
        {
            ordersTable.Columns.Add("productID", typeof(int));
            ordersTable.Columns.Add("orderedProduct", typeof(Product));
            ordersTable.Columns.Add("orderedQuantity", typeof(int));
            ordersTable.Columns.Add("orderDate", typeof(DateTime));
        }//Builds the orders table
        private static void FillOrdersTable()
        {
            BuildOrdersTable();
        foreach (var item in Orders)
            {
                ordersTable.Rows.Add(item.ProductID, item.OrderedProduct, item.OrderedQuantity, item.OrderDate);
            }
        }//Fills the orders table
        private static void SaveOrders()
        {
            StreamWriter sw = new StreamWriter(CSVRoot + "/orders_data.csv", false);
            //headers    
            for (int i = 0; i < ordersTable.Columns.Count; i++)
            {
                sw.Write(ordersTable.Columns[i]);
                if (i < ordersTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in ordersTable.Rows)
            {
                for (int i = 0; i < ordersTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (dr[i] is Product product)
                        {
                            sw.Write("{0};{1};{2};{3}", product.Name, product.DepartmentID, product.SellingPrice, product.BuyingPrice);
                        }
                        else
                        {
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                    }
                    if (i < ordersTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }//Saves the orders in the csv file
        private static void ReadOrders()
        {
             var st = new StreamReader(CSVRoot + "/orders_data.csv");
            var line1 = st.ReadLine();//Skip Line
            while (!st.EndOfStream)
            {
                var line = st.ReadLine();
                var values = line.Split(',');

                var productInfo = values[1].Split(';');
                var product = new Product(productInfo[0], Convert.ToInt32(productInfo[1]), Convert.ToDouble(productInfo[2]), Convert.ToDouble(productInfo[3]));
                Orders.Add(new Order(Convert.ToInt32(values[0]), product, Convert.ToInt32(values[2]), Convert.ToDateTime(values[3])));
            }
        }//Reads the orders in the csv file

        //tblDepartments
        static private DataTable departmentsTable = new DataTable();
        public static void BuildDepartmentsTable()
        {
            departmentsTable.Columns.Add("departmentID", typeof(int));
            departmentsTable.Columns.Add("departmentName", typeof(string));
            departmentsTable.Columns.Add("employeeCapacity", typeof(int));
            departmentsTable.Columns.Add("productCapacity", typeof(int));
            departmentsTable.Columns.Add("productsID", typeof(Dictionary<int, int>));
            departmentsTable.Columns.Add("employeesID", typeof(SortedSet<int>));
        }//Builds the departments table
        private static void FillDepartmentsTable()
        {
            BuildDepartmentsTable();
            foreach (var item in Departments)
            {
                departmentsTable.Rows.Add(item.Key, item.Value.Name, item.Value.EmployeeCapacity
                                      , item.Value.ProductCapacity, item.Value.GetProducts(), item.Value.GetEmployeesID());
            }
        }//Fills the departments table
        private static void SaveDepartments()
        {
            StreamWriter sw = new StreamWriter(CSVRoot + "/departments_data.csv", false);
            //headers    
            for (int i = 0; i < departmentsTable.Columns.Count; i++)
            {
                sw.Write(departmentsTable.Columns[i]);
                if (i < departmentsTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in departmentsTable.Rows)
            {
                for (int i = 0; i < departmentsTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        if (dr[i] is Dictionary<int, int> dic)
                        {
                            foreach (var pair in dic)
                                sw.Write("{0};{1};", pair.Key, pair.Value);
                        }
                        else if (dr[i] is SortedSet<int> set)
                        {
                            foreach (var item in set)
                                sw.Write("{0};", item);
                        }
                        else
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                    }
                    if (i < departmentsTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }//Saves the orders in the csv file
        private static void ReadDepartments()
        {
        var st = new StreamReader(CSVRoot + "/departments_data.csv");
        var line1 = st.ReadLine();//Skip Line
        while (!st.EndOfStream)
        {
            var line = st.ReadLine();
            var values = line.Split(',');
            var dep = new Department(values[1], Convert.ToInt32(values[2]), Convert.ToInt32(values[3]));

            var productIds = values[4].Split(';');
            for (int i = 0; i < productIds.Length - 1; i += 2)
            {
                dep.AddProduct(Convert.ToInt32(productIds[i]), Convert.ToInt32(productIds[i + 1]));
            }

            var employeeIds = values[5].Split(';');
            for (int i = 0; i < employeeIds.Length - 1; i++)
            {
                dep.AddEmployee(Convert.ToInt32(employeeIds[i]));
            }
            Departments.Add(Convert.ToInt32(values[0]), dep);
        }
        }//Reads the orders in the csv file


        public static void LoadAll()
        {
            if (!Directory.Exists(CSVRoot))
            {
                Directory.CreateDirectory(CSVRoot);
            }
            if (File.Exists(CSVRoot + "/products_data.csv") && File.Exists(CSVRoot + "/departments_data.csv") && File.Exists(CSVRoot + "/orders_data.csv") && File.Exists(CSVRoot + "/employees_data.csv"))
            {
                ReadParams();
                ReadProducts();
                ReadDepartments();
                ReadOrders();
                ReadEmployees();
            }
        }//A function to read all the data from the csv
        public static void StoreAll()
        {
            if (!Directory.Exists(CSVRoot))
            {
                Directory.CreateDirectory(CSVRoot);
            }
            FillProductsTable();
            FillDepartmentsTable();
            FillOrdersTable();
            FillEmployeesTable();
            SaveProducts();
            SaveDepartments();
            SaveOrders();
            SaveEmployees();
            SaveParams();
        }//A function to write all the data to the csv
        public static void ResetAll()
        {
            File.Delete(CSVRoot + "/products_data.csv");
            File.Delete(CSVRoot + "/departments_data.csv");
            File.Delete(CSVRoot + "/orders_data.csv");
            File.Delete(CSVRoot + "/employees_data.csv");
            File.Delete(CSVRoot + "/settings_data.csv");
        }//Delete all the data (remove the data) from the csv
    }
}
