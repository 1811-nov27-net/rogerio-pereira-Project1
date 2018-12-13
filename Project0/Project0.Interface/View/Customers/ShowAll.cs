using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using CustomerDataAccess = Project0.DataAccess.Customers;

namespace Project0.Interface.View.Customers
{
    class ShowAll
    {
        public static void Show()
        {
            Console.WriteLine("Fetching Data, please wait...");

            CustomerController controller = new CustomerController();
            List<CustomerDataAccess> list = controller.getAll(false);

            ClearHelper.Clear();
            Console.WriteLine("Customers:\n");

            foreach(CustomerDataAccess customer in list)
            {
                Console.WriteLine(customer.ToString());
            }
        }
    }
}
