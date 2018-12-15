using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1.WebUi.Models.ViewModels
{
    public class OrderForm
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int AddressId { get; set; }

        [Required]
        [Range(1, 500)]
        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public virtual Address Address { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IList<OrderPizza> OrderPizzas { get; set; }

        public IEnumerable<Customer> CustomerList { get; set; }
        public IEnumerable<Pizza> PizzasList { get; set; }

        public OrderForm(IEnumerable<Customer> customerList, IEnumerable<Pizza> pizzasList)
        {
            CustomerList = customerList;
            PizzasList = pizzasList;
            Date = DateTime.Now;
        }

        public OrderForm(Order order, IEnumerable<Customer> customerList, IEnumerable<Pizza> pizzasList)
        {
            Id = order.Id;
            CustomerId = order.CustomerId;
            AddressId = order.AddressId;
            Value = order.Value;
            Date = order.Date;
            Address = order.Address;
            Customer = order.Customer;
            OrderPizzas = order.OrderPizzas;

            CustomerList = customerList;
            PizzasList = pizzasList;
        }
    }
}
