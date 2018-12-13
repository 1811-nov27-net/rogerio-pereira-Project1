using Microsoft.EntityFrameworkCore;
using Project1.DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly Project1Context _db;

        public AddressRepository(Project1Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));

            // code-first style, make sure the database exists by now.
            db.Database.EnsureCreated();
        }

        public void Delete(int id)
        {
            Addresses tracked = GetById(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Address with this id", nameof(id));
            }
            _db.Remove(tracked);
        }

        public IList GetAll()
        {
            return (List<Addresses>) _db.Addresses.Include(m => m.Customer).ToList();
        }

        public Addresses GetById(int id)
        {
            return _db.Addresses.Find(id);
        }

        //Search by the Address 1
        public IList GetByName(string name)
        {
            return (List<Addresses>)_db.Addresses.Where(model => model.Address1.Contains(name)).ToList();
        }
        
        //Search by the Customer Id
        public IList GetByCustomerId(int customerId)
        {
            return (List<Addresses>)_db.Addresses.Where(model => model.CustomerId == customerId).ToList();
        }
        
        public Addresses Save(Addresses model, int? id = null)
        {
            if (id == null || id < 1)
                return Create(model);
            else
                return Update(model, id);
        }

        public Addresses Create(Addresses model)
        {
            _db.Add((Addresses)model);

            return (Addresses)model;
        }

        public Addresses Update(Addresses model, int? id = null)
        {
            if (id == null)
            {
                throw new ArgumentException("Nedded id", nameof(id));
            }

            Addresses tracked = _db.Addresses.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Address with this id", nameof(id));
            }

            _db.Entry(tracked).CurrentValues.SetValues(model);

            return (Addresses)model;
        }

        public void SetDefaultAddress(int addressId, int customerId)
        {
            List<Addresses> addresses = (List<Addresses>)GetByCustomerId(customerId);

            foreach(Addresses address in addresses)
            {
                if(address.Id == addressId)
                    address.DefaultAddress = true;
                else
                    address.DefaultAddress = false;

                Update(address, address.Id);
            }
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
