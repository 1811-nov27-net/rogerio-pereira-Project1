using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DataAccess
{
    [Table("pizzas", Schema = "pizza")]
    public partial class Pizzas
    {
        public Pizzas()
        {
            OrderPizzas = new HashSet<OrderPizzas>();
            PizzasIngredients = new HashSet<PizzasIngredients>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("price", TypeName = "money")]
        public decimal Price { get; set; }

        [InverseProperty("Pizza")]
        public virtual ICollection<OrderPizzas> OrderPizzas { get; set; }
        [InverseProperty("Pizza")]
        public virtual ICollection<PizzasIngredients> PizzasIngredients { get; set; }
    }
}
