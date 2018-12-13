using Project0.DataAccess;
using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using PizzasDataAcess = Project0.DataAccess.Pizzas;
using IngredientsDataAcess = Project0.DataAccess.Ingredients;

namespace Project0.Interface.View.Pizzas
{
    class Update
    {
        public static void Show()
        {
            PizzaController controller = new PizzaController();
            PizzasDataAcess Pizza = new PizzasDataAcess();

            Console.Write("Pizza ID:\n");
            int id = Int32.Parse(Console.ReadLine());
            
            Pizza = controller.FindById(id);
            
            Console.WriteLine(Pizza.ToString());

            Console.WriteLine($"New name: (leave blank for no change) ({Pizza.Name})");
            string name = Console.ReadLine();
            if(!String.IsNullOrEmpty(name))
            {
                Pizza.Name = name;
            }

            Console.WriteLine($"New price: (leave blank for no change) ({Convert.ToDecimal(string.Format("{0:0,00.00}", Pizza.Price))})");
            string priceString = Console.ReadLine();
            if (!String.IsNullOrEmpty(priceString))
            {
                decimal price = Decimal.Parse(priceString);
                Pizza.Price = price;
            }

            string option = "";

            //Old Ingredients
            List<PizzasIngredients> newIngredients = new List<PizzasIngredients>();

            foreach(PizzasIngredients pizzaIngredients in Pizza.PizzasIngredients)
            {
                option = "";
                IngredientsDataAcess i = pizzaIngredients.Ingredient;

                do
                {
                    Console.WriteLine($"Ingredient ID:{i.Id} - {i.Name}");
                    Console.WriteLine("k - Keep; d - Delete");
                    option = Console.ReadLine();

                    switch (option)
                    {
                        case "k":
                            newIngredients.Add(new PizzasIngredients() {PizzaId = Pizza.Id, IngredientId = i.Id });
                            break;
                        case "d":
                            break;
                        default:
                            Console.WriteLine("Wrong Option");
                            break;
                    }
                } while (option != "k" && option != "d");
            }

            Console.WriteLine("Retrieving Ingredients, please wait...\n");

            IngredientController ingredientController = new IngredientController();
            List<IngredientsDataAcess> ingredientsList = ingredientController.getAll();

            option = "";
            do
            {
                Console.WriteLine();
                //Show all Ingredientes
                foreach (IngredientsDataAcess ingredient in ingredientsList)
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
                        newIngredients.Add(new PizzasIngredients() { PizzaId = Pizza.Id, IngredientId = ingredientSearch.Id });
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

            //Add INgredients to pizza
            Pizza.PizzasIngredients = new List<PizzasIngredients>();
            Pizza.PizzasIngredients = newIngredients;
            controller.Update(Pizza);

            Console.WriteLine("\nPizza updated!\n");
            Console.ReadKey();
            ClearHelper.Clear();
        }
    }
}
