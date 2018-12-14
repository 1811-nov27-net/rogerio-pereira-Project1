using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models.ViewModels
{
    public class PizzaForm
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<OrderPizza> OrderPizzas { get; set; }

        public virtual ICollection<PizzaIngredient> PizzasIngredients { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }

        public PizzaForm(Pizza pizza, IEnumerable<Ingredient> ingredients)
        {
            Id = pizza.Id;
            Name = pizza.Name;
            Price = pizza.Price;
            PizzasIngredients = pizza.PizzasIngredients;

            Ingredients = ingredients;
        }

        public PizzaForm(IEnumerable<Ingredient> ingredients)
        {
            Ingredients = ingredients;
        }
    }
}
