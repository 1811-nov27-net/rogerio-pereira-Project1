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
    public class ShowAllCustomer
    {
        public static void Show()
        {
            Console.WriteLine("Fetching Data, please wait...");

            CustomerController customerController = new CustomerController();
            List<CustomerDataAccess> customersList = customerController.getAll();
            CustomerDataAccess customer = null;
            int customerId = 0;

            do
            {
                ClearHelper.Clear();
                Console.WriteLine("Show all Orders by Customer\n");

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

            OrderController controller = new OrderController();
            List<OrderDataAccess> orders = controller.FindByCustomer(customer.Id);

            orders = SortForm.Sort(orders);

            ClearHelper.Clear();
            Console.WriteLine($"{customer.FirstName} {customer.LastName}'s Orders:\n");

            foreach (OrderDataAccess order in orders)
            {
                Console.WriteLine(order.ToString());
            }
        }
    }
}
