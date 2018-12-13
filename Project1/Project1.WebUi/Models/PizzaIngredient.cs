using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models
{
    public class PizzaIngredient
    {
        public int Id { get; set; }
        
        public int PizzaId { get; set; }

        public int IngredientId { get; set; }
        
        public virtual Ingredient Ingredient { get; set; }

        public virtual Pizza Pizza { get; set; }

        public void addIngredient(Ingredient ingredient)
        {
            Ingredient = ingredient;
        }
    }
}
