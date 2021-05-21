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
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double PriceTax { get; set; }

        public double PriceWithTax(double price)
        {
            this.PriceTax = price * TAX + price;
            return PriceTax;
        }

        public override string ToString()
        {
            return "Name : " + Name + "Quantity : " + Quantity + "Price : " + Price;
        }

    }
}
