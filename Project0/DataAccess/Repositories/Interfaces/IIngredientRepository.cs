using System.Collections;

namespace Project1.DataAccess.Repositories.Interfaces
{
    public interface IIngredientRepository
    {
        Ingredients Create(Ingredients model);
        void Delete(int id);
        IList GetAll();
        Ingredients GetById(int id);
        IList GetByName(string name);
        Ingredients Save(Ingredients model, int? id = null);
        void SaveChanges();
        Ingredients Update(Ingredients model, int? id = null);
    }
}