using Project1.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DataAccess
{
    [Table("orders", Schema = "pizza")]
    public partial class Orders
    {
        public Orders()
        {
            OrderPizzas = new List<OrderPizzas>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("customerId")]
        public int CustomerId { get; set; }
        [Required]
        [Column("addressId")]
        public int AddressId { get; set; }
        [Required]
        [Range(1, 500)]
        [Column("value", TypeName = "money")]
        public decimal Value { get; set; }
        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [ForeignKey("AddressId")]
        [InverseProperty("Orders")]
        public virtual Addresses Address { get; set; }
        [ForeignKey("CustomerId")]
        [InverseProperty("Orders")]
        public virtual Customers Customer { get; set; }
        [InverseProperty("Order")]
        public virtual IEnumerable<OrderPizzas> OrderPizzas { get; set; }
    }
}
