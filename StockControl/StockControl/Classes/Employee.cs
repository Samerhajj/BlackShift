using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public abstract class Employee
    {
        //Properties
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public string Gender { get; set; }
        public double Raise { get; set; } //Contains the extra raise in % (Percentage e.g. 13%)
        public DateTime DateOfBirth { get; set; }
        public abstract double Income { get; }

        //Constructors
        public Employee() { }

        public Employee(string name, int departmentId, DateTime dateOfBirth, string gender, double raise = 0)
        {
            if (!String.IsNullOrEmpty(name))
            {
                if (!String.IsNullOrEmpty(gender))
                {
                    if (DateTime.Now.Date > dateOfBirth.AddYears(18))
                    {
                        Name = name;
                        Raise = raise;
                        DepartmentID = departmentId;
                        DateOfBirth = dateOfBirth;
                        Gender = gender;
                    }
                    else
                    {
                        throw new InvalidOperationException("Employee must be over 18.");
                    }
                }
                else
                {
                    throw new ArgumentNullException("", "Gender was not chosen.");
                }
            }
            else
            {
                throw new ArgumentNullException("", "Employee name was not entered.");
            }
        }

        //Methods
    }
}
