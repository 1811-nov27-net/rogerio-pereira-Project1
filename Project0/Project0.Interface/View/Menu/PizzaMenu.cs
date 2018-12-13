using Project0.Interface.View.Pizzas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Interface.View.Menu
{
    public class PizzaMenu
    {
        /// <summary>
        /// Show Pizza Main Menu
        /// </summary>
        public static void ShowPizzaMenu()
        {
            string menuOption = new string("");
            string menu = "Pizzas\n\n" +
                            "1 - New\n" +
                            "2 - Show All\n" +
                            "3 - Find\n" +
                            "4 - Update\n" +
                            "\nb - Back\n\n" +
                            "Option: ";
            do
            {
                Console.Write(menu);
                menuOption = Console.ReadLine();

                switch (menuOption)
                {
                    case "1":
                        ClearHelper.Clear();
                        PizzaForm.ShowForm();
                        break;
                    case "2":
                        ClearHelper.Clear();
                        ShowAll.Show();
                        Console.WriteLine();
                        Console.ReadKey();
                        ClearHelper.Clear();
                        break;
                    case "3":
                        ClearHelper.Clear();
                        SearchForm.Search();
                        break;
                    case "4":
                        ClearHelper.Clear();
                        ShowAll.Show();
                        Console.WriteLine();
                        Update.Show();
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
    }
}
