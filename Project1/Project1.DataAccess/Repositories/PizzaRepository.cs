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
    public class PizzaRepository : IPizzaRepository
    {
        private readonly Project1Context _db;

        public PizzaRepository(Project1Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));

            // code-first style, make sure the database exists by now.
            db.Database.EnsureCreated();
        }

        public void Delete(int id)
        {
            Pizzas tracked = GetById(id);
            if (tracked == null)
            {
                throw new ArgumentException("No Pizza with this id", nameof(id));
            }
            _db.Remove(tracked);
        }

        public IList GetAll()
        {
            return (List<Pizzas>) _db.Pizzas.ToList();
        }

        public IList GetAllWithIngredients()
        {
            return (List<Pizzas>)_db.Pizzas
                    .Include(pizza => pizza.PizzasIngredients)
                    .ThenInclude(pizzasIngredients => pizzasIngredients.Ingredient)
                    .ToList();
        }

        public Pizzas GetById(int id)
        {
            return _db.Pizzas
                    .Include(pizza => pizza.PizzasIngredients)
                    .ThenInclude(pizzasIngredients => pizzasIngredients.Ingredient)
                    .Where(model => model.Id == id)
                    .ToList()
                    .First();
        }

        public IList GetByName(string name)
        {
            return (List<Pizzas>)_db.Pizzas
                    .Include(pizza => pizza.PizzasIngredients)
                    .ThenInclude(pizzasIngredients => pizzasIngredients.Ingredient)
                    .Where(model => model.Name.Contains(name))
                    .ToList();
        }

        public Pizzas Save(Pizzas model, int? id = null)
        {
            if (id == null || id < 1)
                return Create(model);
            else
                return Update(model, id);
        }

        public Pizzas Create(Pizzas model)
        {
            _db.Add((Pizzas)model);

            return (Pizzas)model;
        }

        public Pizzas Update(Pizzas model, int? id = null)
        {
            if (id == null)
            {
                throw new ArgumentException("Nedded id", nameof(id));
            }

            Pizzas tracked = _db.Pizzas
                    .Include(pizza => pizza.PizzasIngredients)
                    .ThenInclude(pizzasIngredients => pizzasIngredients.Ingredient)
                    .Where(pizza => pizza.Id == id)
                    .First();
            if (tracked == null)
            {
                throw new ArgumentException("No Pizza with this id", nameof(id));
            }

            _db.Entry(tracked).CurrentValues.SetValues(model);
            tracked.PizzasIngredients = model.PizzasIngredients;

            return (Pizzas)model;
        }

        public bool CheckStock(Pizzas pizza)
        {
            bool stockAvailable = true;

            List<PizzasIngredients> pizzaIngredients = _db.PizzasIngredients
                                                        .Include(p => p.Ingredient)
                                                        .Where(p => p.PizzaId == pizza.Id)
                                                        .ToList();

            foreach(PizzasIngredients pizzaIngredient in pizzaIngredients)
            {
                Ingredients ingredient = pizzaIngredient.Ingredient;
                if ((ingredient.Stock - 1) < 0)
                    stockAvailable = false;
            }

            return stockAvailable;
        }

        public void DecreaseStock(Pizzas pizza)
        {
            List<PizzasIngredients> pizzaIngredients = _db.PizzasIngredients
                                                        .Include(p => p.Ingredient)
                                                        .Where(p => p.PizzaId == pizza.Id)
                                                        .ToList();

            foreach (PizzasIngredients pizzaIngredient in pizzaIngredients)
            {
                IngredientRepository ingredientRepository = new IngredientRepository(_db);
                Ingredients ingredient = (Ingredients)ingredientRepository.GetById(pizzaIngredient.IngredientId);
                ingredient.Stock = ingredient.Stock-1;
                ingredientRepository.Update(ingredient, ingredient.Id);
                ingredientRepository.SaveChanges();
            }
        }

        public List<Pizzas> getSuggestedPizzas(int customerId)
        {
            List<Pizzas> suggested = new List<Pizzas>();

            suggested = _db.Pizzas
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

        public void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
