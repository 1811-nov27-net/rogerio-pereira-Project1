using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using PizzasDataAcess = Project0.DataAccess.Pizzas;

namespace Project0.Interface.View.Pizzas
{
    class SearchForm
    {
        private static PizzaController controller = new PizzaController();

        public static void Search()
        {
            string menuOption = new string("");
            string menu = "Pizza Search\n\n" +
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
            Console.WriteLine("Pizza Search\n");
            Console.WriteLine("Search by ID:");
            int id = Int32.Parse(Console.ReadLine());
            
            Console.WriteLine("\nFetching Data, please wait...");
            PizzasDataAcess pizza = controller.FindById(id);

            Console.WriteLine();

            if (pizza == null)
                Console.WriteLine($"There is no Ingredient with this Id");
            else
                Console.WriteLine(pizza.ToString());

            Console.ReadKey();
            ClearHelper.Clear();
        }

        private static void SearchByName()
        {
            ClearHelper.Clear();
            Console.WriteLine("Pizza Search\n");
            Console.WriteLine("Search by Name:");
            string name = Console.ReadLine();

            Console.WriteLine("\nFetching Data, please wait...");
            List<PizzasDataAcess> pizzas = controller.FindByName(name);

            Console.WriteLine();

            if (pizzas.Count == 0)
                Console.WriteLine($"There is no Ingredient with this Name");
            else
            {
                foreach(PizzasDataAcess pizza in pizzas)
                    Console.WriteLine(pizza.ToString());
            }

            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
