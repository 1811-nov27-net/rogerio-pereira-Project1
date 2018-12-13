using Microsoft.EntityFrameworkCore;
using Project0.DataAccess;
using Project0.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Control.Model
{
    public class IngredientController
    {
        IngredientRepository repository = null;

        public IngredientController()
        {
            repository = new IngredientRepository(DbOptions.Context);
        }

        public List<Ingredients> getAll()
        {
            return (List<Ingredients>)repository.GetAll();
        }

        public Ingredients FindById(int id)
        {
            return (Ingredients)repository.GetById(id);
        }

        public List<Ingredients> FindByName(string name)
        {
            return (List<Ingredients>)repository.GetByName(name);
        }

        public Ingredients Save(Ingredients ingredient)
        {
            Ingredients i = (Ingredients)repository.Save(ingredient);
            repository.SaveChanges();
            return i;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.SaveChanges();
        }

        public Ingredients Update(Ingredients ingredient)
        {
            repository.Save(ingredient, ingredient.Id);
            repository.SaveChanges();
            return ingredient;
        }
    }
}
