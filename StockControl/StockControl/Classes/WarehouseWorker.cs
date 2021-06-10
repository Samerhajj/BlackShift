using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class WarehouseWorker : Employee
    {
        public int ProductsProcessed { get; set; }
        public override double Income
        {
            get { return SettingsParams.WarehouseWorkerWage * (Raise + 1); }
        }

        //Constructor
        public WarehouseWorker() : base(){ }
        public WarehouseWorker(string name, int departmentId, string gender, DateTime dateOfBirth, int productsProcessed = 0)
            : base(name, departmentId, dateOfBirth, gender, Data.EmployeeTypes.WarehouseWorker)
        {
            ProductsProcessed = productsProcessed;
        }
    }
}
