using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DataAccess
{
    [Table("ingredients", Schema = "pizza")]
    public partial class Ingredients
    {
        public Ingredients()
        {
            PizzasIngredients = new HashSet<PizzasIngredients>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Column("stock")]
        public int Stock { get; set; }

        [InverseProperty("Ingredient")]
        public virtual ICollection<PizzasIngredients> PizzasIngredients { get; set; }
        
    }
}
