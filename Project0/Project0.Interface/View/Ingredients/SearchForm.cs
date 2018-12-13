using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using IngredientsDataAcess = Project0.DataAccess.Ingredients;

namespace Project0.Interface.View.Ingredients
{
    class SearchForm
    {
        private static IngredientController controller = new IngredientController();

        public static void Search()
        {
            string menuOption = new string("");
            string menu = "Ingredients Search\n\n" +
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
            Console.WriteLine("Ingredients Search\n");
            Console.WriteLine("Search by ID:");
            int id = Int32.Parse(Console.ReadLine());
            
            Console.WriteLine("\nFetching Data, please wait...");
            IngredientsDataAcess ingredient = controller.FindById(id);

            Console.WriteLine();

            if (ingredient == null)
                Console.WriteLine($"There is no Ingredient with this Id");
            else
                Console.WriteLine(ingredient.ToString());

            Console.ReadKey();
            ClearHelper.Clear();
        }

        private static void SearchByName()
        {
            ClearHelper.Clear();
            Console.WriteLine("Ingredients Search\n");
            Console.WriteLine("Search by Name:");
            string name = Console.ReadLine();

            Console.WriteLine("\nFetching Data, please wait...");
            List<IngredientsDataAcess> ingredients = controller.FindByName(name);

            Console.WriteLine();

            if (ingredients.Count == 0)
                Console.WriteLine($"There is no Ingredient with this Name");
            else
            {
                foreach(IngredientsDataAcess ingredient in ingredients)
                    Console.WriteLine(ingredient.ToString());
            }

            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
