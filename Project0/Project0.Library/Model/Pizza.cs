using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Model
{
    /// <summary>
    /// Ingrendients Model
    /// </summary>
    public class Pizza : AModelBase
    {
        /// <summary>
        /// AModelBase function
        /// </summary>
        /// <returns>Model Name</returns>
        protected override string GetModelName()
        {
            return "Pizza";
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
