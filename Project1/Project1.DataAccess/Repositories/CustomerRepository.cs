using Microsoft.EntityFrameworkCore;
using Project1.DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Project1Context _db;

        public CustomerRepository(Project1Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));

            // code-first style, make sure the database exists by now.
            db.Database.EnsureCreated();
        }

        public void Delete(int id)
        {
            Customers tracked = GetById(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Customer with this id", nameof(id));
            }
            _db.Remove(tracked);
        }

        public IList GetAll()
        {
            return (List<Customers>)_db.Customers.ToList();
        }

        public IList GetAllWithAddress()
        {
            return (List<Customers>)_db.Customers.Include(m => m.Addresses).ToList();
        }

        public Customers GetById(int id)
        {
            return _db.Customers.Find(id);
        }

        public Customers findByIdWithAddress(int id)
        {
            return _db.Customers.Include(c => c.Addresses).Where(model => model.Id == id).First();
        }

        public IList GetByName(string name)
        {
            return (List<Customers>)_db.Customers.Include(m => m.Addresses).Where(model => model.FirstName.Contains(name) || model.LastName.Contains(name)).ToList();
        }

        public Customers Save(Customers model, int? id = null)
        {
            if (id == null || id < 1)
                return Create(model);
            else
                return Update(model, id);
        }

        public Customers Create(Customers model)
        {
            _db.Add((Customers)model);

            return (Customers)model;
        }

        public Customers Update(Customers model, int? id = null)
        {
            if (id == null)
            {
                throw new ArgumentException("Nedded id", nameof(id));
            }

            Customers tracked = _db.Customers.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Customer with this id", nameof(id));
            }

            _db.Entry(tracked).CurrentValues.SetValues(model);

            return (Customers)model;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
