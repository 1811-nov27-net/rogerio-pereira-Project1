using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project1.DataAccess.Repositories.Interfaces;
using Project1.WebUi.Models.ViewModels;

namespace Project1.WebUi.Controllers
{
    public class ReportsController : Controller
    {
        private IOrderRepository orderRepository;

        public ReportsController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            Reports reports = new Reports();
            reports.TotalOfDay = GetTotalOfDay();

            return View(reports);
        }

        private decimal GetTotalOfDay()
        {
            DateTime dayString = DateTime.Today;
            return orderRepository.GetTotalOfDay(dayString);
        }
    }
}