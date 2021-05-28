using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class Employee
    {
        //Properties
        public string Name { get; set; }
        public string Department { get; set; }
        public string Gender { get; set; }
        public double Salary { get; set; }
        public DateTime DateOfBirth { get; set; }

        //Constructors
        public Employee() { }
        public Employee(string name, double salary)
        {
            this.Name = name;
            this.Salary = salary;
        }
    }
}
