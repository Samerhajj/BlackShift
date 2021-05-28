using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public struct Order
    {
        public DateTime OrderDate{ get; }
        public int OrderedQuantity { get; }
        public double CostPerUnit { get; }
        public int ProductID { get; }
        public string ProductName { get; }
        public double TotalCostWithTax
        {
            get { return CostPerUnit * OrderedQuantity; }
        }

        //Constructer
        public Order(int orderedQuantity, double costPerUnit, int productID, string productName)
        {
            OrderedQuantity = orderedQuantity;
            CostPerUnit = costPerUnit;
            OrderDate = DateTime.Now;
            ProductID = productID;
            ProductName = productName;
        }
    }
}
