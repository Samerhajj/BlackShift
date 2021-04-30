using System;
using System.Collections.Generic;
namespace Box
{
    // Item Class
    // name , number , price 
    public class Item
    {
        protected string name;
        protected int number;
        protected double price;

        public Item(string name, int number, double price)
        {
            this.name = name;
            this.number = number;
            this.price = price;
        }//contractor Item
        public override string ToString()
        {
            return "name of the item " + name + " number of the item " + number + " price of the item " + price + "$\n";
        }//ToString Item
    }//End of Class Item


       public void print_all_items()//print all the items
        {
            foreach (var item in items)
            {
                Console.Write(item.ToString());
            }
        }
        public void print_all_employees()
        {
            foreach (var employee in employees)
            {
                Console.WriteLine("department number :" + departmentNumber + " " + employee.ToString());
            }
        }//print all the employees in the department
    }//End of Class Department
