using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Classes
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }

        public Employee(string name, int id, double salary)
        {
            this.Name = name;
            this.Id = id;
            this.Salary = salary;
        }
        public override string ToString()
        {
            return "Name : " + Name + " Id : " + Id + " Salary : " + Salary;
        }
    }
}
