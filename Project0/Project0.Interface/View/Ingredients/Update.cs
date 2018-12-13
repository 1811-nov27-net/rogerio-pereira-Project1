using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using IngredientsDataAcess = Project0.DataAccess.Ingredients;

namespace Project0.Interface.View.Ingredients
{
    class Update
    {
        public static void Show()
        {
            IngredientController controller = new IngredientController();
            IngredientsDataAcess ingredient = new IngredientsDataAcess();

            Console.Write("Ingredient ID:\n");
            int id = Int32.Parse(Console.ReadLine());
            
            ingredient = controller.FindById(id);
            
            Console.WriteLine(ingredient.ToString());

            Console.WriteLine($"New name: (leave blank for no change) ({ingredient.Name})");
            string name = Console.ReadLine();
            if(!String.IsNullOrEmpty(name))
            {
                ingredient.Name = name;
            }

            Console.WriteLine($"New stock quantity: (leave blank for no change) ({ingredient.Stock})");
            string stockStr = Console.ReadLine();
            if (!String.IsNullOrEmpty(stockStr))
            {
                int stock = Int32.Parse(stockStr);
                ingredient.Stock = stock;
            }
            
            controller.Update(ingredient);

            Console.WriteLine("\nIngredient updated!\n");
            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
