using Microsoft.EntityFrameworkCore;
using Project0.DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project0.DataAccess.Repositories
{
    public class IngredientRepository : ARepository, IIngredientRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">Project0Context</param>
        public IngredientRepository(Project0Context db) : base(db) { }

        public override void Delete(int id)
        {
            Ingredients tracked = Db.Ingredients.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Ingredient with this id", nameof(id));
            }
            Db.Remove(tracked);
        }

        public override IList GetAll()
        {
            return (List<Ingredients>) Db.Ingredients.ToList();
        }

        public override AModel GetById(int id)
        {
            return Db.Ingredients.Find(id);
        }

        public override IList GetByName(string name)
        {
            return (List<Ingredients>)Db.Ingredients.Where(model => model.Name.Contains(name)).ToList();
        }

        protected override AModel Create(AModel model)
        {
            Db.Add((Ingredients)model);

            return (Ingredients)model;
        }

        protected override AModel Update(AModel model, int? id = null)
        {
            if (id == null)
            {
                throw new ArgumentException("Nedded id", nameof(id));
            }

            Ingredients tracked = Db.Ingredients.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Ingredient with this id", nameof(id));
            }

            Db.Entry(tracked).CurrentValues.SetValues(model);

            return (Ingredients)model;
        }
    }
}
