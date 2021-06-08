using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class WarehousePacker : Employee
    {
        public int ProductsPacked { get; set; }
        public override double Income
        {
            get { return SettingsParams.WarehousePackerWage * (Raise + 1); }
        }

        //Constructor
        public WarehousePacker() : base() { }
        public WarehousePacker(string name, int departmentId, string gender, DateTime dateOfBirth, int productsPacked = 0)
            : base(name, departmentId, dateOfBirth, gender)
        {
            ProductsPacked = productsPacked;
        }

        //Methods

    }
}
