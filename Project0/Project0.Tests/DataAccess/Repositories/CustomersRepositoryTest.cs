using Project0.DataAccess;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using Project0.DataAccess.Repositories;
using System.Collections.Generic;

namespace Project0.Tests.DataAccess.Repositories
{
    public class CustomersRepositoryTest
    {
        [Fact]
        public void CreateCustomersWorks()
        {
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_create").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);

                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                customer.Addresses.Add(new Addresses { CustomerId = customer.Id, Address1 = "Address 1", City = "City", State = "ST", Zipcode = 12345});
                repo.Save(customer);
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                Customers customer = db.Customers.Include(m => m.Addresses).First(m => m.FirstName == "First Name" && m.LastName == "Last Name");
                List<Addresses> listAddress = customer.Addresses.ToList();

                Assert.Equal("First Name", customer.FirstName);
                Assert.Equal("Last Name", customer.LastName);
                Assert.NotEqual(0, customer.Id); // should get some generated ID

                //Address
                Assert.Equal(1, listAddress.Count());
                Assert.Equal(customer.Id, listAddress[0].CustomerId);
                Assert.Equal("Address 1", listAddress[0].Address1);
                Assert.Null(listAddress[0].Address2);
                Assert.Equal("City", listAddress[0].City);
                Assert.Equal("ST", listAddress[0].State);
                Assert.Equal(12345, listAddress[0].Zipcode);
                Assert.NotEqual(0, listAddress[0].Id);
            }
        }

        [Fact]
        public void GetAllCustomers()
        {
            List<Customers> list = new List<Customers>();

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_getAll").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);

                for (int i = 0; i < 5; i++)
                {
                    Customers customer = new Customers { FirstName = $"First Name {i}", LastName = $"Last Name {i}" };
                    list.Add(customer);
                    repo.Save(customer);
                }
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                List<Customers> customers = (List<Customers>)repo.GetAll();

                Assert.Equal(list.Count, customers.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    Assert.Equal(list[i].FirstName, customers[i].FirstName);
                    Assert.Equal(list[i].LastName, customers[i].LastName);
                    Assert.NotEqual(0, customers[i].Id); // should get some generated ID
                }
            }
        }

        [Fact]
        public void GetAllAddress()
        {
            List<Addresses> listAddresses = new List<Addresses>();

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_create_address").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };

                Addresses address = new Addresses { CustomerId = customer.Id, Address1 = "Address 1", City = "City 1", State = "S1", Zipcode = 12345 };
                customer.Addresses.Add(address);
                listAddresses.Add(address);

                address = new Addresses { CustomerId = customer.Id, Address1 = "Address 2", City = "City 2", State = "S2", Zipcode = 12345 };
                customer.Addresses.Add(address);
                listAddresses.Add(address);

                address = new Addresses { CustomerId = customer.Id, Address1 = "Address 3", City = "City 3", State = "S3", Zipcode = 12345 };
                customer.Addresses.Add(address);
                listAddresses.Add(address);

                repo.Save(customer);
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                Customers customer = db.Customers.Include(m => m.Addresses).First(m => m.FirstName == "First Name" && m.LastName == "Last Name");
                List<Addresses> addresses = customer.Addresses.ToList();

                Assert.Equal(listAddresses.Count, addresses.Count);

                for (int i = 0; i < addresses.Count; i++)
                {
                    Assert.NotEqual(0, addresses[i].Id);
                    Assert.Equal(customer.Id, addresses[i].CustomerId);
                    Assert.Equal(listAddresses[i].Address1, addresses[i].Address1);
                    Assert.Equal(listAddresses[i].Address2, addresses[i].Address2);
                    Assert.Equal(listAddresses[i].City, addresses[i].City);
                    Assert.Equal(listAddresses[i].State, addresses[i].State);
                    Assert.Equal(listAddresses[i].Zipcode, addresses[i].Zipcode);
                }
            }
        }

        [Fact]
        public void GetAllAddressWithNoAddressShouldReturnNull()
        {
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_create_address_2").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                
                repo.Save(customer);
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                Customers customer = db.Customers.Include(m => m.Addresses).First(m => m.FirstName == "First Name" && m.LastName == "Last Name");
                List<Addresses> addresses = customer.Addresses.ToList();

                Assert.Empty(addresses);
            }
        }

        [Fact]
        public void GetByIdWorks()
        {
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_getById").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);

                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                repo.Save(customer);
                repo.SaveChanges();
                id = customer.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                Customers customer = (Customers)repo.GetById(id);

                Assert.Equal("First Name", customer.FirstName);
                Assert.Equal("Last Name", customer.LastName);
                Assert.NotEqual(0, customer.Id); // should get some generated ID
            }
        }

        [Fact]
        public void GetByIdThatDoesntExistReturnsNull()
        {
            int id = 100;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_getById").Options;
            using (var db = new Project0Context(options)) ;

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                Customers customer = (Customers)repo.GetById(id);

                Assert.Null(customer);
            }
        }
        
        [Fact]
        public void GetByNameWorks()
        {
            List<Customers> inserted = new List<Customers>();
            string name = "Test";
            string nameWrong = "Name";
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_getByName_List").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);

                Customers customer = new Customers { FirstName = "First Test", LastName = "Last" };
                repo.Save(customer);
                inserted.Add(customer);

                customer = new Customers { FirstName = "First", LastName = "Last Test" };
                repo.Save(customer);
                inserted.Add(customer);

                customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                repo.Save(customer);
                inserted.Add(customer);

                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                List<Customers> list = (List<Customers>)repo.GetByName(name);

                Assert.Equal(2, list.Count);

                foreach (Customers customer in list)
                {
                    Assert.NotEqual(0, customer.Id);
                    Assert.DoesNotContain(nameWrong, customer.FirstName);
                    Assert.DoesNotContain(nameWrong, customer.LastName);

                    //Assert if the string searched is contained in FirstName OR LastName
                    bool contains = customer.FirstName.Contains(name) || customer.LastName.Contains(name);
                    Assert.True(contains);
                }
            }
        }

        [Fact]
        public void GetByNameThatDoesntExistsReturnsNull()
        {
            List<Customers> inserted = new List<Customers>();
            string name = "existing";

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_getByName").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);

                Customers customer = new Customers { FirstName = "First Test", LastName = "Last" };
                repo.Save(customer);
                inserted.Add(customer);

                customer = new Customers { FirstName = "First", LastName = "Last Test" };
                repo.Save(customer);
                inserted.Add(customer);

                customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                repo.Save(customer);
                inserted.Add(customer);

                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                List<Customers> list = (List<Customers>)repo.GetByName(name);

                Assert.Empty(list);
            }
        }
        
        [Fact]
        public void DeleteWorks()
        {
            int id = 0;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_delete").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);

                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                repo.Save(customer);

                repo.SaveChanges();
                id = customer.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                Customers customer = (Customers)repo.GetById(id);

                Assert.Equal("First Name", customer.FirstName);
                Assert.Equal("Last Name", customer.LastName);
                Assert.NotEqual(0, customer.Id); // should get some generated ID

                repo.Delete(id);
                repo.SaveChanges();
                customer = (Customers)repo.GetById(id);

                Assert.Null(customer);
            }
        }

        [Fact]
        public void DeleteWithIdThatDoesntExistThrowsException()
        {
            int id = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_delete").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                Customers customer = (Customers)repo.GetById(id);

                Assert.Null(customer);

                Assert.Throws<ArgumentException>(() => repo.Delete(id));

            }
        }
        
        [Fact]
        public void UpdateWorks()
        {
            int id = 0;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_update").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);

                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                repo.Save(customer);
                repo.SaveChanges();
                id = customer.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                Customers customer = (Customers)repo.GetById(id);

                Assert.Equal("First Name", customer.FirstName);
                Assert.Equal("Last Name", customer.LastName);
                Assert.NotEqual(0, customer.Id); // should get some generated ID

                customer.FirstName = "FN Update 2";
                customer.LastName = "LN Update 2";

                repo.Save(customer, id);
                repo.SaveChanges();

                customer = (Customers)repo.GetById(id);

                Assert.NotEqual(0, customer.Id);
                Assert.Equal(id, customer.Id);
                Assert.Equal("FN Update 2", customer.FirstName);
                Assert.Equal("LN Update 2", customer.LastName);
            }
        }
        
        [Fact]
        public void UpdateWithWorngIdShouldReturnException()
        {
            int id = 0;
            int idWrong = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_customer_test_update").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);

                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                repo.Save(customer);
                repo.SaveChanges();
                id = customer.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new CustomerRepository(db);
                Customers customer = (Customers)repo.GetById(id);
                Customers customerNull = (Customers)repo.GetById(idWrong);

                Assert.Equal("First Name", customer.FirstName);
                Assert.Equal("Last Name", customer.LastName);
                Assert.NotEqual(0, customer.Id); // should get some generated ID

                Assert.Null(customerNull);

                customer.FirstName = "FN Update 2";
                customer.LastName = "LN Update 2";
                
                Assert.Throws<ArgumentException>(() => repo.Save(customer, idWrong));
            }
        }
    }
}
