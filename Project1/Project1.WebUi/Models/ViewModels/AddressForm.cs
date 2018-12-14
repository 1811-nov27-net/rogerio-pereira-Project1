using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.WebUi.Models.ViewModels
{
    public class AddressForm
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

        public IEnumerable<Customer> Customers { get; set; }

        public AddressForm(Address address, IEnumerable<Customer> customers)
        {
            Id = address.Id;
            CustomerId = address.CustomerId;
            DefaultAddress = address.DefaultAddress;
            Address1 = address.Address1;
            Address2 = address.Address2;
            City = address.City;
            State = address.State;
            Zipcode = address.Zipcode;
            Customers = customers;
        }

        public AddressForm(IEnumerable<Customer> customers)
        {
            Customers = customers;
        }
    }
}
