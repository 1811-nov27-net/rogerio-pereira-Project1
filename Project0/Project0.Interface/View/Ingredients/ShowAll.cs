using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using IngredientsDataAcess = Project0.DataAccess.Ingredients;

namespace Project0.Interface.View.Ingredients
{
    class ShowAll
    {
        public static void Show()
        {
            Console.WriteLine("Fetching Data, please wait...");

            IngredientController controller = new IngredientController();
            List<IngredientsDataAcess> list = controller.getAll();

            ClearHelper.Clear();
            Console.WriteLine("Ingredients:\n");

            foreach(IngredientsDataAcess ingredient in list)
            {
                Console.WriteLine(ingredient.ToString());
            }
        }
    }
}
