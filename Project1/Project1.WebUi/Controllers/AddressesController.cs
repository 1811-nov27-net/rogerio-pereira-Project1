﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.WebUi.Models;
using Project1.DataAccess;
using Project1.DataAccess.Repositories.Interfaces;
using Project1.DataAccess.Repositories;
using Project1.WebUi.Models.ViewModels;

namespace Project1.WebUi.Controllers
{
    public class AddressesController : Controller
    {
        private IAddressRepository Repository { get; set; }
        private ICustomerRepository CustomerRepository { get; set; }

        public AddressesController(IAddressRepository repository, ICustomerRepository customerRepository)
        {
            Repository = repository;
            CustomerRepository = customerRepository;
        }

        // GET: Address
        public ActionResult Index()
        {
            return View(Mapper.Map< IEnumerable<Addresses>, IEnumerable<Address>>((IEnumerable<Addresses>)Repository.GetAll()));
        }

        // GET: Address/Details/5
        public ActionResult Details(int id)
        {
            Address address = Mapper.Map<Addresses, Address>(Repository.GetById(id));
            if (address != null)
            {
                return View(address);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Address/Create
        public ActionResult Create()
        {
            IEnumerable<Customer> customers = Mapper.Map<IEnumerable<Customers>, IEnumerable<Customer>>((IEnumerable<Customers>)CustomerRepository.GetAll());
            AddressForm addressViewModel = new AddressForm(customers);
            return View(addressViewModel);
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Address address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.Save(Mapper.Map<Address, Addresses>(address));
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

        // GET: Address/Edit/5
        public ActionResult Edit(int id)
        {
            Address address = Mapper.Map<Addresses, Address> (Repository.GetById(id));
            IEnumerable<Customer> customers = Mapper.Map<IEnumerable<Customers>, IEnumerable<Customer>>((IEnumerable<Customers>)CustomerRepository.GetAll());
            AddressForm addressViewModel = new AddressForm(address, customers);

            if (address != null)
            {
                return View(addressViewModel);
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Address/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Address address)
        {
            try
            {
                if (id != address.Id)
                {
                    ModelState.AddModelError("id", "should match the route id");
                    return View();
                }

                if (ModelState.IsValid)
                {
                    Repository.Save(Mapper.Map<Address, Addresses>(address), id);
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

        // GET: Address/Delete/5
        public ActionResult Delete(int id)
        {
            var address = Mapper.Map<Addresses, Address>(Repository.GetById(id));
            if (address != null)
            {
                return View(address);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Address/Delete/5
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

        //GET: Address/5/5
        public ActionResult SetDefaultAddress (int addressId, int customerId)
        {
            try
            {
                Repository.SetDefaultAddress(addressId, customerId);
                Repository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}