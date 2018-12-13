using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Model
{
    /// <summary>
    /// Ingrendients Model
    /// </summary>
    public class Ingredient : AModelBase
    {
        /// <summary>
        /// AModelBase function
        /// </summary>
        /// <returns>Model Name</returns>
        protected override string GetModelName()
        {
            return "Ingredient";
        }

        public Ingredient() { }

        public Ingredient(int id, string name, int quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
;        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
