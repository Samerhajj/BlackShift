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

        //Constructor
        public Product(string productName, int departmentID, double sellingPrice, double buyingPrice)
        {
            if (!String.IsNullOrEmpty(productName))
            {
                if (sellingPrice != 0 && buyingPrice != 0)
                {
                    if (sellingPrice - buyingPrice <= 10)
                    {
                        Name = productName;
                        DepartmentID = departmentID;
                        SellingPrice = sellingPrice;
                        BuyingPrice = buyingPrice;
                    }
                    else
                    {
                        throw new ArgumentException("Price gouging is illegal.");
                    }
                }
                else
                {
                    throw new ArgumentException("Price can't be zero");
                }
            }
            else
            {
                throw new ArgumentNullException("", "The name was not entered.");
            }
        }
    }
}
