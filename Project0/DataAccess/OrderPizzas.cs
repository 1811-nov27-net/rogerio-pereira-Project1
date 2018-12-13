using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project0.DataAccess
{
    [Table("orderPizzas", Schema = "pizza")]
    public partial class OrderPizzas : AModel
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("orderId")]
        public int OrderId { get; set; }
        [Column("pizzaId")]
        public int PizzaId { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("OrderPizzas")]
        public virtual Orders Order { get; set; }
        [ForeignKey("PizzaId")]
        [InverseProperty("OrderPizzas")]
        public virtual Pizzas Pizza { get; set; }
    }
}
