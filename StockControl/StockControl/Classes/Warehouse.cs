using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class Warehouse
    {
        //Properties
        public string Name { get; set; }
        private Dictionary<int, Department> departments;

        //Constructors
        public Warehouse(){}
        public Warehouse(string name)
        {
            this.Name = name;
            departments = new Dictionary<int, Department>();
        }

        //Methods
        public void AddDepartment(Department department)
        {
            departments.Add(department.ID, department);
        }
        public void RemoveDepartment(Department department)
        {
            departments.Remove(department.ID);
        }
        public Department this[int id]
        {
            get { return departments[id]; }
            set { departments[id] = value; }
        }
    }
}
