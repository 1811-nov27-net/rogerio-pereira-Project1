using Project0.Interface.View.Menu;
using Project0.Library.Control.Model;
using System;
using System.Collections.Generic;
using System.Text;
using OrdersDataAcess = Project0.DataAccess.Orders;

namespace Project0.Interface.View.Orders
{
    class ShowAll
    {
        public static void Show()
        {
            Console.WriteLine("Fetching Data, please wait...");

            OrderController controller = new OrderController();
            List<OrdersDataAcess> list = controller.getAll();

            list = SortForm.Sort(list);

            ClearHelper.Clear();
            Console.WriteLine("Orders:\n");

            foreach(OrdersDataAcess order in list)
            {
                Console.WriteLine(order.ToString());
            }
        }
    }
}
