using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myProject.Models;
using myProject.Services;
using myProject.ViewModels;

namespace myProject.Controllers
{
    public class CategoryController : Controller
    {
        private IDataService<Category> _dataService;
        public CategoryController(IDataService<Category> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory (CategoryAddCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {

                Category cat = new Category
                {
                    CategoryId=vm.CategoryId,
                    CategoryName = vm.CategoryName
                };
                    _dataService.Create(cat);
                    return RedirectToAction("Index", "Home");
            }
            //if invalid
            return View(vm);
        }
    }
}