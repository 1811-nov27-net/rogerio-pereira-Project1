using Project0.Interface.View.Ingredients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Interface.View.Menu
{
    public class IngredientMenu
    {
        /// <summary>
        /// Show Ingredients Main Menu
        /// </summary>
        public static void ShowIngredientsMenu()
        {
            string menuOption = new string("");
            string menu = "Ingredients\n\n" +
                            "1 - New\n" +
                            "2 - Invetory\n" +
                            "3 - Find\n" +
                            "4 - Update\n" +
                            //"5 - Delete\n" +
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
                        IngredientForm.ShowForm();
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
                    /*case "5":
                        ClearHelper.Clear();
                        ShowAll.Show();
                        Console.WriteLine();
                        Delete.Show();
                        break;*/
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
