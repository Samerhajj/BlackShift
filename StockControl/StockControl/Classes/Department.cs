using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class Department
    {
        //Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int EmployeeCapacity { get; set; }
        public int ProductCapacity { get; set; }
        private Dictionary<int,Employee> employees;
        private Dictionary<int,Product> products;

        //Constructors
        public Department() { }
        public Department(int departmentID, string departmentName)
        {
            ID = departmentID;
            Name = departmentName;
            products = new Dictionary<int, Product>();
            employees = new Dictionary<int, Employee>();
        }

        //Methods
        public void AddProduct(Product item)
        {
            if (products.Count <= ProductCapacity)
            {
                //products.Add(item.ID, item);
            }
            else
            {
                throw new OverCapacityException($"The Department Can't Have More Than {ProductCapacity} Products.");
            }
        }
        public void AddEmployee(Employee employee)
        {
            if (employees.Count <= EmployeeCapacity)
            {
                //employees.Add(employee.ID, employee);
            }
            else
            {
                throw new OverCapacityException($"The Department Can't Have More Than {EmployeeCapacity} Employees.");
            }
        }
        public void RemoveProduct(int productID)
        {
            products.Remove(productID);
        }
        public void RemoveEmployee(int employeeID)
        {
            employees.Remove(employeeID);
        }
        public override string ToString()
        {
            return "Department Name : " + Name + "\n";
        }
    }
}
