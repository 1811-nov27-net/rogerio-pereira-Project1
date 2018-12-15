using Project1.WebUi.Controllers.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int AddressId { get; set; }
        [Required]
        [Range(1, 500)]
        public decimal Value { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual Address Address { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IList<OrderPizza> OrderPizzas { get; set; }

        public Order()
        {
            OrderPizzas = new List<OrderPizza>();
        }

        public override string ToString()
        {
            /*string ret = $"ID: {Id} - {Customer.FirstName} {Customer.LastName}";
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

            return ret;*/
            return "";
        }
        
        public bool AddPizza(Pizza pizza)
        {
            decimal newValue = Value + pizza.Price;

            if (OrderPizzas.Count >= 12)
            {
                throw new MaximumQuantityException("Maximum quantity of pizzas allowed (12 pizzas).");
            }
            else if (newValue > 500)
            {
                throw new MaximumAmountException("Maximum Order Amount Allowed ($ 500).");
            }
            else
            {
                OrderPizza op = new OrderPizza();
                op.addPizza(pizza);

                if (Id != null && Id > 0)
                    op.OrderId = Id;

                OrderPizzas.Add(op);
                Value += pizza.Price;

                return true;
            }
        }

        public bool canOrderFromSameAddress(DateTime date)
        {
            //If Less then 2 hours can't place order to the address
            double totalHours = (DateTime.Now - date).TotalHours;
            if (totalHours < 2)
            {
                throw new SamePlaceException("Can't order from the same place Within 2 hours.");
            }

            return true;
        }
    }
}
