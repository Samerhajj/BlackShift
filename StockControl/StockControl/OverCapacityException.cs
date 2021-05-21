using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    //This Exception Is Thrown When Trying To Add A Product / Empolyee To A Full Deparment.
    class OverCapacityException : ApplicationException
    {
        public OverCapacityException(string message) : base(message) { }
    }
}
