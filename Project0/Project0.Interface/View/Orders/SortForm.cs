using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrdersDataAcess = Project0.DataAccess.Orders;

namespace Project0.Interface.View.Orders
{
    class SortForm
    {
        public static List<OrdersDataAcess> Sort(List<OrdersDataAcess> list)
        {
            string menuOption = new string("");
            string menu = "Sort by:\n\n" +
                            "1 - Earliest First\n" +              //Older First
                            "2 - Latest First\n" +                //Newer Fisrt
                            "3 - Cheapest First\n" +              //Cheapest First
                            "4 - Most Expensive First\n" +        //Most Expensive First
                            "Option: ";
            do
            {
                Console.Write(menu);
                menuOption = Console.ReadLine();

                switch (menuOption)
                {
                    case "1":
                        list = list.OrderBy(m => m.Date).ToList();
                        break;
                    case "2":
                        list = list.OrderByDescending(m => m.Date).ToList();
                        break;
                    case "3":
                        list = list.OrderBy(m => m.Value).ToList();
                        break;
                    case "4":
                        list = list.OrderByDescending(m => m.Value).ToList();
                        break;
                    default:
                        Console.WriteLine("Wrong option!");
                        Console.ReadKey();
                        ClearHelper.Clear();
                        break;
                }
            } while (menuOption != "1" && menuOption != "2" && menuOption != "3" && menuOption != "4");

            return list;
        }
    }
}
