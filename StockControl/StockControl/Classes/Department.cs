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
        private Dictionary<int,Employee> employees;
        private Dictionary<int,Product> products;

        //Constructors
        public Department() { }
        public Department(int departmentID, string departmentName)
        {
            ID = departmentID;
            this.Name = departmentName;
            products = new Dictionary<int, Product>();
            employees = new Dictionary<int, Employee>();
        }

        //Methods
        public void AddProduct(Product item)
        {
            products.Add(item.ID, item);
        }
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee.ID, employee);
        }
        public void RemoveProduct(int productID)
        {
            products.Remove(productID);
        }
        public void RemoveEmployee(int employeeID)
        {
            employees.Remove(employeeID);
        }
        public int EmployeeCapacity() 
        {
            return employees.Count();
        }
        public int ProductCapacity()
        {
            return products.Count();
        }
        public override string ToString()
        {
            return "Department Name : " + Name + "\n";
        }
    }
}
