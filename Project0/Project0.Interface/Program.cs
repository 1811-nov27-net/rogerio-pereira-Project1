using Project0.DataAccess;
using Project0.Interface.View.Menu;
using System;

namespace Project0.Interface
{
    public class Program
    {
        static void Main(string[] args)
        {
            DbOptions.ConfigDatabase();
            MainMenu.ShowMainMenu();
        }
    }
}
