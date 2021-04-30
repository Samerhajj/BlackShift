using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Classes
{
    public class Storage
    {
        public string Name { get; set; }
        private List<Department> departments;

        public Storage(string name)
        {
            this.Name = name;
            departments = new List<Department>();
        }
        public void AddDepartment(Department department)
        {
            departments.Add(department);
        }
        public void RemoveDepartment(Department department)
        {
            departments.Remove(department);
        }
    }
}
