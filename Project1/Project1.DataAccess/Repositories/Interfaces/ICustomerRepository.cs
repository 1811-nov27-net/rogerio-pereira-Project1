using System.Collections;

namespace Project1.DataAccess.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customers Create(Customers model);
        void Delete(int id);
        Customers findByIdWithAddress(int id);
        IList GetAll();
        IList GetAllWithAddress();
        Customers GetById(int id);
        IList GetByName(string name);
        Customers Save(Customers model, int? id = null);
        void SaveChanges();
        Customers Update(Customers model, int? id = null);
    }
}