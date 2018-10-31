using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myProject.Models;
using myProject.Services;
using myProject.ViewModels;
using System.Web;

namespace myProject.Controllers
{
    public class HamperController : Controller
    {
        private IDataService<Hamper> _dataService;
        public HamperController(IDataService<Hamper> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(HamperAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Hamper hamper = new Hamper
                {
                    CategoryId = vm.CategoryId,
                    HamperName = vm.HamperName,
                    Details = vm.Details,
                    Price = vm.Price
                };
                _dataService.Create(hamper);
                return RedirectToAction("Index", "Home");
            }
            //if invalid
            return View(vm);
        }
    }
}