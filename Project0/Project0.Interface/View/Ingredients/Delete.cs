using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using IngredientsDataAcess = Project0.DataAccess.Ingredients;

namespace Project0.Interface.View.Ingredients
{
    class Delete
    {
        public static void Show()
        {
            IngredientsDataAcess ingredient = new IngredientsDataAcess();

            Console.Write("Ingredient ID:\n");
            int id = Int32.Parse(Console.ReadLine());

            IngredientController controller = new IngredientController();
            controller.Delete(id);

            Console.WriteLine("\nIngredient deleted!\n");
            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
