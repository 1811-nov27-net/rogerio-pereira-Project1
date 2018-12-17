using Project1.DataAccess;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using Project1.DataAccess.Repositories;
using System.Collections.Generic;

namespace Project1.Tests.DataAccess.Repositories
{
    public class OrdersRepositoryTest
    {
        /*private Customers customer = null;
        private Addresses address = null;
        private Pizzas pizza = null;

        public OrdersRepositoryTest()
        {
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_create").Options;
            using (var db = new Project1Context(options)) ;

            using (var db = new Project1Context(options))
            {
                CustomerRepository customerRepository = new CustomerRepository(db);
                PizzaRepository pizzaRepository = new PizzaRepository(db);
                IngredientRepository ingredientRepository = new IngredientRepository(db);

                address = new Addresses { Address1 = "Address 1", City = "City", State = "ST", Zipcode = 12345 };

                customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                customer.Addresses.Add(address);
                customerRepository.Save(customer);
                customerRepository.SaveChanges();

                Ingredients ingredient = new Ingredients() { Name = "Ingredient", Stock = 10 };
                ingredientRepository.Save(ingredient);
                ingredientRepository.SaveChanges();

                pizza = new Pizzas() { Name = "Pizza", Price = 20 };
                pizza.PizzasIngredients.Add(new PizzasIngredients() { IngredientId = ingredient.Id });
                pizzaRepository.Save(pizza);
                pizzaRepository.SaveChanges();
            }
        }

        [Fact]
        public void CreateOrdersWorks()
        {
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_create").Options;
            using (var db = new Project1Context(options)) ;
            Orders orderSaved = null;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                Orders order = new Orders { CustomerId = customer.Id, AddressId = address.Id, Value = 20, Date = DateTime.Now };
                order.OrderPizzas = new OrderPizzas[] { new OrderPizzas() {OrderId = order.Id, PizzaId = pizza.Id } };
                repo.Save(order);
                repo.SaveChanges();
                orderSaved = order;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                Orders order = repo.GetById(orderSaved.Id);

                Assert.Equal(orderSaved.CustomerId, order.CustomerId);
                Assert.Equal(orderSaved.AddressId, order.AddressId);
                Assert.Equal(orderSaved.Date, order.Date);
                Assert.Equal(orderSaved.Value, order.Value);
                Assert.Equal(1, order.OrderPizzas.Count());
                Assert.NotEqual(0, order.Id); // should get some generated ID
            }
        }

        [Fact]
        public void GetAllWorks()
        {
            List<Orders> list = new List<Orders>();

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_getAll").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);

                for (int i = 0; i < 5; i++)
                {
                    Orders order = new Orders { Name = $"Test {i}", Stock = 10 };
                    list.Add(order);
                    repo.Save(order);
                }
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                List<Orders> orders = (List<Orders>)repo.GetAll();

                Assert.Equal(list.Count, orders.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    Assert.Equal(list[i].Name, orders[i].Name);
                    Assert.Equal(10, orders[i].Stock);
                }
            }
        }

        [Fact]
        public void GetByIdWorks()
        {
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_getById").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);

                Orders order = new Orders { Name = "Test By Id", Stock = 10 };
                repo.Save(order);
                repo.SaveChanges();
                id = order.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                Orders order = (Orders)repo.GetById(id);

                Assert.NotEqual(0, order.Id);
                Assert.Equal("Test By Id", order.Name);
                Assert.Equal(10, order.Stock);
            }
        }



        [Fact]
        public void GetByIdThatDoesntExistReturnsNull()
        {
            int id = 100;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_getById").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            //using (var db = new Project1Context(options))
            //{
            //    var repo = new OrderRepository(db);

            //    Orders order = new Orders { Name = "Test By Id", Stock = 10 };
            //    repo.Save(order);
            //    repo.SaveChanges();
            //    id = order.Id;
            //}

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                Orders order = (Orders)repo.GetById(id);

                Assert.Null(order);
            }
        }

        [Fact]
        public void GetByNameWorks()
        {
            List<Orders> inserted = new List<Orders>();
            string name = "Test";
            string nameWrong = "Name 3";
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_getByName_List").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);

                Orders order = new Orders { Name = "Test By Name", Stock = 10 };
                repo.Save(order);
                inserted.Add(order);

                order = new Orders { Name = "Test By Name 2", Stock = 10 };
                repo.Save(order);
                inserted.Add(order);

                order = new Orders { Name = "Name 3", Stock = 10 };
                repo.Save(order);
                inserted.Add(order);
                
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                List<Orders> list = (List<Orders>)repo.GetByName(name);
                
                Assert.Equal(2, list.Count);

                foreach(Orders order in list)
                {
                    Assert.NotEqual(0, order.Id);
                    Assert.NotEqual(nameWrong, order.Name);
                    Assert.Contains(name, order.Name);
                    Assert.Equal(10, order.Stock);
                }
            }
        }



        [Fact]
        public void GetByNameThatDoesntExistsReturnsNull()
        {
            string name = "Not existing name";

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_getByName").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);

                Orders order = new Orders { Name = "Test By Name", Stock = 10 };
                repo.Save(order);
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                List<Orders> listOrders = (List<Orders>)repo.GetByName(name);

                Assert.Empty(listOrders);
            }
        }

        [Fact]
        public void DeleteWorks()
        {
            int id = 0;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);

                Orders order = new Orders { Name = "Test Delete", Stock = 10 };
                repo.Save(order);
                repo.SaveChanges();
                id = order.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                Orders order = (Orders)repo.GetById(id);

                Assert.NotEqual(0, order.Id);
                Assert.Equal("Test Delete", order.Name);
                Assert.Equal(10, order.Stock);
                
                repo.Delete(id);
                repo.SaveChanges();
                order = (Orders)repo.GetById(id);

                Assert.Null(order);
            }
        }
        
        [Fact]
        public void DeleteWithIdThatDoesntExistThrowsException()
        {
            int id = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            //using (var db = new Project1Context(options))
            //{
            //    var repo = new OrderRepository(db);

            //    Orders order = new Orders { Name = "Test Delete", Stock = 10 };
            //    repo.Save(order);
            //    repo.SaveChanges();
            //    id = order.Id;
            //}

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                Orders order = (Orders)repo.GetById(id);

                Assert.Null(order);

                Assert.Throws<ArgumentException>(() => repo.Delete(id));
                
            }
        }

        [Fact]
        public void UpdateWorks()
        {
            int id = 0;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_update").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);

                Orders order = new Orders { Name = "Test Update", Stock = 10 };
                repo.Save(order, id);
                repo.SaveChanges();
                id = order.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                Orders order = (Orders)repo.GetById(id);

                Assert.NotEqual(0, order.Id);
                Assert.Equal("Test Update", order.Name);
                Assert.Equal(10, order.Stock);

                order.Name = "Test Update 2";
                order.Stock = 20;

                repo.Save(order, id);
                repo.SaveChanges();

                order = (Orders)repo.GetById(id);

                Assert.NotEqual(0, order.Id);
                Assert.Equal(id, order.Id);
                Assert.Equal("Test Update 2", order.Name);
                Assert.Equal(20, order.Stock);
            }
        }
        
        [Fact]
        public void UpdateWithWorngIdShouldReturnException()
        {
            int id = 0;
            int idWrong = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_order_test_update").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);

                Orders order = new Orders { Name = "Test Update", Stock = 10 };
                repo.Save(order, id);
                repo.SaveChanges();
                id = order.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new OrderRepository(db);
                Orders order = (Orders)repo.GetById(id);
                Orders orderNull = (Orders)repo.GetById(idWrong);

                Assert.NotEqual(0, order.Id);
                Assert.Equal("Test Update", order.Name);
                Assert.Equal(10, order.Stock);

                Assert.Null(orderNull);

                order.Name = "Test Update 2";
                order.Stock = 20;

                
                Assert.Throws<ArgumentException>(() => repo.Save(order, idWrong));
            }
        }*/
    }
}
