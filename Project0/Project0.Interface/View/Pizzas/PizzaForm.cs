using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using PizzasDataAcess = Project0.DataAccess.Pizzas;
using PizzasIngredientsDataAcess = Project0.DataAccess.PizzasIngredients;
using IngredientsDataAcess = Project0.DataAccess.Ingredients;

namespace Project0.Interface.View.Pizzas
{
    public class PizzaForm
    {
        public static void ShowForm()
        {
            PizzasDataAcess pizza = new PizzasDataAcess();

            Console.Write("Pizza Name:\n");
            pizza.Name = Console.ReadLine();

            Console.Write("Pizza Price:\n");
            pizza.Price = Decimal.Parse(Console.ReadLine());

            Console.WriteLine("Retrieving Ingredients, please wait...\n");

            IngredientController ingredientController = new IngredientController();
            List<IngredientsDataAcess> ingredientsList = ingredientController.getAll();

            string option = "";
            do
            {
                Console.WriteLine();
                //Show all Ingredientes
                foreach(IngredientsDataAcess ingredient in ingredientsList)
                {
                    Console.WriteLine($"Ingredient ID: {ingredient.Id} - {ingredient.Name}");
                }
                Console.WriteLine("\nd - Done");

                Console.Write("\nIngredient ID:\n");
                option = Console.ReadLine();
                int ingredientId = 0;

                //if is integer
                if (int.TryParse(option, out ingredientId))
                {
                    IngredientsDataAcess ingredientSearch = ingredientController.FindById(ingredientId);

                    //If Ingredient exists with this ID
                    if (ingredientSearch != null)
                    {
                        pizza.PizzasIngredients.Add(new PizzasIngredientsDataAcess() { IngredientId = ingredientSearch.Id });
                    }
                    //Not existis ingredients
                    else
                    {
                        Console.WriteLine("Invalid ID!");
                        Console.ReadKey();
                        ClearHelper.Clear();
                    }

                }
                //Not Integer, if is option d
                else if (option != "d")
                {
                    Console.WriteLine("Wrong Id or \"d\" for done");
                    Console.ReadKey();
                    ClearHelper.Clear();
                }
            } while (option != "d");


            PizzaController controller = new PizzaController();
            controller.Save(pizza);

            Console.WriteLine("\nPizza saved!\n");
            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
