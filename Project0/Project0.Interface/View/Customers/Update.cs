using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using CustomersDataAcess = Project0.DataAccess.Customers;

namespace Project0.Interface.View.Customers
{
    class Update
    {
        public static void Show()
        {
            CustomerController controller = new CustomerController();
            CustomersDataAcess customer = new CustomersDataAcess();

            Console.Write("Customer ID:\n");
            int id = Int32.Parse(Console.ReadLine());
            
            customer = controller.FindById(id);
            
            Console.WriteLine(customer.ToString());

            Console.WriteLine($"New First Name: (leave blank for no change) ({customer.FirstName})");
            string firstName = Console.ReadLine();
            if(!String.IsNullOrEmpty(firstName))
            {
                customer.FirstName = firstName;
            }

            Console.WriteLine($"New Last Name: (leave blank for no change) ({customer.LastName})");
            string lastName = Console.ReadLine();
            if (!String.IsNullOrEmpty(lastName))
            {
                customer.LastName = lastName;
            }

            controller.Update(customer);

            Console.WriteLine("\nCustomer updated!\n");
            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
