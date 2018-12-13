using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using AddressesDataAccess = Project0.DataAccess.Addresses;
using CustomerDataAccess = Project0.DataAccess.Customers;
using OrderDataAccess = Project0.DataAccess.Orders;

namespace Project0.Interface.View.Orders
{
    public class ShowAllAddress
    {
        public static void Show()
        {
            Console.WriteLine("Fetching Data, please wait...");

            AddressController addressController = new AddressController();
            List<AddressesDataAccess> addressList = addressController.getAll();
            AddressesDataAccess address = null;

            int addressId = 0;

            do
            {
                ClearHelper.Clear();
                Console.WriteLine("Show all Orders by Customer\n");

                //Show all customers
                foreach (AddressesDataAccess a in addressList)
                {
                    Console.WriteLine(a.ToString());
                }

                //Select Customer
                Console.WriteLine("\nSelect Address:\n");
                addressId = Int32.Parse(Console.ReadLine());
                address = addressController.FindById(addressId);

                if (address == null)
                {
                    Console.WriteLine("\nWrong Address Id");
                    Console.ReadKey();
                }

                ClearHelper.Clear();
            } while (address == null);

            OrderController controller = new OrderController();
            List<OrderDataAccess> orders = controller.FindByAddress(address.Id);
            
            orders = SortForm.Sort(orders);

            ClearHelper.Clear();
            Console.WriteLine($"{address.Address1}'s Orders:\n");

            foreach (OrderDataAccess order in orders)
            {
                Console.WriteLine(order.ToString());
            }
        }
    }
}
