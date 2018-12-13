using System.Collections;

namespace Project1.DataAccess.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Addresses Create(Addresses model);
        void Delete(int id);
        IList GetAll();
        IList GetByCustomerId(int customerId);
        Addresses GetById(int id);
        IList GetByName(string name);
        Addresses Save(Addresses model, int? id = null);
        void SaveChanges();
        void SetDefaultAddress(int addressId, int customerId);
        Addresses Update(Addresses model, int? id = null);
    }
}