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

namespace Project1.WebUi.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderRepository Repository { get; set; }

        public OrdersController(IOrderRepository repository)
        {
            Repository = repository;
        }

        // GET: Order
        public ActionResult Index()
        {
            return View(Mapper.Map< IEnumerable<Orders>, IEnumerable<Order>>((IEnumerable<Orders>)Repository.GetAll()));
            //return View(Repository.GetAll());
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            Order order = Mapper.Map<Orders, Order>(Repository.GetById(id));
            if (order != null)
            {
                return View(order);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.Save(Mapper.Map<Order, Orders>(order));
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

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            Order order = Mapper.Map<Orders, Order> (Repository.GetById(id));
            if (order != null)
            {
                return View(order);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                if (id != order.Id)
                {
                    ModelState.AddModelError("id", "should match the route id");
                    return View();
                }

                if (ModelState.IsValid)
                {
                    Repository.Save(Mapper.Map<Order, Orders>(order), id);
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

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            var order = Mapper.Map<Orders, Order>(Repository.GetById(id));
            if (order != null)
            {
                return View(order);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Order/Delete/5
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
    }
}