using Microsoft.EntityFrameworkCore;
using Project0.DataAccess.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Project0.DataAccess.Repositories
{
    public class OrderRepository : ARepository, IOrderRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="db">Project0Context</param>
        public OrderRepository(Project0Context db) : base(db) { }

        public override void Delete(int id)
        {
            Orders tracked = Db.Orders.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Order with this id", nameof(id));
            }
            Db.Remove(tracked);
        }

        public override IList GetAll()
        {
            return (List<Orders>) Db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(orderPizzas => orderPizzas.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .ToList();
        }

        public override AModel GetById(int id)
        {
            return Db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(order => order.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .Where(model => model.Id == id)
                    .ToList()
                    .First();
        }
        
        public override IList GetByName(string name)
        {
            return (List<Orders>)Db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(order => order.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .Where(model => model.Customer.FirstName.Contains(name) || model.Customer.LastName.Contains(name))
                    .ToList();
        }

        public IList GetByCustomer(int customerId)
        {
            return (List<Orders>)Db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(order => order.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .Where(model => model.CustomerId == customerId)
                    .ToList();
        }

        public IList GetByAddress(int addressId)
        {
            return (List<Orders>)Db.Orders
                    .Include(orderCustomers => orderCustomers.Customer)
                    .Include(orderAddress => orderAddress.Address)
                    .Include(order => order.OrderPizzas)
                    .ThenInclude(pizzas => pizzas.Pizza)
                    .Where(model => model.AddressId == addressId)
                    .ToList();
        }

        protected override AModel Create(AModel model)
        {
            Db.Add((Orders)model);

            return (Orders)model;
        }

        protected override AModel Update(AModel model, int? id = null)
        {
            if (id == null)
            {
                throw new ArgumentException("Nedded id", nameof(id));
            }

            Orders tracked = Db.Orders.Find(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Order with this id", nameof(id));
            }

            Db.Entry(tracked).CurrentValues.SetValues(model);

            return (Orders)model;
        }

        public DateTime getLastOrderDate(int addressId)
        {
            return Db.Orders.Where(m => m.AddressId == addressId).DefaultIfEmpty().Max(d => d.Date);
        }

        public List<Pizzas> getSuggestedPizzas(int customerId)
        {
            List<Pizzas> suggested = new List<Pizzas>();
            
            suggested = Db.Pizzas
                        .FromSql(
                            "select top(3) o.customerId, count(*) as totalPizza, p.id as pizzaId, p.* " +
                            "from pizza.orders o " +
                            "   inner join pizza.orderPizzas op " +
                            "       on o.id = op.orderId " +
                            "   inner join pizza.pizzas p " +
                            "       on op.pizzaId = p.id " +
                            "group by p.id, p.name, p.price, o.customerId " +
                            "having o.customerId = @id " +
                            "order by o.customerId asc, totalPizza desc ",
                            new SqlParameter("@id", customerId)
                        ).ToList();

            return suggested;
        }
    }
}
