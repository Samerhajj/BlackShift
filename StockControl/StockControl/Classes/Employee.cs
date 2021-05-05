using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }

        public Employee() { }
        public Employee(string name, int id, double salary)
        {
            this.Name = name;
            this.ID = id;
            this.Salary = salary;
        }
        public override string ToString()
        {
            return "Name : " + Name + " Id : " + ID + " Salary : " + Salary;
        }
    }

}
