using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public virtual ICollection<OrderPizza> OrderPizzas { get; set; }

        public virtual ICollection<PizzaIngredient> PizzasIngredients { get; set; }

        public Pizza()
        {
            PizzasIngredients = new List<PizzaIngredient>();
        }

        public override string ToString()
        {
            string ret = $"ID: {Id} - {Name} {Convert.ToDecimal(string.Format("{0:0,00.00}", Price))}";

            if (PizzasIngredients.Count > 0)
                ret = ret + "\nIngredients\n";

            foreach (PizzaIngredient ingredients in PizzasIngredients)
            {
                ret = ret + $"Ingredient ID:{ingredients.Ingredient.Id} - {ingredients.Ingredient.Name}\n";
            }

            return ret;
        }

        public void addIngredients(Ingredient ingredient)
        {
            PizzaIngredient pi = new PizzaIngredient();
            pi.addIngredient(ingredient);

            if (Id != null && Id > 0)
                pi.PizzaId = Id;

            PizzasIngredients.Add(pi);
        }
    }
}
