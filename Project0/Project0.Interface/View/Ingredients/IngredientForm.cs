using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using IngredientsDataAcess = Project0.DataAccess.Ingredients;

namespace Project0.Interface.View.Ingredients
{
    public class IngredientForm
    {
        public static void ShowForm()
        {
            IngredientsDataAcess ingredient = new IngredientsDataAcess();

            Console.Write("Ingrediente Name:\n");
            ingredient.Name = Console.ReadLine();

            Console.Write("Quantity in Stock:\n");
            ingredient.Stock = Int32.Parse(Console.ReadLine());

            IngredientController controller = new IngredientController();
            controller.Save(ingredient);

            Console.WriteLine("\nIngredient saved!\n");
            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
