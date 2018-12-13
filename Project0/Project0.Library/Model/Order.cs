using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Model
{
    /// <summary>
    /// Ingrendients Model
    /// </summary>
    public class Order : AModelBase
    {
        /// <summary>
        /// AModelBase function
        /// </summary>
        /// <returns>Model Name</returns>
        protected override string GetModelName()
        {
            return "Order";
        }

        public Order()
        {
            Pizzas = new List<Pizza>();
        }

        public int Id { get; set; }
        public int CostumerId { get; set; }
        public int AddressId { get; set; }
        public List<Pizza> Pizzas { get; }
        public double Value { get; set; }
        public DateTime date { get; set; }

        /// <summary>
        /// Add Pizzas to Order
        ///     If quantity of pizzas > 12 or amount > 500 doesn't add the pizza
        /// </summary>
        /// <param name="pizza"></param>
        public bool AddPizza(Pizza pizza)
        {
            double newValue = Value + pizza.Price;

            if (Pizzas.Count >= 12)
            {
                Console.WriteLine("Maximum quantity of pizzas allowed (12 pizzas)");
                return false;
            }
            else if (newValue > 500)
            {
                Console.WriteLine("Maximum Order Amount Allowed ($ 500)");
                return false;
            }
            else
            {
                Pizzas.Add(pizza);
                Value += pizza.Price;
                Console.Write(" - added");
                Console.WriteLine();
                return true;
            }
        }

        public bool canOrderFromSameAddress(DateTime date)
        {
            //If Less then 2 hours can't place order to the address
            double totalHours = (DateTime.Now - date).TotalHours;
            if (totalHours < 2)
                return false;

            return true;
        }
    }
}
