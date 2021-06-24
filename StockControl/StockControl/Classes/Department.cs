using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class Department
    {
        //Properties / Fields
        public string Name { get; set; }
        public int EmployeeCapacity { get; set; }
        public int ProductCapacity { get; set; }
        public int EmployeeCount
        {
            get { return employeesID.Count; }
        }
        public int ProductCount { get; set; }

        private Dictionary<int,int> products = new Dictionary<int,int>();//All the products ID And Quantity in this department
        private SortedSet<int> employeesID = new SortedSet<int>();//All the employees ID in this department

        //Constructors
        public Department() { }
        public Department(string departmentName, int employeeCapacity, int productCapacity)
        {
            if (!String.IsNullOrEmpty(departmentName))
            {
                if (employeeCapacity > 0 && productCapacity > 0)
                {
                    Name = departmentName;
                    EmployeeCapacity = employeeCapacity;
                    ProductCapacity = productCapacity;
                }
                else throw new FormatException("Capacity must be at least 1.");
            }
            else
            {
                throw new ArgumentNullException("", "The department name was not entered.");
            }
        }

        //Methods
        public void AddProduct(int productId, int quantity = 0)
        {
            if (ProductCount + quantity <= ProductCapacity)
            {
                products.Add(productId, quantity);
                ProductCount += quantity;
            }
            else
            {
                throw new OverCapacityException($"The Department Can't Have More Than {ProductCapacity} Products.");
            }
        }//Checking the if the department is full and adds the productId if not
        public void AddEmployee(int employeeId)
        {
            if (employeesID.Count + 1 <= EmployeeCapacity)
            {
                employeesID.Add(employeeId);
            }
            else
            {
                throw new OverCapacityException($"The Department Can't Have More Than {EmployeeCapacity} Employees.");
            }
        }//Checking the if the department is full and adds the employeeId if not
        public void RemoveProduct(int productId)
        {
            ProductCount -= products[productId];
            products.Remove(productId);
        }//Removes product from the department
        public void RemoveEmployee(int employeeId)
        {
            employeesID.Remove(employeeId);
        }//Removes employee form the department
        public void AddQuantity(int productId, int amount)
        {
            if (ProductCount + amount <= ProductCapacity)
            {
                products[productId] += amount;
                ProductCount += amount;
            }
            else
            {
                throw new OverCapacityException($"The Department Can't Have More Than {ProductCapacity} Products.");
            }
        }//Adds quantity to a specific product
        public int GetQuantity(int productId)
        {
            return products[productId];
        }//Returns the quantity for a  specific product
        public SortedSet<int> GetEmployeesID()
        {
            return employeesID;
        }//Returns the SortedSet of the employees
        public Dictionary<int, int> GetProducts()
        {
            return products;
        }//Returns the Dictionary of the products
    }
}
