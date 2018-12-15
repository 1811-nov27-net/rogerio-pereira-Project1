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

namespace Project1.WebUi.Controllers
{
    public class IngredientsController : Controller
    {
        private IIngredientRepository Repository { get; set; }

        public IngredientsController(IIngredientRepository repository)
        {
            Repository = repository;
        }

        // GET: Ingredient
        public ActionResult Index()
        {
            return View(Mapper.Map< IEnumerable<Ingredients>, IEnumerable<Ingredient>>((IEnumerable<Ingredients>)Repository.GetAll()));
            //return View(Repository.GetAll());
        }

        // GET: Ingredient/Details/5
        public ActionResult Details(int id)
        {
            Ingredient ingredient = Mapper.Map<Ingredients, Ingredient>(Repository.GetById(id));
            if (ingredient != null)
            {
                return View(ingredient);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Ingredient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ingredient ingredient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.Save(Mapper.Map<Ingredient, Ingredients>(ingredient));
                    Repository.SaveChanges();
                }
                else
                {
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Stock", ex.Message);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredient/Edit/5
        public ActionResult Edit(int id)
        {
            Ingredient ingredient = Mapper.Map<Ingredients, Ingredient> (Repository.GetById(id));
            if (ingredient != null)
            {
                return View(ingredient);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Ingredient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Ingredient ingredient)
        {
            try
            {
                if (id != ingredient.Id)
                {
                    ModelState.AddModelError("id", "should match the route id");
                    return View();
                }

                if (ModelState.IsValid)
                {
                    Repository.Save(Mapper.Map<Ingredient, Ingredients>(ingredient), id);
                    Repository.SaveChanges();
                }
                else
                {
                    // get a new Edit page, but with the current ModelState errors.
                    return View();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Ingredient/Delete/5
        public ActionResult Delete(int id)
        {
            var ingredient = Mapper.Map<Ingredients, Ingredient>(Repository.GetById(id));
            if (ingredient != null)
            {
                return View(ingredient);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Ingredient/Delete/5
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
            catch(Exception e)
            {
                throw e;
            }
        }

        // POST: Ingredient/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(Search search)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Ingredient> ingredients = new List<Ingredient>();

                    if (search.searchText == "" || search.searchText == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    if (search.searchBy == "ID")
                    {
                        int id = int.Parse(search.searchText);

                        Ingredient ingredient = Mapper.Map<Ingredients, Ingredient>(Repository.GetById(id));
                        ingredients.Add(ingredient);
                    }
                    else if (search.searchBy == "Name")
                    {
                        ingredients = Mapper.Map<List<Ingredients>, List<Ingredient>>((List<Ingredients>)Repository.GetByName(search.searchText));
                    }

                    return View("Index", ingredients);
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