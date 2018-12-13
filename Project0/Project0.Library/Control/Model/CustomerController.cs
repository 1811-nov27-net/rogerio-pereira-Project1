using Microsoft.EntityFrameworkCore;
using Project0.DataAccess;
using Project0.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Control.Model
{
    public class CustomerController
    {
        CustomerRepository repository = null;

        public CustomerController()
        {
            repository = new CustomerRepository(DbOptions.Context);
        }

        public List<Customers> getAll(bool? withAddress = false)
        {
            if(withAddress == true)
                return (List<Customers>)repository.GetAllWithAddress();

            return (List<Customers>)repository.GetAll();
        }

        public Customers FindById(int id, bool? withAddress = false)
        {
            if (withAddress == true)
                return (Customers)repository.findByIdWithAddress(id);

            return (Customers)repository.GetById(id);
        }

        public List<Customers> FindByName(string name)
        {
            return (List<Customers>)repository.GetByName(name);
        }

        public Customers Save(Customers customer)
        {
            Customers c = (Customers)repository.Save(customer);
            repository.SaveChanges();
            return c;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.SaveChanges();
        }

        public Customers Update(Customers customer)
        {
            repository.Save(customer, customer.Id);
            repository.SaveChanges();
            return customer;
        }
    }
}
