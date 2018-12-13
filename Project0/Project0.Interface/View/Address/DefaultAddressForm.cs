using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using AddressesDataAccess = Project0.DataAccess.Addresses;
using CustomerDataAccess = Project0.DataAccess.Customers;

namespace Project0.Interface.View.Address
{
    public class DefaultAddressForm
    {
        public static void ShowForm()
        {
            Console.WriteLine("Fetching Data, please wait...");

            //Customer
            CustomerController customerController = new CustomerController();
            List<CustomerDataAccess> customersList = customerController.getAll();
            CustomerDataAccess customer = null;
            int customerId = 0;

            do
            {
                ClearHelper.Clear();
                Console.WriteLine("Set Default Address\n");

                //Show all customers
                foreach (CustomerDataAccess c in customersList)
                {
                    Console.WriteLine(c.ToString());
                }

                //Select Customer
                Console.WriteLine("\nSelect Customer:\n");
                customerId = Int32.Parse(Console.ReadLine());
                customer = customerController.FindById(customerId);

                if (customer == null)
                {
                    Console.WriteLine("\nWrong Customer Id");
                    Console.ReadKey();
                }

                ClearHelper.Clear();
            } while (customer == null);

            AddressController addressController = new AddressController();
            List<AddressesDataAccess> addresses = addressController.FindByCustomerId(customer.Id);
            AddressesDataAccess address = null;
            int addressId = 0;

            do
            {
                ClearHelper.Clear();
                Console.WriteLine("Set Default Address\n");

                //Show all customers
                foreach (AddressesDataAccess a in addresses)
                {
                    Console.WriteLine(a.ToString());
                }

                //Select Customer
                Console.WriteLine("\nSelect Default Address:\n");
                addressId = Int32.Parse(Console.ReadLine());
                address = addressController.FindById(addressId);

                if (address == null)
                {
                    Console.WriteLine("\nWrong Address Id");
                    Console.ReadKey();
                }
            } while (address == null);

            addressController.SetDefaultAddress(address.Id, customer.Id);

            Console.WriteLine("Default Address saved!\n");
        }
    }
}
