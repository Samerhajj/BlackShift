using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class MaterialHandler : Employee
    {
        public int MaterialHandled { get; set; }
        public override double Income
        {
            get { return SettingsParams.MaterialHandlerWage * (Raise + 1); }
        }

        //Constructor
        public MaterialHandler() : base(){ }
        public MaterialHandler(string name, int departmentId, string gender, DateTime dateOfBirth, int materialHandled = 0)
            : base(name, departmentId, dateOfBirth, gender, Data.EmployeeTypes.MaterialHandler)
        {
            MaterialHandled = materialHandled;
        }
    }
}
