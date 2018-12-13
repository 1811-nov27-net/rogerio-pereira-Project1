using Microsoft.EntityFrameworkCore;
using Project0.DataAccess;
using Project0.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Control.Model
{
    public class AddressController
    {
        AddressRepository repository = null;

        public AddressController()
        {
            repository = new AddressRepository(DbOptions.Context);
        }

        public List<Addresses> getAll()
        {
            return (List<Addresses>)repository.GetAll();
        }

        public Addresses FindById(int id)
        {
            return (Addresses)repository.GetById(id);
        }

        public List<Addresses> FindByName(string name)
        {
            return (List<Addresses>)repository.GetByName(name);
        }

        public List<Addresses> FindByCustomerId(int customerId)
        {
            return (List<Addresses>)repository.GetByCustomerId(customerId);
        }

        public Addresses Save(Addresses Address)
        {
            Addresses c = (Addresses)repository.Save(Address);
            repository.SaveChanges();
            return c;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.SaveChanges();
        }

        public Addresses Update(Addresses Address)
        {
            repository.Save(Address, Address.Id);
            repository.SaveChanges();
            return Address;
        }

        public void SetDefaultAddress(int addressId, int customerId)
        {
            repository.SetDefaultAddress(addressId, customerId);
            repository.SaveChanges();
        }
    }
}
