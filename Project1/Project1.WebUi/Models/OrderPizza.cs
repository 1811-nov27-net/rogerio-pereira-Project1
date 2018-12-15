using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models
{
    public class OrderPizza
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int PizzaId { get; set; }
        
        public virtual Order Order { get; set; }

        public virtual Pizza Pizza { get; set; }

        public void addPizza(Pizza pizza)
        {
            Pizza = pizza;
            PizzaId = pizza.Id;
        }
    }
}
