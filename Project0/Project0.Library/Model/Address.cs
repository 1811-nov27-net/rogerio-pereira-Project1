using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Model
{
    /// <summary>
    /// Ingrendients Model
    /// </summary>
    public class Address : AModelBase
    {
        /// <summary>
        /// AModelBase function
        /// </summary>
        /// <returns>Model Name</returns>
        protected override string GetModelName()
        {
            return "Address";
        }

        public int Id { get; set; }
        public int CostumerId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }
}
