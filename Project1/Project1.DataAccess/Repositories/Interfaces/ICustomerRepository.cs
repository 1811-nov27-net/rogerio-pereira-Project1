using System.Collections;

namespace Project1.DataAccess.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customers Create(Customers model);
        void Delete(int id);
        Customers findByIdWithAddress(int id);
        IEnumerable GetAll();
        IEnumerable GetAllWithAddress();
        Customers GetById(int id);
        IEnumerable GetByName(string name);
        Customers Save(Customers model, int? id = null);
        void SaveChanges();
        Customers Update(Customers model, int? id = null);
    }
}