using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models.ViewModels
{
    public class CustomerAddressForm
    {
        public Customer Customer { get; set; } 
        public Address Address { get; set; }
    }
}
