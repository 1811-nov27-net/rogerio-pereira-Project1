using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DataAccess
{
    [Table("pizzasIngredients", Schema = "pizza")]
    public partial class PizzasIngredients
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("pizzaId")]
        public int PizzaId { get; set; }
        [Column("ingredientId")]
        public int IngredientId { get; set; }

        [ForeignKey("IngredientId")]
        [InverseProperty("PizzasIngredients")]
        public virtual Ingredients Ingredient { get; set; }
        [ForeignKey("PizzaId")]
        [InverseProperty("PizzasIngredients")]
        public virtual Pizzas Pizza { get; set; }

        public void addIngredient(Ingredients ingredient)
        {
            Ingredient = ingredient;
        }
    }
}
