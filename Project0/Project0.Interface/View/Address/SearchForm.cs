using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using AddressesDataAcess = Project0.DataAccess.Addresses;

namespace Project0.Interface.View.Address
{
    class SearchForm
    {
        private static AddressController controller = new AddressController();

        public static void Search()
        {
            string menuOption = new string("");
            string menu = "Address Search\n\n" +
                            "1 - Search by ID\n" +
                            "2 - Search by Street 1\n" +
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
            Console.WriteLine("Address Search\n");
            Console.WriteLine("Search by ID:");
            int id = Int32.Parse(Console.ReadLine());
            
            Console.WriteLine("\nFetching Data, please wait...");
            AddressesDataAcess Address = controller.FindById(id);

            Console.WriteLine();

            if (Address == null)
                Console.WriteLine($"There is no Address with this Id");
            else
                Console.WriteLine(Address.ToString());

            Console.ReadKey();
            ClearHelper.Clear();
        }

        private static void SearchByName()
        {
            ClearHelper.Clear();
            Console.WriteLine("Addresses Search\n");
            Console.WriteLine("Search by Name:");
            string name = Console.ReadLine();

            Console.WriteLine("\nFetching Data, please wait...");
            List<AddressesDataAcess> Addresses = controller.FindByName(name);

            Console.WriteLine();

            if (Addresses.Count == 0)
                Console.WriteLine($"There is no Address with this Street");
            else
            {
                foreach(AddressesDataAcess Address in Addresses)
                    Console.WriteLine(Address.ToString());
            }

            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
