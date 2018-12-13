using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int AddressId { get; set; }

        public decimal Value { get; set; }

        public DateTime Date { get; set; }
        
        public virtual Address Address { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderPizza> OrderPizzas { get; set; }

        public override string ToString()
        {
            string ret = $"ID: {Id} - {Customer.FirstName} {Customer.LastName}";
            ret = ret + $"\nDelivered at: { Address.ToString() }";
            ret = ret + $"\nDate: { Date }";
            ret = ret + $"\nValue: $ {Convert.ToDecimal(string.Format("{0:0,00.00}", Value))}";

            if (OrderPizzas.Count > 0)
                ret = ret + "\nPizzas";

            foreach (OrderPizza orderPizza in OrderPizzas)
            {
                Pizza pizza = orderPizza.Pizza;
                ret = ret + $"\nPizza ID: {pizza.Id} - {pizza.Name} - $ {Convert.ToDecimal(string.Format("{0:0,00.00}", pizza.Price))}";
            }

            ret = ret + "\n";

            return ret;
        }
        
        public bool AddPizza(Pizza pizza)
        {
            decimal newValue = Value + pizza.Price;

            if (OrderPizzas.Count >= 12)
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
                OrderPizzas.Add(new OrderPizza() { PizzaId = pizza.Id });
                Value += pizza.Price;
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
