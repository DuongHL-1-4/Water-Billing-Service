using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE07201_BD00535_Duong_ASM1
{
    class Program
    {
        static List<Customer> customers = new List<Customer>();
        static void Main(string[] args)
        {
            while (true)
            {
                //Menu
                Console.WriteLine("Water Billing Service");
                Console.WriteLine("1 - Add Customer");
                Console.WriteLine("2 - Customer List");
                Console.WriteLine("3 - Customer Bill");
                Console.WriteLine("4 - Exit");
                Console.Write("Enter your choise 1 - 4: ");

                int choise;
                if (!int.TryParse(Console.ReadLine(), out choise))
                {
                    Console.WriteLine("Invalid input. Please enter a number!");
                    continue;
                }

                switch (choise)
                {
                    case 1:
                        AddCustomer();
                        break;
                    case 2:
                        ViewCustomerList();
                        break;
                    case 3:
                        CalculateWaterBill();
                        break;
                    case 4:
                        Console.WriteLine("Thank you for using our water billing service!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                        break;
                }
            }
        }
        static void AddCustomer()
        {
            Console.WriteLine("Add Customer: ");
            Console.Write("Enter customer name: ");
            string customerName = Console.ReadLine();

            int lastMonthReading, thisMonthReading, customerType, numberOfPeople = 1;
            Console.Write("Enter last month's water meter reading: ");
            while (!int.TryParse(Console.ReadLine(), out lastMonthReading) || lastMonthReading < 0)
            {
                if (lastMonthReading < 0)
                {
                    Console.WriteLine("Values must be positive (+) numbers");
                    Console.Write("Enter last month's water meter reading: ");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.Write("Enter last month's water meter reading: ");
                }
            }
            do
            {
                Console.Write("Enter this month's water meter reading: ");
                while (!int.TryParse(Console.ReadLine(), out thisMonthReading))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.Write("Enter this month's water meter reading: ");
                }
                if (thisMonthReading <= lastMonthReading)
                {
                    Console.WriteLine("Error: This month's reading must be greater than last month's reading.");
                }
            }
            while (thisMonthReading <= lastMonthReading);
            Console.WriteLine("Select customer type:");
            Console.WriteLine("1. Household");
            Console.WriteLine("2. Administrative agency, public services");
            Console.WriteLine("3. Production units");
            Console.WriteLine("4. Business services");
            Console.Write("Enter the customer type (choose numbers 1-4): ");
            while (!int.TryParse(Console.ReadLine(), out customerType) || customerType < 1 || customerType > 4)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                Console.Write("Enter the customer type (choose numbers 1-4): ");
            }

            //Household
            if (customerType == 1)
            {
                Console.Write("Enter number of people in the household: ");
                while (!int.TryParse(Console.ReadLine(), out numberOfPeople) || numberOfPeople < 1)
                {
                    Console.WriteLine("Invalid input. Please enter number of people!");//
                    Console.Write("Enter number of people in the household: ");
                }
            }

            // Add Customer
            Customer newCustomer = new Customer(customerName, lastMonthReading, thisMonthReading, customerType, numberOfPeople);
            customers.Add(newCustomer);

            Console.WriteLine("Customer added successfully!");
        }
        static void ViewCustomerList()
        {
            Console.WriteLine("\nCustomer List:");
            foreach (var customer in customers)
            {
                Console.WriteLine("- {0}", customer.CustomerName);
            }
        }
        static void CalculateWaterBill()
        {
            Console.WriteLine("\nCalculating water bill for a customer:");
            Console.Write("Enter customer name: ");
            string customerName = Console.ReadLine();

            Customer customer = customers.Find(c => c.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase));
            if (customer == null)
            {
                Console.WriteLine($"Customer '{customerName}' not found.");
                return;
            }

            //Display detailed information
            double totalWaterBill = customer.CalculateBill();
            Console.WriteLine("\nCustomer Information:");
            Console.WriteLine($"Customer Name: {customer.CustomerName}");
            Console.WriteLine($"Last month's water meter reading: {customer.LastMonthReading}");
            Console.WriteLine($"This month's water meter reading: {customer.ThisMonthReading}");
            Console.WriteLine($"Amount of consumption: {customer.Consumption} m3");
            Console.WriteLine($"Total water bill: {totalWaterBill.ToString("C2")} VND");
        }
    }
}
