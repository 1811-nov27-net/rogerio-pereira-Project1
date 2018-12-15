using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.WebUi.Models;
using Project1.DataAccess;
using Project1.DataAccess.Repositories.Interfaces;
using Project1.WebUi.Models.ViewModels;
using Project1.WebUi.Controllers.Exceptions;

namespace Project1.WebUi.Controllers
{
    public class PizzasController : Controller
    {
        private IPizzaRepository Repository { get; set; }
        private IIngredientRepository IngredientsRepository { get; set; }

        public PizzasController(IPizzaRepository repository, IIngredientRepository ingredientsRepository)
        {
            Repository = repository;
            IngredientsRepository = ingredientsRepository;
        }

        // GET: Pizza
        public ActionResult Index()
        {
            return View(Mapper.Map<IEnumerable<Pizzas>, IEnumerable<Pizza>>((IEnumerable<Pizzas>)Repository.GetAll()));
            //return View(Repository.GetAll());
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = Mapper.Map<Pizzas, Pizza>(Repository.GetById(id));
            if (pizza != null)
            {
                return View(pizza);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            IEnumerable<Ingredient> ingredients = Mapper.Map<IEnumerable<Ingredients>, IEnumerable<Ingredient>>((IEnumerable<Ingredients>)IngredientsRepository.GetAll());
            PizzaForm pizzaViewModel = new PizzaForm(ingredients);
            return View(pizzaViewModel);
        }

        // POST: Pizza/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        //public ActionResult Create(PizzaForm p)
        //public ActionResult Create(Pizza pizza)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                Pizza pizza = new Pizza()
                {
                    //Id = int.Parse(collection["Id"]),
                    Name = collection["Name"],
                    Price = decimal.Parse(collection["Price"])
                };

                //Convert all ingredientsId to int
                List<int> ingredientsIdList = new List<int>();
                foreach (string ingredientsIdString in collection["ingredientsId"])
                {
                    ingredientsIdList.Add(int.Parse(ingredientsIdString));
                }

                //Add all ingredients to pizza
                foreach (int ingredientId in ingredientsIdList)
                {
                    Ingredient ingredient = Mapper.Map<Ingredients, Ingredient>(IngredientsRepository.GetById(ingredientId));
                    pizza.addIngredients(ingredient);
                }

                Repository.Save(Mapper.Map<Pizza, Pizzas>(pizza));
                Repository.SaveChanges();
                //}
                //else
                //{
                //    return View();
                //}

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                //ModelState.AddModelError("Stock", ex.Message);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            var pizza = Mapper.Map<Pizzas, Pizza>(Repository.GetById(id));
            PizzaForm pizzaViewModel;
            IEnumerable<Ingredient> ingredients = Mapper.Map<IEnumerable<Ingredients>, IEnumerable<Ingredient>>((IEnumerable<Ingredients>)IngredientsRepository.GetAll());

            if (pizza != null)
            {
                pizzaViewModel = new PizzaForm(pizza, ingredients);
                return View(pizzaViewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {

                //if (ModelState.IsValid)
                //{
                Pizza pizza = Mapper.Map<Pizzas, Pizza>(Repository.GetById(id));

                if (id != pizza.Id)
                {
                    ModelState.AddModelError("id", "should match the route id");
                    return View();
                }

                pizza.Name = collection["Name"];
                pizza.Price = decimal.Parse(collection["Price"]);
                pizza.PizzasIngredients = new List<PizzaIngredient>();

                //Convert all ingredientsId to int
                List<int> ingredientsIdList = new List<int>();
                foreach (string ingredientsIdString in collection["ingredientsId"])
                {
                    ingredientsIdList.Add(int.Parse(ingredientsIdString));
                }

                //Add all ingredients to pizza
                foreach (int ingredientId in ingredientsIdList)
                {
                    Ingredient ingredient = Mapper.Map<Ingredients, Ingredient>(IngredientsRepository.GetById(ingredientId));
                    pizza.addIngredients(ingredient);
                }

                Pizzas pizzaDataAccess = Mapper.Map<Pizza, Pizzas>(pizza);
                pizzaDataAccess.PizzasIngredients = Mapper.Map<ICollection<PizzaIngredient>, ICollection<PizzasIngredients>>(pizza.PizzasIngredients);

                Repository.Save(pizzaDataAccess, id);
                Repository.SaveChanges();
                //}
                //else
                //{
                //    // get a new Edit page, but with the current ModelState errors.
                //    return View();
                //}

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            var pizza = Mapper.Map<Pizzas, Pizza>(Repository.GetById(id));
            if (pizza != null)
            {
                return View(pizza);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Repository.Delete(id);
                Repository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // GET: Pizzas/CheckPizzaStock/5/2
        public bool CheckPizzaStock(int pizzaId, int quantity)
        { 
            try
            {
                var pizza = Mapper.Map<Pizzas, Pizza>(Repository.GetById(pizzaId));
                if (pizza != null)
                {
                    return pizza.checkStock(quantity);
                }
            }
            catch(MinimumStockException ex)
            {
                return false;
            }

            return false;
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(Search search)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Pizza> pizzas = new List<Pizza>();

                    if (search.searchText == "" || search.searchText == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    if (search.searchBy == "ID")
                    {
                        int id = int.Parse(search.searchText);

                        Pizza pizza = Mapper.Map<Pizzas, Pizza>(Repository.GetById(id));
                        pizzas.Add(pizza);
                    }
                    else if (search.searchBy == "Name")
                    {
                        pizzas = Mapper.Map<List<Pizzas>, List<Pizza>>((List<Pizzas>)Repository.GetByName(search.searchText));
                    }

                    return View("Index", pizzas);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}