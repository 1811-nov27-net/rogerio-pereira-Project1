using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models
{
    public class Address
    {
        public int Id { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        
        [Required]
        public bool DefaultAddress { get; set; }

        [Required]
        [StringLength(100)]
        public string Address1 { get; set; }
        
        [StringLength(100)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        public int Zipcode { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString()
        {
            string ret = $"Address ID: {Id} - {Address1}.";

            if (!String.IsNullOrEmpty(Address2))
                ret = ret + $" {Address2}.";

            ret = ret + $" {City} - {State}. {Zipcode}";

            if (DefaultAddress == true)
                ret = ret + " *DEFAULT ADDRESS*";

            return ret;
        }
    }
}
