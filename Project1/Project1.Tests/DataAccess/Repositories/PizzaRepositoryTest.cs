using Project1.DataAccess;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using Project1.DataAccess.Repositories;
using System.Collections.Generic;

namespace Project1.Tests.DataAccess.Repositories
{
    public class PizzaRepositoryTest
    {
        [Fact]
        public void CreatePizzasWorks()
        {
            Pizzas pizzaSaved = null;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_create").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                var ingredientRepository = new IngredientRepository(db);
                

                Pizzas pizza = new Pizzas
                {
                    Name = "Pizza",
                    Price = 20,
                };

                repo.Save(pizza);
                repo.SaveChanges();
                pizzaSaved = pizza;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                Pizzas Pizza = db.Pizzas.First(m => m.Name == "Pizza");
                Assert.Equal("Pizza", Pizza.Name);
                Assert.Equal(20, Pizza.Price);
                Assert.NotEqual(0, Pizza.Id); // should get some generated ID
            }
        }

        [Fact]
        public void GetAllWorks()
        {
            List<Pizzas> list = new List<Pizzas>();

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_getAll").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);

                for (int i = 0; i < 5; i++)
                {
                    Pizzas pizza = new Pizzas
                    {
                        Name = $"Pizza {i}",
                        Price = 20
                    };
                    list.Add(pizza);
                    repo.Save(pizza);
                }
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                List<Pizzas> Pizzas = (List<Pizzas>)repo.GetAll();

                Assert.Equal(list.Count, Pizzas.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    Assert.Equal(list[i].Name, Pizzas[i].Name);
                    Assert.Equal(list[i].Price, Pizzas[i].Price);
                }
            }
        }

        [Fact]
        public void GetByIdWorks()
        {
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_getById").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                var ingredientRepository = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients() { Name = "Ingredient", Stock = 10 };
                ingredientRepository.Save(ingredient);
                ingredientRepository.SaveChanges();

                Pizzas pizza = new Pizzas
                {
                    Name = "Pizza 1",
                    Price = 20
                };
                pizza.PizzasIngredients.Add(new PizzasIngredients() { IngredientId = ingredient.Id } );
                repo.Save(pizza);
                repo.SaveChanges();
                id = pizza.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                Pizzas Pizza = (Pizzas)repo.GetById(id);

                Assert.NotEqual(0, Pizza.Id);
                Assert.Equal("Pizza 1", Pizza.Name);
                Assert.Equal(20, Pizza.Price);
            }
        }
        
        [Fact]
        public void GetByIdThatDoesntExistReturnsNull()
        {
            int id = 100;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_getById").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            //using (var db = new Project1Context(options))
            //{
            //var repo = new PizzaRepository(db);

            //Pizzas Pizza = new Pizzas { Name = "Test By Id", Stock = 10 };
            //repo.Save(Pizza);
            //repo.SaveChanges();
            //id = Pizza.Id;
            //}

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                Pizzas Pizza = (Pizzas)repo.GetById(id);

                Assert.Null(Pizza);
            }
        }

        [Fact]
        public void GetByNameWorks()
        {
            List<Pizzas> inserted = new List<Pizzas>();
            string name = "Test";
            string wrongName = "Name";
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_getByName_List").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                var ingredientRepository = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients() { Name = "Ingredient", Stock = 10 };
                ingredientRepository.Save(ingredient);
                ingredientRepository.SaveChanges();

                Pizzas Pizza = new Pizzas {
                    Name = "Test Pizza 1",
                    Price = 20,
                };
                Pizza.PizzasIngredients.Add(new PizzasIngredients() { IngredientId = ingredient.Id });
                repo.Save(Pizza);
                inserted.Add(Pizza);
                
                Pizza = new Pizzas
                {
                    Name = "Name Pizza 2",
                    Price = 20,
                };
                Pizza.PizzasIngredients.Add(new PizzasIngredients() { IngredientId = ingredient.Id });
                repo.Save(Pizza);
                inserted.Add(Pizza);
                
                Pizza = new Pizzas
                {
                    Name = "Test Pizza 3",
                    Price = 20,
                };
                Pizza.PizzasIngredients.Add(new PizzasIngredients() { IngredientId = ingredient.Id });
                repo.Save(Pizza);
                inserted.Add(Pizza);
                
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                List<Pizzas> list = (List<Pizzas>)repo.GetByName(name);

                Assert.Equal(2, list.Count);

                foreach (Pizzas Pizza in list)
                {
                    Assert.NotEqual(0, Pizza.Id);
                    Assert.DoesNotContain(wrongName, Pizza.Name);
                    Assert.Contains(name, Pizza.Name);
                    Assert.Equal(20, Pizza.Price);
                }
            }
        }

        [Fact]
        public void GetByNameThatDoesntExistsReturnsNull()
        {
            string name = "Not existing name";

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_getByName_List_Wrong").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);

                Pizzas Pizza = new Pizzas
                {
                    Name = "Name Pizza 1",
                    Price = 20,
                };
                repo.Save(Pizza);

                Pizza = new Pizzas
                {
                    Name = "Name Pizza 2",
                    Price = 20,
                };
                repo.Save(Pizza);

                Pizza = new Pizzas
                {
                    Name = "Name Pizza 3",
                    Price = 20,
                };
                repo.Save(Pizza);

                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                List<Pizzas> list = (List<Pizzas>)repo.GetByName(name);

                Assert.Empty(list);
            }
        }

        [Fact]
        public void DeleteWorks()
        {
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                var ingredientRepository = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients() { Name = "Ingredient", Stock = 10 };
                ingredientRepository.Save(ingredient);
                ingredientRepository.SaveChanges();

                Pizzas pizza = new Pizzas
                {
                    Name = "Pizza 1",
                    Price = 20,
                };
                pizza.PizzasIngredients.Add(new PizzasIngredients() { IngredientId = ingredient.Id });
                repo.Save(pizza);
                repo.SaveChanges();
                id = pizza.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                Pizzas pizza = (Pizzas)repo.GetById(id);

                Assert.NotEqual(0, pizza.Id);
                Assert.Equal("Pizza 1", pizza.Name);
                Assert.Equal(20, pizza.Price);

                repo.Delete(id);
                repo.SaveChanges();
                pizza = (Pizzas)repo.GetById(id);

                Assert.Null(pizza);
            }
        }

        [Fact]
        public void DeleteWithIdThatDoesntExistThrowsException()
        {
            int id = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            //using (var db = new Project1Context(options))
            //{
                //var repo = new PizzaRepository(db);

                //Pizzas Pizza = new Pizzas { Name = "Test Delete", Stock = 10 };
                //repo.Save(Pizza);
                //repo.SaveChanges();
                //id = Pizza.Id;
            //}

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                Pizzas Pizza = (Pizzas)repo.GetById(id);

                Assert.Null(Pizza);

                Assert.Throws<ArgumentException>(() => repo.Delete(id));
            }
        }

        [Fact]
        public void UpdateWorks()
        {
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                var ingredientRepository = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients() { Name = "Ingredient", Stock = 10 };
                ingredientRepository.Save(ingredient);
                ingredientRepository.SaveChanges();

                Pizzas pizza = new Pizzas
                {
                    Name = "Pizza 1",
                    Price = 20,
                };
                pizza.PizzasIngredients.Add(new PizzasIngredients() { IngredientId = ingredient.Id });
                repo.Save(pizza);
                repo.SaveChanges();
                id = pizza.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                Pizzas pizza = (Pizzas)repo.GetById(id);

                Assert.NotEqual(0, pizza.Id);
                Assert.Equal("Pizza 1", pizza.Name);
                Assert.Equal(20, pizza.Price);
                

                pizza.Name = "Pizza 1 alt";
                pizza.Price = 40;

                repo.Save(pizza, id);
                repo.SaveChanges();

                pizza = (Pizzas)repo.GetById(id);

                Assert.NotEqual(0, pizza.Id);
                Assert.Equal("Pizza 1 alt", pizza.Name);
                Assert.Equal(40, pizza.Price);
            }
        }
        
        [Fact]
        public void UpdateWithWorngIdShouldReturnException()
        {
            int id = 0;
            int idWrong = 1000;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_pizza_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                var ingredientRepository = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients() { Name = "Ingredient", Stock = 10 };
                ingredientRepository.Save(ingredient);
                ingredientRepository.SaveChanges();

                Pizzas pizza = new Pizzas
                {
                    Name = "Pizza 1",
                    Price = 20,
                };
                pizza.PizzasIngredients.Add(new PizzasIngredients() { IngredientId = ingredient.Id });
                repo.Save(pizza);
                repo.SaveChanges();
                id = pizza.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new PizzaRepository(db);
                Pizzas pizza = (Pizzas)repo.GetById(id);

                Assert.NotEqual(0, pizza.Id);
                Assert.Equal("Pizza 1", pizza.Name);
                Assert.Equal(20, pizza.Price);
                
                pizza.Name = "Pizza 1 alt";
                pizza.Price = 40;
                
                Assert.Throws<ArgumentException>(() => repo.Save(pizza, idWrong));
            }
        }
    }
}