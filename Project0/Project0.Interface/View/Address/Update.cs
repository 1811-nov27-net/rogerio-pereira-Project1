using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using AddressesDataAcess = Project0.DataAccess.Addresses;

namespace Project0.Interface.View.Address
{
    class Update
    {
        public static void Show()
        {
            AddressController controller = new AddressController();
            AddressesDataAcess address = new AddressesDataAcess();

            Console.Write("Address ID:\n");
            int id = Int32.Parse(Console.ReadLine());

            address = controller.FindById(id);

            Console.WriteLine($"Address: {address.ToString()}\n");

            Console.WriteLine("Update Address\n");

            Console.Write($"Address Line 1: (leave blank for no change) ({address.Address1})\n");
            string address1 = Console.ReadLine();
            if (!String.IsNullOrEmpty(address1))
            {
                address.Address1 = address1;
            }

            Console.Write($"Address Line 2: (leave blank for no change) ({address.Address2})\n");
            string address2 = Console.ReadLine();
            if (!String.IsNullOrEmpty(address2))
            {
                address.Address2 = address2;
            }

            Console.Write($"City: (leave blank for no change) ({address.City})\n");
            string city = Console.ReadLine();
            if (!String.IsNullOrEmpty(city))
            {
                address.City = city;
            }

            string state = null;
            do
            {
                Console.Write($"State: (2 letters): (leave blank for no change) ({address.State})\n");
                state = Console.ReadLine();
            } while (state.Length != 2 || String.IsNullOrEmpty(state));
            if (!String.IsNullOrEmpty(state))
            {
                address.State = state;
            }

            Console.Write($"ZipCode: (leave blank for no change) ({address.Zipcode})\n");
            string zipcode = Console.ReadLine();
            if (!String.IsNullOrEmpty(zipcode))
            {
                address.Zipcode = Int32.Parse(zipcode);
            }

            controller.Update(address);

            Console.WriteLine("\nAddress updated!\n");
            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
