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
        public int DepartmentID { get; set; }
        public string Gender { get; set; }
        public double Salary { get; set; }
        public DateTime DateOfBirth { get; set; }

        //Constructors
        public Employee(string name, DateTime dateOfBirth, string gender, int departmentId)
        {
            if(name != string.Empty)
            {
                if (gender != null)
                {
                    Name = name;
                    DateOfBirth = dateOfBirth;
                    Gender = gender;
                    DepartmentID = departmentId;
                }
                else
                {
                    throw new ArgumentNullException("", "Gender was not chosen.");
                }
            }
            else
            {
                throw new ArgumentNullException("","Employee name was not entered.");
            }
        }

        //Methods
    }
}
