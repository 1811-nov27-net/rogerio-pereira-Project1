using Project1.DataAccess;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using Project1.DataAccess.Repositories;
using System.Collections.Generic;

namespace Project0.Tests.DataAccess.Repositories
{
    public class AddressRepositoryTest
    {
        /*[Fact]
        public void CreateAddressesWorks()
        {
            Customers customerSaved = null;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_create").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
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
            using (var db = new Project0Context(options))
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

        public void CreateAddressesWithWrongCustomerIdDoesntWorks()
        {

        }

        [Fact]
        public void GetAllWorks()
        {
            List<Addresses> list = new List<Addresses>();

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_getAll").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);

                for (int i = 0; i < 5; i++)
                {
                    Addresses address = new Addresses
                    {
                        CustomerId = customer.Id,
                        Address1 = "Address 1",
                        Address2 = "Address 2",
                        City = "City",
                        State = "ST",
                        Zipcode = 12345
                    };
                    list.Add(Address);
                    repo.Save(Address);
                }
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);
                List<Addresses> Addresses = (List<Addresses>)repo.GetAll();

                Assert.Equal(list.Count, Addresses.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    Assert.Equal(list[i].Name, Addresses[i].Name);
                    Assert.Equal(10, Addresses[i].Stock);
                }
            }
        }

        [Fact]
        public void GetByIdWorks()
        {
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_getById").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);

                Addresses Address = new Addresses { Name = "Test By Id", Stock = 10 };
                repo.Save(Address);
                repo.SaveChanges();
                id = Address.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses Address = (Addresses)repo.GetById(id);

                Assert.NotEqual(0, Address.Id);
                Assert.Equal("Test By Id", Address.Name);
                Assert.Equal(10, Address.Stock);
            }
        }



        [Fact]
        public void GetByIdThatDoesntExistReturnsNull()
        {
            int id = 100;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_getById").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            //using (var db = new Project0Context(options))
            //{
                //var repo = new AddressRepository(db);

                //Addresses Address = new Addresses { Name = "Test By Id", Stock = 10 };
                //repo.Save(Address);
                //repo.SaveChanges();
                //id = Address.Id;
            //}

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
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
            string name = "Test";
            string nameWrong = "Name 3";
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_getByName_List").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);

                Addresses Address = new Addresses { Name = "Test By Name", Stock = 10 };
                repo.Save(Address);
                inserted.Add(Address);

                Address = new Addresses { Name = "Test By Name 2", Stock = 10 };
                repo.Save(Address);
                inserted.Add(Address);

                Address = new Addresses { Name = "Name 3", Stock = 10 };
                repo.Save(Address);
                inserted.Add(Address);
                
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);
                List<Addresses> list = (List<Addresses>)repo.GetByName(name);
                
                Assert.Equal(2, list.Count);

                foreach(Addresses Address in list)
                {
                    Assert.NotEqual(0, Address.Id);
                    Assert.NotEqual(nameWrong, Address.Name);
                    Assert.Contains(name, Address.Name);
                    Assert.Equal(10, Address.Stock);
                }
            }
        }



        [Fact]
        public void GetByNameThatDoesntExistsReturnsNull()
        {
            string name = "Not existing name";

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_getByName").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);

                Addresses Address = new Addresses { Name = "Test By Name", Stock = 10 };
                repo.Save(Address);
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);
                List<Addresses> listAddresses = (List<Addresses>)repo.GetByName(name);

                Assert.Empty(listAddresses);
            }
        }

        [Fact]
        public void DeleteWorks()
        {
            int id = 0;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_delete").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);

                Addresses Address = new Addresses { Name = "Test Delete", Stock = 10 };
                repo.Save(Address);
                repo.SaveChanges();
                id = Address.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses Address = (Addresses)repo.GetById(id);

                Assert.NotEqual(0, Address.Id);
                Assert.Equal("Test Delete", Address.Name);
                Assert.Equal(10, Address.Stock);
                
                repo.Delete(id);
                repo.SaveChanges();
                Address = (Addresses)repo.GetById(id);

                Assert.Null(Address);
            }
        }
        
        [Fact]
        public void DeleteWithIdThatDoesntExistThrowsException()
        {
            int id = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_delete").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            //using (var db = new Project0Context(options))
            //{
                //var repo = new AddressRepository(db);

                //Addresses Address = new Addresses { Name = "Test Delete", Stock = 10 };
                //repo.Save(Address);
                //repo.SaveChanges();
                //id = Address.Id;
            //}

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
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
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_update").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);

                Addresses Address = new Addresses { Name = "Test Update", Stock = 10 };
                repo.Save(Address, id);
                repo.SaveChanges();
                id = Address.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses Address = (Addresses)repo.GetById(id);

                Assert.NotEqual(0, Address.Id);
                Assert.Equal("Test Update", Address.Name);
                Assert.Equal(10, Address.Stock);

                Address.Name = "Test Update 2";
                Address.Stock = 20;

                repo.Save(Address, id);
                repo.SaveChanges();

                Address = (Addresses)repo.GetById(id);

                Assert.NotEqual(0, Address.Id);
                Assert.Equal(id, Address.Id);
                Assert.Equal("Test Update 2", Address.Name);
                Assert.Equal(20, Address.Stock);
            }
        }
        
        [Fact]
        public void UpdateWithWorngIdShouldReturnException()
        {
            int id = 0;
            int idWrong = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project0Context>()
                .UseInMemoryDatabase("db_address_test_update").Options;
            using (var db = new Project0Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);

                Addresses Address = new Addresses { Name = "Test Update", Stock = 10 };
                repo.Save(Address, id);
                repo.SaveChanges();
                id = Address.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project0Context(options))
            {
                var repo = new AddressRepository(db);
                Addresses Address = (Addresses)repo.GetById(id);
                Addresses AddressNull = (Addresses)repo.GetById(idWrong);

                Assert.NotEqual(0, Address.Id);
                Assert.Equal("Test Update", Address.Name);
                Assert.Equal(10, Address.Stock);

                Assert.Null(AddressNull);

                Address.Name = "Test Update 2";
                Address.Stock = 20;

                
                Assert.Throws<ArgumentException>(() => repo.Save(Address, idWrong));
            }
        }*/
    }
}
