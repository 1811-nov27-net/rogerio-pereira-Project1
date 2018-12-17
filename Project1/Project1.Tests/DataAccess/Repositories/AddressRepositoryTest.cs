using Project1.DataAccess;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using Project1.DataAccess.Repositories;
using System.Collections.Generic;

namespace Project1.Tests.DataAccess.Repositories
{
    public class AddressRepositoryTest
    {
        [Fact]
        public void CreateAddressesWorks()
        {
            Customers customerSaved = null;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_create").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);

                //Create customer
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                repo.SaveChanges();

                Addresses address = new Addresses
                {
                    CustomerId = customer.Id,
                    Address1 = "Address 1",
                    Address2 = "Address 2",
                    City = "City",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(address);
                repo.SaveChanges();
                customerSaved = customer;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                Addresses Address = db.Addresses.First(m => m.Address1 == "Address 1");
                Assert.Equal(customerSaved.Id, Address.CustomerId);
                Assert.Equal("Address 1", Address.Address1);
                Assert.Equal("Address 2", Address.Address2);
                Assert.Equal("City", Address.City);
                Assert.Equal("ST", Address.State);
                Assert.Equal(12345, Address.Zipcode);
                Assert.NotEqual(0, Address.Id); // should get some generated ID
            }
        }

        //public void CreateAddressesWithWrongCustomerIdDoesntWorks()
        //{

        //}

        [Fact]
        public void GetAllWorks()
        {
            List<Addresses> list = new List<Addresses>();
            Customers customerSaved = null;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_getAll").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var customerRepo = new CustomerRepository(db);
                var repo = new AddressRepository(db);

                //Create customer
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                customerRepo.Save(customer);
                customerRepo.SaveChanges();

                for (int i = 0; i < 5; i++)
                {
                    Addresses address = new Addresses
                    {
                        CustomerId = customer.Id,
                        Address1 = $"Address 1 {i}",
                        Address2 = $"Address 2 {i}",
                        City = $"City {i}",
                        State = "ST",
                        Zipcode = 12345
                    };
                    list.Add(address);
                    repo.Save(address);
                }
                repo.SaveChanges();
                customerSaved = customer;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);
                List<Addresses> Addresses = (List<Addresses>)repo.GetAll();

                Assert.Equal(list.Count, Addresses.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    Assert.Equal(list[i].CustomerId, customerSaved.Id);
                    Assert.Equal(list[i].Address1, Addresses[i].Address1);
                    Assert.Equal(list[i].Address2, Addresses[i].Address2);
                    Assert.Equal(list[i].City, Addresses[i].City);
                    Assert.Equal(list[i].State, Addresses[i].State);
                    Assert.Equal(list[i].Zipcode, Addresses[i].Zipcode);
                }
            }
        }

        [Fact]
        public void GetByIdWorks()
        {
            int id = 0;
            Customers customerSaved = null;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_getById").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var customerRepo = new CustomerRepository(db);
                var repo = new AddressRepository(db);

                //Create customer
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                customerRepo.Save(customer);
                customerRepo.SaveChanges();

                Addresses address = new Addresses
                {
                    CustomerId = customer.Id,
                    Address1 = "Address 1 Test By Id",
                    Address2 = "Address 2 Test By Id",
                    City = "City Test By Id",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(address);
                repo.SaveChanges();
                customerSaved = customer;
                id = address.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses Address = (Addresses)repo.GetById(id);

                Assert.NotEqual(0, Address.Id);
                Assert.Equal(customerSaved.Id, Address.CustomerId);
                Assert.Equal("Address 1 Test By Id", Address.Address1);
                Assert.Equal("Address 2 Test By Id", Address.Address2);
                Assert.Equal("City Test By Id", Address.City);
                Assert.Equal("ST", Address.State);
                Assert.Equal(12345, Address.Zipcode);
            }
        }



        [Fact]
        public void GetByIdThatDoesntExistReturnsNull()
        {
            int id = 100;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_getById").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            //using (var db = new Project1Context(options))
            //{
            //var repo = new AddressRepository(db);

            //Addresses Address = new Addresses { Name = "Test By Id", Stock = 10 };
            //repo.Save(Address);
            //repo.SaveChanges();
            //id = Address.Id;
            //}

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses Address = (Addresses)repo.GetById(id);

                Assert.Null(Address);
            }
        }

        [Fact]
        public void GetByNameWorks()
        {
            List<Addresses> inserted = new List<Addresses>();
            string rightString = "Test";
            string wrongString = "Name 3";
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_getByName_List").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var customerRepo = new CustomerRepository(db);
                var repo = new AddressRepository(db);

                //Create customer
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                customerRepo.Save(customer);
                customerRepo.SaveChanges();

                Addresses Address = new Addresses {
                    CustomerId = customer.Id,
                    Address1 = "Test Address 1",
                    City = "City",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(Address);
                inserted.Add(Address);

                Address = new Addresses {
                    CustomerId = customer.Id,
                    Address1 = "Test Address 1",
                    City = "City",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(Address);
                inserted.Add(Address);

                Address = new Addresses {
                    CustomerId = customer.Id,
                    Address1 = "Name 3 Address 1",
                    City = "City",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(Address);
                inserted.Add(Address);

                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);
                List<Addresses> list = (List<Addresses>)repo.GetByName(rightString);

                Assert.Equal(2, list.Count);

                foreach (Addresses Address in list)
                {
                    Assert.NotEqual(0, Address.Id);
                    Assert.DoesNotContain(wrongString, Address.Address1);
                    Assert.Contains(rightString, Address.Address1);
                    Assert.Equal("City", Address.City);
                    Assert.Equal("ST", Address.State);
                    Assert.Equal(12345, Address.Zipcode);
                }
            }
        }

        [Fact]
        public void GetByNameThatDoesntExistsReturnsNull()
        {
            List<Addresses> inserted = new List<Addresses>();
            string name = "Not existing name";

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_getByName_List_Wrong").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var customerRepo = new CustomerRepository(db);
                var repo = new AddressRepository(db);

                //Create customer
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                customerRepo.Save(customer);
                customerRepo.SaveChanges();

                Addresses Address = new Addresses
                {
                    CustomerId = customer.Id,
                    Address1 = "Test Address 1",
                    City = "City",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(Address);
                inserted.Add(Address);

                Address = new Addresses
                {
                    CustomerId = customer.Id,
                    Address1 = "Test Address 1",
                    City = "City",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(Address);
                inserted.Add(Address);

                Address = new Addresses
                {
                    CustomerId = customer.Id,
                    Address1 = "Name 3 Address 1",
                    City = "City",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(Address);
                inserted.Add(Address);

                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);
                List<Addresses> list = (List<Addresses>)repo.GetByName(name);

                Assert.Empty(list);
            }
        }

        [Fact]
        public void DeleteWorks()
        {
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var customerRepo = new CustomerRepository(db);
                var repo = new AddressRepository(db);

                //Create customer
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                customerRepo.Save(customer);
                customerRepo.SaveChanges();

                Addresses address = new Addresses
                {
                    CustomerId = customer.Id,
                    Address1 = "Ad1 Test Delete",
                    City = "City Delete",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(address);
                customerRepo.SaveChanges();
                id = address.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses address = (Addresses)repo.GetById(id);

                Assert.Equal(id, address.Id);
                Assert.Equal("Ad1 Test Delete", address.Address1);
                Assert.Equal("City Delete", address.City);
                Assert.Equal("ST", address.State);
                Assert.Equal(12345, address.Zipcode);

                repo.Delete(id);
                repo.SaveChanges();
                address = (Addresses)repo.GetById(id);

                Assert.Null(address);
            }
        }

        [Fact]
        public void DeleteWithIdThatDoesntExistThrowsException()
        {
            int id = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            //using (var db = new Project1Context(options))
            //{
                //var repo = new AddressRepository(db);

                //Addresses Address = new Addresses { Name = "Test Delete", Stock = 10 };
                //repo.Save(Address);
                //repo.SaveChanges();
                //id = Address.Id;
            //}

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses Address = (Addresses)repo.GetById(id);

                Assert.Null(Address);

                Assert.Throws<ArgumentException>(() => repo.Delete(id));
                
            }
        }

        [Fact]
        public void UpdateWorks()
        {
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var customerRepo = new CustomerRepository(db);
                var repo = new AddressRepository(db);

                //Create customer
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                customerRepo.Save(customer);
                customerRepo.SaveChanges();

                Addresses address = new Addresses
                {
                    CustomerId = customer.Id,
                    Address1 = "Ad1 Test",
                    City = "City",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(address);
                customerRepo.SaveChanges();
                id = address.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses address = (Addresses)repo.GetById(id);

                Assert.Equal(id, address.Id);
                Assert.Equal("Ad1 Test", address.Address1);
                Assert.Equal("City", address.City);
                Assert.Equal("ST", address.State);
                Assert.Equal(12345, address.Zipcode);

                address.Address1 = "Ad1 Test alt";
                address.City = "City alt";
                address.State = "AL";
                address.Zipcode = 98765;

                repo.Save(address, id);
                repo.SaveChanges();

                address = (Addresses)repo.GetById(id);

                Assert.Equal(id, address.Id);
                Assert.Equal("Ad1 Test alt", address.Address1);
                Assert.Equal("City alt", address.City);
                Assert.Equal("AL", address.State);
                Assert.Equal(98765, address.Zipcode);
            }
        }
        
        [Fact]
        public void UpdateWithWorngIdShouldReturnException()
        {
            int id = 0;
            int idWrong = 1000;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db_address_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var customerRepo = new CustomerRepository(db);
                var repo = new AddressRepository(db);

                //Create customer
                Customers customer = new Customers { FirstName = "First Name", LastName = "Last Name" };
                customerRepo.Save(customer);
                customerRepo.SaveChanges();

                Addresses address = new Addresses
                {
                    CustomerId = customer.Id,
                    Address1 = "Ad1 Test",
                    City = "City",
                    State = "ST",
                    Zipcode = 12345
                };
                repo.Save(address);
                customerRepo.SaveChanges();
                id = address.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses address = (Addresses)repo.GetById(id);

                Assert.Equal(id, address.Id);
                Assert.Equal("Ad1 Test", address.Address1);
                Assert.Equal("City", address.City);
                Assert.Equal("ST", address.State);
                Assert.Equal(12345, address.Zipcode);

                address.Address1 = "Ad1 Test alt";
                address.City = "City alt";
                address.State = "AL";
                address.Zipcode = 98765;

                Assert.Throws<ArgumentException>(() => repo.Save(address, idWrong));
            }
        }
    }
}