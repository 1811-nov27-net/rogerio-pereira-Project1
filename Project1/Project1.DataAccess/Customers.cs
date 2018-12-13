using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1.DataAccess
{
    [Table("customers", Schema = "pizza")]
    public partial class Customers
    {
        public Customers()
        {
            Addresses = new HashSet<Addresses>();
            Orders = new HashSet<Orders>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("firstName")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Column("lastName")]
        [StringLength(50)]
        public string LastName { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Addresses> Addresses { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<Orders> Orders { get; set; }

        public override string ToString()
        {
            string ret = $"ID: {Id} - {FirstName} {LastName}\n";

            if(Addresses.Count > 0)
                ret = ret + "Addresses\n";

            foreach (Addresses address in Addresses)
            {
                ret = ret + $"{address.ToString()}\n";
            }

            return ret;
        }
    }
}
