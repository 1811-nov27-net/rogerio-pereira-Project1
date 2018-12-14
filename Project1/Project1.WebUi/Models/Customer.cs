using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Addresses = new List<Address>();
        }

        public override string ToString()
        {
            string ret = $"ID: {Id} - {FirstName} {LastName}\n";

            if (Addresses.Count > 0)
                ret = ret + "Addresses\n";

            foreach (Address address in Addresses)
            {
                ret = ret + $"{address.ToString()}\n";
            }

            return ret;
        }
    }
}
