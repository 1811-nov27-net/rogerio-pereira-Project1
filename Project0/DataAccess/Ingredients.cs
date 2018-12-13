using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project0.DataAccess
{
    [Table("ingredients", Schema = "pizza")]
    public partial class Ingredients : AModel
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
        [Column("stock")]
        public int Stock { get; set; }

        [InverseProperty("Ingredient")]
        public virtual ICollection<PizzasIngredients> PizzasIngredients { get; set; }

        public override string ToString()
        {
            return $"ID: {Id} - {Name} ({Stock} in stock)";
        }
    }
}
