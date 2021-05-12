using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
   public class Product
    {
        private const double TAX = 0.17;
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double PriceTax { get; set; }

        public double PriceWithTax(double price)
        {
            this.PriceTax = price * TAX + price;
            return PriceTax;
        }

    }
    

}
