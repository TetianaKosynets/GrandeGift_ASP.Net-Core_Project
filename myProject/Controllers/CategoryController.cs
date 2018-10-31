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
        private IDataService<Category> _categoryDataService;
        private IDataService<Hamper> _hamperDataService;

        public CategoryController(IDataService<Category>dataServiceCategory,
                                  IDataService<Hamper> dataServiceHamper)
        {
            _categoryDataService = dataServiceCategory;
            _hamperDataService = dataServiceHamper;
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryAddCategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Category cat = new Category
                {
                    CategoryName = vm.CategoryName
                };

                _categoryDataService.Create(cat);
                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }

        public IActionResult Details()
        {
            IEnumerable<Hamper> hampers = _hamperDataService.GetAll();
            IEnumerable<Category> cat = _categoryDataService.GetAll();

            CategoryDetailsViewModel vm = new CategoryDetailsViewModel
            {
                Hampers = hampers,
                Categories = cat
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category cat = _categoryDataService.GetSingle(c => c.CategoryId == id);

            CategoryEditViewModel vm = new CategoryEditViewModel
            {
                 CategoryId = cat.CategoryId,
                 CategoryName = cat.CategoryName

            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(CategoryEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //map
                Category updatedCat = new Category
                {
                    CategoryId = vm.CategoryId,
                    CategoryName = vm.CategoryName
                };
                //save
                _categoryDataService.Update(updatedCat);
                //go home
                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }

        public IActionResult CategoryById(int id)
        {
            Category cat = _categoryDataService.GetSingle(c => c.CategoryId == id);

            IEnumerable <Hamper> hamper = _hamperDataService.Query(h => h.CategoryId == id);

            CategoryCategoryByIdViewModel vm = new CategoryCategoryByIdViewModel
            {
                CategoryId = cat.CategoryId,
                Hampers = hamper,
                CategoryName = cat.CategoryName
            };

            return View(vm);
        }

        //public IActionResult Delete(int id)
        //{
        //    Category cat = _categoryDataService.GetSingle(c => c.CategoryId == id);
        //    _categoryDataService.Delete(cat);
        //    return RedirectToAction("Index", "Home");
        //}
    }
}