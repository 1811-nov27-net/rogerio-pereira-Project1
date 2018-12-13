using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using AddressesDataAccess = Project0.DataAccess.Addresses;
using CustomerDataAccess = Project0.DataAccess.Customers;
using OrderDataAccess = Project0.DataAccess.Orders;
using PizzaDataAccess = Project0.DataAccess.Pizzas;

namespace Project0.Interface.View.Orders
{
    public class OrderForm
    {
        public static void ShowForm()
        {
            Console.WriteLine("Fetching Data, please wait...");

            OrderController orderController = new OrderController();
            OrderDataAccess order = new OrderDataAccess();

            //Customer
            CustomerController customerController = new CustomerController();
            List<CustomerDataAccess> customersList = customerController.getAll();
            CustomerDataAccess customer = null;
            int customerId = 0;

            do
            {
                ClearHelper.Clear();
                Console.WriteLine("New Order\n");

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

            order.CustomerId = customer.Id;

            //Customer Address
            Console.WriteLine("Fetching Customer Addresses, please wait...");

            AddressController addressController = new AddressController();
            List<AddressesDataAccess> addresses = addressController.FindByCustomerId(customer.Id);
            AddressesDataAccess address = null;
            int addressId = 0;
            bool validAddress = false;

            do
            {
                ClearHelper.Clear();
                Console.WriteLine("Customer Address\n");

                //Show all customers
                foreach (AddressesDataAccess a in addresses)
                {
                    Console.WriteLine(a.ToString());
                }

                //Select Customer
                Console.WriteLine("\nSelect Customer's Address:\n");
                addressId = Int32.Parse(Console.ReadLine());
                address = addressController.FindById(addressId);

                if (address == null)
                {
                    Console.WriteLine("\nWrong Customer Address's Id");
                    Console.ReadKey();
                }

                Console.WriteLine("\nChecking last order from this address. Please wait!\n");
                DateTime lastOrder = orderController.getLastOrderDate(address.Id);
                
                if (order.canOrderFromSameAddress(lastOrder) == true)
                {
                    order.AddressId = addressId;
                    validAddress = true;
                }
                else
                {
                    Console.WriteLine("It's been less then 2 hours from the last order from this address");
                    Console.ReadKey();
                }
            } while (validAddress != true);

            //Pizzas
            Console.WriteLine("Fetching Pizzas, please wait...");

            PizzaController pizzaController = new PizzaController();
            List<PizzaDataAccess> pizzas = pizzaController.getAll();
            List<PizzaDataAccess> suggestedPizzas = orderController.getSuggestedPizzas(customer.Id);
            PizzaDataAccess pizza = null;
            string pizzaOption = null;
            int pizzaId = 0;

            do
            {
                pizzas = null;
                pizzas = pizzaController.getAll();

                ClearHelper.Clear();
                Console.WriteLine("Adding Pizzas to Order\n");

                //Show all pizzas
                foreach (PizzaDataAccess p in pizzas)
                {
                    //Cannot use Console.WriteLine(p.ToString()); because after adding a pizza to order, it shows the pizza's ingredient, don't know why
                    Console.WriteLine($"ID: {p.Id} - {p.Name} {Convert.ToDecimal(string.Format("{0:0,00.00}", p.Price))}");
                }
                
                //Suggested Pizzas
                Console.WriteLine("\n***SUGGESTED PIZZAS***");
                foreach (PizzaDataAccess sp in suggestedPizzas)
                {
                    Console.WriteLine(sp.ToString());
                }
                Console.WriteLine("***END SUGGESTED PIZZAS***");

                Console.WriteLine("\nd - Done");

                //Select pizza
                Console.WriteLine("\nSelect Pizza's Id:\n");
                pizzaOption = Console.ReadLine();

                bool pizzaOptionIsNumber = int.TryParse(pizzaOption, out pizzaId);

                //If pizzaOption is number get pizza
                if (pizzaOptionIsNumber == true)
                {
                    pizza = pizzaController.FindById(pizzaId);

                    if (pizza == null)
                    {
                        Console.WriteLine("\nWrong Pizza's Id");
                        Console.ReadKey();
                    }

                    bool checkStock = true;
                    bool canAddMorePizzas = true;

                    //Check Stock
                    if (pizzaController.CheckStock(pizza) == true)
                    {
                        canAddMorePizzas = order.AddPizza(pizza);
                        
                        //Cannot add more pizzas, then pizza Option = "d" for DONE, to stop while
                        if (canAddMorePizzas == false)
                        {
                            pizzaOption = "d";
                        }
                        else
                        {
                            pizzaController.DecreaseStock(pizza);
                            Console.WriteLine($"Pizza {pizza.Id} added!");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cannot add this pizza, Pizza's Ingredient stock is low");
                        Console.ReadKey();
                    }
                }
                //else, check if is "d" for Done
                else if(pizzaOption != "d")
                {
                    Console.WriteLine("Wrong option");
                }
            } while (pizzaOption != "d");

            order.Date = DateTime.Now;
            orderController.Save(order);

            Console.WriteLine("\nOrder saved!\n");
            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
