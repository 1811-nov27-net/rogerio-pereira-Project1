using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Model
{
    /// <summary>
    /// Ingrendients Model
    /// </summary>
    public class Costumer : AModelBase
    {
        /// <summary>
        /// AModelBase function
        /// </summary>
        /// <returns>Model Name</returns>
        protected override string GetModelName()
        {
            return "Costumer";
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }
}
