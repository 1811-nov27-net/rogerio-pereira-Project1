using System.Collections;
using System.Collections.Generic;

namespace Project1.DataAccess.Repositories.Interfaces
{
    public interface IIngredientRepository
    {
        Ingredients Create(Ingredients model);
        void Delete(int id);
        IEnumerable GetAll();
        Ingredients GetById(int id);
        IEnumerable GetByName(string name);
        Ingredients Save(Ingredients model, int? id = null);
        void SaveChanges();
        Ingredients Update(Ingredients model, int? id = null);
    }
}