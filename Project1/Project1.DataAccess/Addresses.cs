using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DataAccess
{
    [Table("addresses", Schema = "pizza")]
    public partial class Addresses
    {
        public Addresses()
        {
            Orders = new HashSet<Orders>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("customerId")]
        public int CustomerId { get; set; }
        [Column("defaultAddress")]
        public bool DefaultAddress { get; set; }
        [Required]
        [Column("address1")]
        [StringLength(100)]
        public string Address1 { get; set; }
        [Column("address2")]
        [StringLength(100)]
        public string Address2 { get; set; }
        [Required]
        [Column("city")]
        [StringLength(100)]
        public string City { get; set; }
        [Required]
        [Column("state")]
        [StringLength(2)]
        public string State { get; set; }
        [Column("zipcode")]
        public int Zipcode { get; set; }

        [ForeignKey("CustomerId")]
        [InverseProperty("Addresses")]
        public virtual Customers Customer { get; set; }
        [InverseProperty("Address")]
        public virtual ICollection<Orders> Orders { get; set; }

        public override string ToString()
        {
            string ret = $"Address ID: {Id} - {Address1}.";

            if(!String.IsNullOrEmpty(Address2))
                ret = ret + $" {Address2}.";

            ret = ret + $" {City} - {State}. {Zipcode}";

            if (DefaultAddress == true)
                ret = ret + " *DEFAULT ADDRESS*";

            return ret;
        }
    }
}
