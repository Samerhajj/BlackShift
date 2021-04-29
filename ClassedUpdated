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

    // Employee Class
    // id , name , salary
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public double salary { get; set; }
        public bool hasADrivingForkCarLicense { get; set; }

        public Employee(string name, int id, double salary,bool hasADrivingForkCarLicense)
        {
            this.name = name;
            this.id = id;
            this.salary = salary;
            this.hasADrivingForkCarLicense = hasADrivingForkCarLicense;
        }//contractor Employee
        public override string ToString()
        {
            return "name : " + name + " id : " + id + " salary : " + salary;
        }//ToString Employee
    }//End of Class Employee

    // Part Class
    // departmentName , list(employee) , list(item) , departmentNumber
    public class Department
    {
        protected string departmentName;
        protected List<Employee> employees;
        protected List<Item> items;
        protected int departmentNumber;

        public Department(int departmentNumber, string departmentName)
        {
            this.departmentNumber = departmentNumber;
            this.departmentName = departmentName;
            items = new List<Item>();
            employees = new List<Employee>();
        }//contractor department
        public void AddItem(Item item)//add item to the department
        {
            items.Add(item);
        }
        public void AddEmployee(Employee employee)//add employee to the department
        {
            employees.Add(employee);
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
        public void RemoveEmployee(int id)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if ((employees[i]).id == id) this.employees.Remove(employees[i]);
            }
        }//delete employee
        public void SetEmployee() { }
        public override string ToString()
        {
            return "name of the department : " + departmentName + "\n";
        }//ToString for the department
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

    // Store Class
    // name , parts
    public class Storage
    {
        public string Name { get; set; }
        private List<Department> departments;
        private List<ForkCar> forkCars;
        public Storage(string name)
        {
            this.Name = name;
            departments = new List<Department>();
            forkCars = new List<ForkCar>();
        }//contractor Store
        public void AddDepartment(Department department)//add a department to the store
        {
            departments.Add(department);
        }
        public void RemoveDepartment(Department department)
        {
            departments.Remove(department);
        }
        public void print_all_parts()
        {
            foreach (var department in departments)
            {
                Console.Write(department);
            }
        }//print all departments details
    }
    public class Car
    {
        public int CarNumber { get; set; }
        public string CarName { get; set; }
        public string Fuel { get; set; }
        

        public Car(int CarNumber, string CarName,string Fuel)
        {
            this.CarName = CarName;
            this.CarNumber = CarNumber;
            this.Fuel = Fuel;
        }
    }

    public class ForkCar : Car
    {
        public double maximum_load_weight { get; set; }
        
        public ForkCar(int CarNumber,string CarName,string Fuel,double maximum_load_weight)
            :base(CarNumber,CarName,Fuel)
        {
            this.maximum_load_weight = maximum_load_weight;
        }
    }
    public class TruckCar : Car
    {
        string place_to_be_reached;
        public TruckCar(int CarNumber, string CarName,string Fuel,string place_to_be_reached)
        : base(CarNumber, CarName,Fuel)
        {
            this.place_to_be_reached = place_to_be_reached;
        }
    }
    
    //public class Box
    //{
    
    //}        

    public class main
    {

        static void Main(string[] args)
        {

            //Store
            Storage store = new Storage("BlackShift");

            //Parts
            Department part_1 = new Department(1, "Part_1");
            Department part_2 = new Department(2, "Part_2");
            Department part_3 = new Department(3, "Part_3");
            Department part_4 = new Department(4, "Part_4");

            //define Employees
            Employee emp1 = new Employee("mohamed", 477928892, 7000,true);
            Employee emp2 = new Employee("samer", 477912594, 8000,false);
            Employee emp3 = new Employee("jeris", 467828252, 15000,false);
            Employee emp4 = new Employee("majd", 460158931, 17000,true);

            // define Items
            Item phone_1 = new Item("Iphone", 100, 1000);
            Item phone_2 = new Item("Iphone", 101, 2000);
            Item phone_3 = new Item("Iphone", 102, 1500);
            Item phone_4 = new Item("Iphone", 103, 1200);

            //add part to store
            store.AddDepartment(part_1);  store.AddDepartment(part_2);  store.AddDepartment(part_3);  store.AddDepartment(part_4);

            //add employees to a part
            part_1.AddEmployee(emp1);part_1.AddEmployee(emp2);part_2.AddEmployee(emp3);part_3.AddEmployee(emp4);

            //add items to a part
            part_1.AddItem(phone_1);part_1.AddItem(phone_2);part_2.AddItem(phone_3);part_3.AddItem(phone_4);

            //print
            part_1.print_all_items();

            store.print_all_parts();
            //print_all_employees_d((store.employees).Count);
            Console.WriteLine();

            part_1.print_all_employees();part_2.print_all_employees();part_3.print_all_employees();part_4.print_all_employees();
        }
    }
}



//public static void print_all_employees_d(int numOfEmployees)
//{
//    for (int i = 0; i < numOfEmployees; i++)
//    {
//        Console.Write("  o" + "\t");
//    }
//    Console.WriteLine();
//    for (int i = 0; i < numOfEmployees; i++)
//    {
//        Console.Write(" /|\\" + "\t");
//    }
//    Console.WriteLine();
//    for (int i = 0; i < numOfEmployees; i++)
//    {
//        Console.Write(" /'\\" + "\t");
//        if (i == 10) Console.WriteLine();
//    }

//}
