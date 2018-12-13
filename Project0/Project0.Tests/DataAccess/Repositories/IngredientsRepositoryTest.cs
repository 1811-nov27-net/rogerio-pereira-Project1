using Project1.DataAccess;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using Project1.DataAccess.Repositories;
using System.Collections.Generic;

namespace Project1.Tests.DataAccess.Repositories
{
    public class IngredientsRepositoryTest
    {
        [Fact]
        public void CreateIngredientsWorks()
        {
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_create").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                Ingredients ingredient = new Ingredients { Name = "Test", Stock = 10 };
                repo.Save(ingredient);
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                Ingredients ingredient = db.Ingredients.First(m => m.Name == "Test");
                Assert.Equal("Test", ingredient.Name);
                Assert.Equal(10, ingredient.Stock);
                Assert.NotEqual(0, ingredient.Id); // should get some generated ID
            }
        }

        [Fact]
        public void GetAllWorks()
        {
            List<Ingredients> list = new List<Ingredients>();

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_getAll").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);

                for (int i = 0; i < 5; i++)
                {
                    Ingredients ingredient = new Ingredients { Name = $"Test {i}", Stock = 10 };
                    list.Add(ingredient);
                    repo.Save(ingredient);
                }
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                List<Ingredients> ingredients = (List<Ingredients>)repo.GetAll();

                Assert.Equal(list.Count, ingredients.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    Assert.Equal(list[i].Name, ingredients[i].Name);
                    Assert.Equal(10, ingredients[i].Stock);
                }
            }
        }

        [Fact]
        public void GetByIdWorks()
        {
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_getById").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients { Name = "Test By Id", Stock = 10 };
                repo.Save(ingredient);
                repo.SaveChanges();
                id = ingredient.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                Ingredients ingredient = (Ingredients)repo.GetById(id);

                Assert.NotEqual(0, ingredient.Id);
                Assert.Equal("Test By Id", ingredient.Name);
                Assert.Equal(10, ingredient.Stock);
            }
        }



        [Fact]
        public void GetByIdThatDoesntExistReturnsNull()
        {
            int id = 100;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_getById").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            /*using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients { Name = "Test By Id", Stock = 10 };
                repo.Save(ingredient);
                repo.SaveChanges();
                id = ingredient.Id;
            }*/

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                Ingredients ingredient = (Ingredients)repo.GetById(id);

                Assert.Null(ingredient);
            }
        }

        [Fact]
        public void GetByNameWorks()
        {
            List<Ingredients> inserted = new List<Ingredients>();
            string name = "Test";
            string nameWrong = "Name 3";
            int id = 0;

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_getByName_List").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients { Name = "Test By Name", Stock = 10 };
                repo.Save(ingredient);
                inserted.Add(ingredient);

                ingredient = new Ingredients { Name = "Test By Name 2", Stock = 10 };
                repo.Save(ingredient);
                inserted.Add(ingredient);

                ingredient = new Ingredients { Name = "Name 3", Stock = 10 };
                repo.Save(ingredient);
                inserted.Add(ingredient);
                
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                List<Ingredients> list = (List<Ingredients>)repo.GetByName(name);
                
                Assert.Equal(2, list.Count);

                foreach(Ingredients ingredient in list)
                {
                    Assert.NotEqual(0, ingredient.Id);
                    Assert.NotEqual(nameWrong, ingredient.Name);
                    Assert.Contains(name, ingredient.Name);
                    Assert.Equal(10, ingredient.Stock);
                }
            }
        }



        [Fact]
        public void GetByNameThatDoesntExistsReturnsNull()
        {
            string name = "Not existing name";

            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_getByName").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients { Name = "Test By Name", Stock = 10 };
                repo.Save(ingredient);
                repo.SaveChanges();
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                List<Ingredients> listIngredients = (List<Ingredients>)repo.GetByName(name);

                Assert.Empty(listIngredients);
            }
        }

        [Fact]
        public void DeleteWorks()
        {
            int id = 0;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients { Name = "Test Delete", Stock = 10 };
                repo.Save(ingredient);
                repo.SaveChanges();
                id = ingredient.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                Ingredients ingredient = (Ingredients)repo.GetById(id);

                Assert.NotEqual(0, ingredient.Id);
                Assert.Equal("Test Delete", ingredient.Name);
                Assert.Equal(10, ingredient.Stock);
                
                repo.Delete(id);
                repo.SaveChanges();
                ingredient = (Ingredients)repo.GetById(id);

                Assert.Null(ingredient);
            }
        }
        
        [Fact]
        public void DeleteWithIdThatDoesntExistThrowsException()
        {
            int id = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_delete").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            /*using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients { Name = "Test Delete", Stock = 10 };
                repo.Save(ingredient);
                repo.SaveChanges();
                id = ingredient.Id;
            }*/

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                Ingredients ingredient = (Ingredients)repo.GetById(id);

                Assert.Null(ingredient);

                Assert.Throws<ArgumentException>(() => repo.Delete(id));
                
            }
        }

        [Fact]
        public void UpdateWorks()
        {
            int id = 0;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_update").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients { Name = "Test Update", Stock = 10 };
                repo.Save(ingredient, id);
                repo.SaveChanges();
                id = ingredient.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                Ingredients ingredient = (Ingredients)repo.GetById(id);

                Assert.NotEqual(0, ingredient.Id);
                Assert.Equal("Test Update", ingredient.Name);
                Assert.Equal(10, ingredient.Stock);

                ingredient.Name = "Test Update 2";
                ingredient.Stock = 20;

                repo.Save(ingredient, id);
                repo.SaveChanges();

                ingredient = (Ingredients)repo.GetById(id);

                Assert.NotEqual(0, ingredient.Id);
                Assert.Equal(id, ingredient.Id);
                Assert.Equal("Test Update 2", ingredient.Name);
                Assert.Equal(20, ingredient.Stock);
            }
        }
        
        [Fact]
        public void UpdateWithWorngIdShouldReturnException()
        {
            int id = 0;
            int idWrong = 1000;
            // arrange (use the context directly - we assume that works)
            var options = new DbContextOptionsBuilder<Project1Context>()
                .UseInMemoryDatabase("db__ingredient_test_update").Options;
            using (var db = new Project1Context(options)) ;

            // act (for act, only use the repo, to test it)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);

                Ingredients ingredient = new Ingredients { Name = "Test Update", Stock = 10 };
                repo.Save(ingredient, id);
                repo.SaveChanges();
                id = ingredient.Id;
            }

            // assert (for assert, once again use the context directly for verify.)
            using (var db = new Project1Context(options))
            {
                var repo = new IngredientRepository(db);
                Ingredients ingredient = (Ingredients)repo.GetById(id);
                Ingredients ingredientNull = (Ingredients)repo.GetById(idWrong);

                Assert.NotEqual(0, ingredient.Id);
                Assert.Equal("Test Update", ingredient.Name);
                Assert.Equal(10, ingredient.Stock);

                Assert.Null(ingredientNull);

                ingredient.Name = "Test Update 2";
                ingredient.Stock = 20;

                
                Assert.Throws<ArgumentException>(() => repo.Save(ingredient, idWrong));
            }
        }
    }
}
