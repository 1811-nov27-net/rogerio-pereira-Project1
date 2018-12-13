using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using PizzasDataAcess = Project0.DataAccess.Pizzas;

namespace Project0.Interface.View.Pizzas
{
    class ShowAll
    {
        public static void Show()
        {
            Console.WriteLine("Fetching Data, please wait...");

            PizzaController controller = new PizzaController();
            List<PizzasDataAcess> list = controller.GetAllWithIngredients();

            ClearHelper.Clear();
            Console.WriteLine("Pizzas:\n");

            foreach(PizzasDataAcess Pizza in list)
            {
                Console.WriteLine(Pizza.ToString());
            }
        }
    }
}
