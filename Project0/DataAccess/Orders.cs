using Project1.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DataAccess
{
    [Table("orders", Schema = "pizza")]
    public partial class Orders
    {
        public Orders()
        {
            OrderPizzas = new HashSet<OrderPizzas>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("customerId")]
        public int CustomerId { get; set; }
        [Column("addressId")]
        public int AddressId { get; set; }
        [Column("value", TypeName = "money")]
        public decimal Value { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }

        [ForeignKey("AddressId")]
        [InverseProperty("Orders")]
        public virtual Addresses Address { get; set; }
        [ForeignKey("CustomerId")]
        [InverseProperty("Orders")]
        public virtual Customers Customer { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<OrderPizzas> OrderPizzas { get; set; }

        public override string ToString()
        {
            string ret = $"ID: {Id} - {Customer.FirstName} {Customer.LastName}";
            ret = ret + $"\nDelivered at: { Address.ToString() }";
            ret = ret + $"\nDate: { Date }";
            ret = ret + $"\nValue: $ {Convert.ToDecimal(string.Format("{0:0,00.00}", Value))}";

            if (OrderPizzas.Count > 0)
                ret = ret + "\nPizzas";

            foreach (OrderPizzas orderPizza in OrderPizzas)
            {
                Pizzas pizza = orderPizza.Pizza;
                ret = ret + $"\nPizza ID: {pizza.Id} - {pizza.Name} - $ {Convert.ToDecimal(string.Format("{0:0,00.00}", pizza.Price))}";
            }

            ret = ret + "\n";

            return ret;
        }

        ///-------------------------------------Remove and put it in Model Class------------------------------------------------

        /// <summary>
        /// Add Pizzas to Order
        ///     If quantity of pizzas > 12 or amount > 500 doesn't add the pizza
        /// </summary>
        /// <param name="pizza"></param>
        public bool AddPizza(Pizzas pizza)
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
                OrderPizzas.Add(new OrderPizzas() { PizzaId = pizza.Id });
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
