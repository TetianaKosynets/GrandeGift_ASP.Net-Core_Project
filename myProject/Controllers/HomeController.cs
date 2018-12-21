using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myProject.Models;
using myProject.Services;
using myProject.ViewModels;

namespace myProject.Controllers
{
    public class HomeController : Controller
    {
        private IDataService<Hamper> _dataService;
        private IDataService<Category> _catDataService;
        private IDataService<Cart> _cartDataService;

        private MyDbContext _context;
        public HomeController(IDataService<Hamper> dataService,
                              IDataService<Category> catDtaService,
                              IDataService<Cart> cartDataService,
                              MyDbContext context)
        {
            _dataService = dataService;
            _catDataService = catDtaService;
            _cartDataService = cartDataService;

            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> cat = _catDataService.GetAll();
            IEnumerable<Hamper> hampers = _dataService.GetAll();

            HomeIndexViewModel vm = new HomeIndexViewModel
            {
                Categories = cat,
                Hampers = hampers
            };
            if (String.IsNullOrEmpty(HttpContext.Session.GetString(CartController.CartSessionKey)))
            {
                IEnumerable<Cart> myCart = _cartDataService.GetAll().Where(c => c.UserId.Contains("guest"));
                if (myCart != null)
                {
                    foreach (var item in myCart)
                    {
                        Cart cart = _cartDataService.GetSingle(c => c.CartId == item.CartId);
                        _cartDataService.Delete(cart);
                    }
                }
            }
            return View(vm);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}