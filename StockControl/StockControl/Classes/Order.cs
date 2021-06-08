using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public struct Order
    {
        public int ProductID { get; }
        public Product OrderedProduct { get; }
        public DateTime OrderDate{ get; }
        public int OrderedQuantity { get; }
        public double TotalCostWithTax
        {
            get { return OrderedProduct.BuyingPriceWithTax * OrderedQuantity; }
        }

        //Constructer
        public Order(int productID, Product orderedProduct, int orderedQuantity)
        {
            if (orderedQuantity > 0)
            {
                OrderedQuantity = orderedQuantity;
                OrderDate = DateTime.Now;
                ProductID = productID;
                OrderedProduct = orderedProduct;
            }
            else
            {
                throw new FormatException("Quantity must be at least 1.");
            }
        }
    }
}
