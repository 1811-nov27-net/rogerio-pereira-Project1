using Microsoft.EntityFrameworkCore;
using Project1.DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly Project1Context _db;

        public IngredientRepository(Project1Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));

            // code-first style, make sure the database exists by now.
            db.Database.EnsureCreated();
        }

        public void Delete(int id)
        {
            Ingredients tracked = _db.Ingredients.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Ingredient with this id", nameof(id));
            }
            _db.Remove(tracked);
        }

        public IList GetAll()
        {
            return (List<Ingredients>) _db.Ingredients.ToList();
        }

        public Ingredients GetById(int id)
        {
            return _db.Ingredients.Find(id);
        }

        public IList GetByName(string name)
        {
            return (List<Ingredients>)_db.Ingredients.Where(model => model.Name.Contains(name)).ToList();
        }

        public Ingredients Save(Ingredients model, int? id = null)
        {
            if (id == null || id < 1)
                return Create(model);
            else
                return Update(model, id);
        }

        public Ingredients Create(Ingredients model)
        {
            _db.Add((Ingredients)model);

            return (Ingredients)model;
        }

        public Ingredients Update(Ingredients model, int? id = null)
        {
            if (id == null)
            {
                throw new ArgumentException("Nedded id", nameof(id));
            }

            Ingredients tracked = _db.Ingredients.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Ingredient with this id", nameof(id));
            }

            _db.Entry(tracked).CurrentValues.SetValues(model);

            return (Ingredients)model;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
