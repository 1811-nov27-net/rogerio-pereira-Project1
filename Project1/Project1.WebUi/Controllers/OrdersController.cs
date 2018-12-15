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
using System.Globalization;
using Project1.WebUi.Controllers.Exceptions;

namespace Project1.WebUi.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderRepository Repository { get; set; }
        private ICustomerRepository CustomerRepository { get; set; }
        private IPizzaRepository PizzaRepository { get; set; }

        public OrdersController(IOrderRepository repository, ICustomerRepository customerRepository, IPizzaRepository pizzaRepository)
        {
            Repository = repository;
            CustomerRepository = customerRepository;
            PizzaRepository = pizzaRepository;
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
            IEnumerable<Customer> customerList = Mapper.Map<IEnumerable<Customers>, IEnumerable<Customer>>((IEnumerable<Customers>)CustomerRepository.GetAll());
            IEnumerable<Pizza> pizzaList = Mapper.Map<IEnumerable<Pizzas>, IEnumerable<Pizza>>((IEnumerable<Pizzas>)PizzaRepository.GetAll());
            OrderForm orderViewForm = new OrderForm(customerList, pizzaList);

            return View(orderViewForm);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                DateTime lastOrderDate = Repository.getLastOrderDate(int.Parse(collection["AddressId"]));

                Order order = new Order();

                order.CustomerId = int.Parse(collection["CustomerId"]);
                order.AddressId = int.Parse(collection["AddressId"]);
                order.Date = DateTime.Parse(collection["Date"]);
                order.Value = 0;

                order.canOrderFromSameAddress(lastOrderDate);

                IList<Pizza> pizzasList = Mapper.Map<IList<Pizzas>, IList<Pizza>>((IList<Pizzas>)PizzaRepository.GetAll());

                for (var i = 0; i<pizzasList.Count(); i++)
                {
                    int quantity = int.Parse(collection["pizzas"][i]);

                    for(var j=0; j<quantity; j++)
                    {
                        Pizza pizza = pizzasList[i];

                        //Check Stock 
                        //Need this assign because the pizzaslist doesn't get all ingredients, (it's used in other places, and don't need the ingredients)
                        Pizza check = Mapper.Map<Pizzas, Pizza>(PizzaRepository.GetById(pizza.Id));
                        check.checkStock(1);

                        order.AddPizza(pizza);
                        PizzaRepository.DecreaseStock(Mapper.Map<Pizza, Pizzas>(pizza));
                    }
                }

                Repository.Save(Mapper.Map<Order, Orders>(order));
                Repository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (MinimumStockException ex)
            {
                ModelState.AddModelError("ErrorPizzas", ex.Message);
                return View();
            }
            catch (SamePlaceException ex)
            {
                ModelState.AddModelError("AddressId", ex.Message);
                return View();
            }
            catch (MaximumAmountException ex)
            {
                ModelState.AddModelError("Value", ex.Message);
                return View();
            }
            catch (MaximumQuantityException ex)
            {
                ModelState.AddModelError("ErrorPizzas", ex.Message);
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ErrorPizzas", e.Message);
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            Order order = Mapper.Map<Orders, Order> (Repository.GetById(id));
            if (order != null)
            {
                IEnumerable<Customer> customerList = Mapper.Map<IEnumerable<Customers>, IEnumerable<Customer>>((IEnumerable<Customers>)CustomerRepository.GetAll());
                IEnumerable<Pizza> pizzaList = Mapper.Map<IEnumerable<Pizzas>, IEnumerable<Pizza>>((IEnumerable<Pizzas>)PizzaRepository.GetAll());
                OrderForm orderViewForm = new OrderForm(order, customerList, pizzaList);

                return View(orderViewForm);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                    Order order = Mapper.Map<Orders, Order>(Repository.GetById(id));

                    if (id != order.Id)
                    {
                        ModelState.AddModelError("id", "should match the route id");
                        return View();
                    }

                    order.CustomerId = int.Parse(collection["CustomerId"]);
                    order.AddressId = int.Parse(collection["AddressId"]);
                    order.OrderPizzas = new List<OrderPizza>();
                    order.Value = 0;

                    IList<Pizza> pizzasList = Mapper.Map<IList<Pizzas>, IList<Pizza>>((IList<Pizzas>)PizzaRepository.GetAll());

                    for (var i = 0; i < pizzasList.Count(); i++)
                    {
                        int quantity = int.Parse(collection["pizzas"][i]);

                        for (var j = 0; j < quantity; j++)
                        {
                            order.AddPizza(pizzasList[i]);
                        }
                    }

                    Orders orderDataAccess = Mapper.Map<Order, Orders>(order);
                    orderDataAccess.OrderPizzas = Mapper.Map<ICollection<OrderPizza>, ICollection<OrderPizzas>>(order.OrderPizzas);

                    Repository.Save(orderDataAccess, id);
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

        //GET: Orders/CanOrderFromSameAddress/5
        public bool CanOrderFromSameAddress(int id)
        {
            try
            {
                DateTime lastOrderDate = Repository.getLastOrderDate(id);
                Order order = new Order();

                return order.canOrderFromSameAddress(lastOrderDate);
            }
            catch
            {
                return false;
            }
        }
    }
}