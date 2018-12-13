using Project0.DataAccess;
using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using CustomerDataAccess = Project0.DataAccess.Customers;
using AddressDataAccess = Project0.DataAccess.Addresses;

namespace Project0.Interface.View.Customers
{
    class CustomerForm
    {
        public static void ShowForm()
        {
            CustomerController controller = new CustomerController();
            CustomerDataAccess customer = new CustomerDataAccess();
            AddressDataAccess address = new AddressDataAccess();

            Console.Write("Customer First Name:\n");
            customer.FirstName = Console.ReadLine();

            Console.Write("Customer Last Name:\n");
            customer.LastName = Console.ReadLine();

            Console.Write("Address Line 1:\n");
            address.Address1 = Console.ReadLine();

            Console.Write("Address Line 2:\n");
            address.Address2 = Console.ReadLine();

            Console.Write("City:\n");
            address.City = Console.ReadLine();

            do
            {
                Console.Write("State: (2 letters)\n");
                address.State = Console.ReadLine();
            } while (address.State.Length != 2);

            Console.Write("ZipCode:\n");
            address.Zipcode = Int32.Parse(Console.ReadLine());

            customer.Addresses.Add(address);
            controller.Save(customer);

            Console.WriteLine("\nCustomer saved!\n");
            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
