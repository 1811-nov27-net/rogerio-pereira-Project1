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
            OrderPizzas = new HashSet<OrderPizzas>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("customerId")]
        public int CustomerId { get; set; }
        [Column("addressId")]
        public int AddressId { get; set; }
        [Column("value", TypeName = "money")]
        public decimal Value { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }

        [ForeignKey("AddressId")]
        [InverseProperty("Orders")]
        public virtual Addresses Address { get; set; }
        [ForeignKey("CustomerId")]
        [InverseProperty("Orders")]
        public virtual Customers Customer { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<OrderPizzas> OrderPizzas { get; set; }
    }
}
