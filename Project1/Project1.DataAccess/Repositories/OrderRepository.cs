using Microsoft.EntityFrameworkCore;
using Project1.DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Project1Context _db;

        public OrderRepository(Project1Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));

            // code-first style, make sure the database exists by now.
            db.Database.EnsureCreated();
        }

        public void Delete(int id)
        {
            Orders tracked = GetById(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Order with this id", nameof(id));
            }
            _db.Remove(tracked);
        }

        public IList GetAll()
        {
            return (List<Orders>) _db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(orderPizzas => orderPizzas.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .ToList();
        }

        public Orders GetById(int id)
        {
            return _db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(order => order.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .Where(model => model.Id == id)
                    .ToList()
                    .First();
        }
        
        public IList GetByName(string name)
        {
            return (List<Orders>)_db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(order => order.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .Where(model => model.Customer.FirstName.Contains(name) || model.Customer.LastName.Contains(name))
                    .ToList();
        }

        public IList GetByCustomer(int customerId)
        {
            return (List<Orders>)_db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(order => order.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .Where(model => model.CustomerId == customerId)
                    .ToList();
        }

        public IList GetByAddress(int addressId)
        {
            return (List<Orders>)_db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(order => order.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .Where(model => model.AddressId == addressId)
                    .ToList();
        }

        public Orders Save(Orders model, int? id = null)
        {
            if (id == null || id < 1)
                return Create(model);
            else
                return Update(model, id);
        }

        public Orders Create(Orders model)
        {
            _db.Add((Orders)model);

            return (Orders)model;
        }

        public Orders Update(Orders model, int? id = null)
        {
            if (id == null)
            {
                throw new ArgumentException("Nedded id", nameof(id));
            }

            Orders tracked = _db.Orders.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Order with this id", nameof(id));
            }

            _db.Entry(tracked).CurrentValues.SetValues(model);
            tracked.OrderPizzas = model.OrderPizzas;

            return (Orders)model;
        }

        public DateTime getLastOrderDate(int addressId)
        {
            DateTime dt = _db.Orders.Where(m => m.AddressId == addressId).DefaultIfEmpty().Max(d => d.Date);
            return dt;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
