using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE07201_BD00535_Duong_ASM1
{
    class Customer
    {
        public string CustomerName { get; }
        public int LastMonthReading { get; }
        public int ThisMonthReading { get; }
        public int CustomerType { get; }
        public int NumberOfPeople { get; }
        public int Consumption => ThisMonthReading - LastMonthReading;

        public Customer(string customerName, int lastMonthReading, int thisMonthReading, int customerType, int numberOfPeople = 1)
        {
            CustomerName = customerName;
            LastMonthReading = lastMonthReading;
            ThisMonthReading = thisMonthReading;
            CustomerType = customerType;
            NumberOfPeople = numberOfPeople;
        }

        //Calculate the water bill based on customer type
        public double CalculateBill()
        {
            double totalWaterBill = 0;

            switch (CustomerType)
            {
                //Household
                case 1:
                    double avgConsumption = (double)Consumption / NumberOfPeople;
                    if (avgConsumption <= 10)
                    {
                        totalWaterBill = Consumption * 5973 * 1.1;
                    }
                    else if (avgConsumption <= 20)
                    {
                        totalWaterBill = (10 * 5973 * 1.1 * NumberOfPeople) + ((Consumption - (10 * NumberOfPeople)) * 7052 * 1.1);
                    }
                    else if (avgConsumption <= 30)
                    {
                        totalWaterBill = (10 * 5973 * 1.1 * NumberOfPeople) + (10 * 7052 * 1.1 * NumberOfPeople)
                            + ((Consumption - (20 * NumberOfPeople)) * 8699 * 1.1);
                    }
                    else
                    {
                        totalWaterBill = (10 * 5973 * 1.1 * NumberOfPeople) + (10 * 7052 * 1.1 * NumberOfPeople) + (10 * 8699 * 1.1 *
                            NumberOfPeople) + ((Consumption - (30 * NumberOfPeople)) * 9955 * 1.1);
                    }
                    break;
                //Administrative agency, public services
                case 2:
                    totalWaterBill = Consumption * 9955 * 1.1;
                    break;
                //Production units
                case 3:
                    totalWaterBill = Consumption * 11615 * 1.1;
                    break;
                //Business services
                case 4:
                    totalWaterBill = Consumption * 22068 * 1.1;
                    break;
                default:
                    throw new InvalidOperationException("Invalid customer type.");
            }
            return totalWaterBill;
        }
    }
}
