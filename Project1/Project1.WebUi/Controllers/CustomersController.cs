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
    public class CustomersController : Controller
    {
        private ICustomerRepository Repository { get; set; }

        public CustomersController(ICustomerRepository repository)
        {
            Repository = repository;
        }

        // GET: Customer
        public ActionResult Index()
        {
            return View(Mapper.Map< IEnumerable<Customers>, IEnumerable<Customer>>((IEnumerable<Customers>)Repository.GetAll()));
            //return View(Repository.GetAll());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            Customer customer = Mapper.Map<Customers, Customer>(Repository.GetById(id));
            if (customer != null)
            {
                return View(customer);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerAddressForm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer customer = model.Customer;
                    model.Address.DefaultAddress = true;
                    customer.Addresses = new List<Address>() { model.Address };
                    

                    Customers customerDataAccess = Mapper.Map<Customer, Customers>(customer);
                    customerDataAccess.Addresses = Mapper.Map<ICollection<Address>, ICollection<Addresses>>(customer.Addresses);

                    Repository.Save(customerDataAccess);
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer = Mapper.Map<Customers, Customer> (Repository.GetById(id));
            if (customer != null)
            {
                return View(customer);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                if (id != customer.Id)
                {
                    ModelState.AddModelError("id", "should match the route id");
                    return View();
                }

                if (ModelState.IsValid)
                {
                    Repository.Save(Mapper.Map<Customer, Customers>(customer), id);
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = Mapper.Map<Customers, Customer>(Repository.GetById(id));
            if (customer != null)
            {
                return View(customer);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Customer/Delete/5
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
        
        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(Search search)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (search.searchText == "" || search.searchText == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    if (search.searchBy == "ID")
                    {
                        int id = int.Parse(search.searchText);

                        Customer customer = Mapper.Map<Customers, Customer>(Repository.GetById(id));
                        List<Customer> customers = new List<Customer>();
                        customers.Add(customer);

                        return View("Index", customers);
                    }
                    else if (search.searchBy == "Name")
                    {
                        IEnumerable<Customer> customers = Mapper.Map<IEnumerable<Customers>, IEnumerable<Customer>>((IEnumerable<Customers>)Repository.GetByName(search.searchText));
                        return View("Index", customers);
                    }
                }
            }
            catch(Exception e)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}