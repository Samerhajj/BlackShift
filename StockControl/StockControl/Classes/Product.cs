using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
   public class Product
    {
        public string Name { get; set; }
        public int DepartmentID { get; set; }
        public int Quantity { get; set; }
        public double SellingPrice { get; set; }
        public double BuyingPrice { get; set; }

        public double SellingPriceWithTax
        {
            get { return SellingPrice * SettingsParams.Tax + SellingPrice; }
        }
        public double BuyingPriceWithTax
        {
            get { return BuyingPrice * SettingsParams.Tax + BuyingPrice; }
        }
    }
}
