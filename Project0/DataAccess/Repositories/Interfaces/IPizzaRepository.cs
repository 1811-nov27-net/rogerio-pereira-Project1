using System.Collections;

namespace Project1.DataAccess.Repositories.Interfaces
{
    public interface IPizzaRepository
    {
        bool CheckStock(Pizzas pizza);
        Pizzas Create(Pizzas model);
        void DecreaseStock(Pizzas pizza);
        void Delete(int id);
        IList GetAll();
        IList GetAllWithIngredients();
        Pizzas GetById(int id);
        IList GetByName(string name);
        Pizzas Save(Pizzas model, int? id = null);
        void SaveChanges();
        Pizzas Update(Pizzas model, int? id = null);
    }
}