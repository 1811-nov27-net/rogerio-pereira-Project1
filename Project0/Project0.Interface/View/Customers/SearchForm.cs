using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using CustomersDataAcess = Project0.DataAccess.Customers;

namespace Project0.Interface.View.Customers
{
    class SearchForm
    {
        private static CustomerController controller = new CustomerController();

        public static void Search()
        {
            string menuOption = new string("");
            string menu = "Customers Search\n\n" +
                            "1 - Search by ID\n" +
                            "2 - Search by Name\n" +
                            "b - Back\n" +
                            "Option: ";
            do
            {
                Console.Write(menu);
                menuOption = Console.ReadLine();

                switch (menuOption)
                {
                    case "1":
                        SearchById();
                        break;
                    case "2":
                        SearchByName();
                        break;
                    case "b":
                        ClearHelper.Clear();
                        break;
                    default:
                        Console.WriteLine("Wrong option!");
                        Console.ReadKey();
                        ClearHelper.Clear();
                        break;
                }
            } while (menuOption != "b");
        }

        private static void SearchById()
        {
            ClearHelper.Clear();
            Console.WriteLine("Customers Search\n");
            Console.WriteLine("Search by ID:");
            int id = Int32.Parse(Console.ReadLine());
            
            Console.WriteLine("\nFetching Data, please wait...");
            CustomersDataAcess customer = controller.FindById(id, true);

            Console.WriteLine();

            if (customer == null)
                Console.WriteLine($"There is no Customer with this Id");
            else
                Console.WriteLine(customer.ToString());

            Console.ReadKey();
            ClearHelper.Clear();
        }

        private static void SearchByName()
        {
            ClearHelper.Clear();
            Console.WriteLine("Customers Search\n");
            Console.WriteLine("Search by Name:");
            string name = Console.ReadLine();

            Console.WriteLine("\nFetching Data, please wait...");
            List<CustomersDataAcess> customers = controller.FindByName(name);

            Console.WriteLine();

            if (customers.Count == 0)
                Console.WriteLine($"There is no Customer with this Name");
            else
            {
                foreach(CustomersDataAcess customer in customers)
                    Console.WriteLine(customer.ToString());
            }

            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
