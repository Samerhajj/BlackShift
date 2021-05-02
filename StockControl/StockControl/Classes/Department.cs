using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Classes
{
    public class Department
    {
        protected string departmentName;
        protected List<Employee> employees;
        protected List<Product> products;
        protected int departmentNumber;

        public Department(int departmentNumber, string departmentName)
        {
            this.departmentNumber = departmentNumber;
            this.departmentName = departmentName;
            products = new List<Product>();
            employees = new List<Employee>();
        }
        public void AddItem(Product item)
        {
            products.Add(item);
        }
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }
        public void RemoveItem(Product item)
        {
            products.Remove(item);
        }
        public void RemoveEmployee(int id)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if ((employees[i]).ID == id) this.employees.Remove(employees[i]);
            }
        }
        public void SetEmployee() { }
        public override string ToString()
        {
            return "Department Name : " + departmentName + "\n";
        }
    }
}
