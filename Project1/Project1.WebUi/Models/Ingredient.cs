using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.PositiveInfinity)]
        public int Stock { get; set; }
        
        //public virtual List<PizzasIngredients> PizzasIngredients { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} - {Name} ({Stock} in stock)";
        }
    }
}
