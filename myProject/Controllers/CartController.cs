using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myProject.Models;
using myProject.Services;
using myProject.ViewModels;

namespace myProject.Controllers
{
    public class CartController : Controller
    {
        public const string CartSessionKey = "_cart";

        private IDataService<Cart> _dataService;
        private IDataService<Hamper> _hamperDataService;
        private IDataService<Address> _addressDataService;
        private IDataService<Profile> _profDataService;

        private MyDbContext _context;

        private UserManager<IdentityUser> _userManagerService;
        public CartController(IDataService<Cart> dataService,
                                IDataService<Hamper> hamperDataService,
                                IDataService<Address> addressDataService,
                                IDataService<Profile> profDataService,
                                MyDbContext contex,
                                UserManager<IdentityUser> userManager )
        {
            _userManagerService = userManager;

            _context = contex;

            _dataService = dataService;
            _hamperDataService = hamperDataService;
            _addressDataService = addressDataService;
            _profDataService = profDataService;
        }

        public IActionResult Create(int id, int qty)
        {
            Hamper hamper = _hamperDataService.GetSingle(c => c.HamperId == id);
            string userId;

            if (User.Identity.IsAuthenticated)
            {
                userId = _userManagerService.GetUserId(User);
            } else
            {
                userId = "guest" + HttpContext.Session.Id;
                HttpContext.Session.SetString(CartSessionKey, userId);
            }

            IEnumerable<Cart> mC = _dataService.GetAll().Where(h => h.HamperId == id);
            //IEnumerable<Cart> c = mC.Where(h => h.UserId == userId);

            Cart cartMy = mC.Where(h => h.UserId == userId).FirstOrDefault();
                if (cartMy == null)
                {
                      Cart myCart = new Cart
                      {
                                UserId = userId,
                                HamperId = hamper.HamperId,
                                Qty = qty
                      };
                       _dataService.Create(myCart);
                }
                else
                {
                    cartMy.Qty = cartMy.Qty + qty;

                    _dataService.Update(cartMy);
                }
            return RedirectToAction("Details", "Hamper", new { id = hamper.CategoryId });
        }

        [HttpGet]
        public IActionResult myCart()
        {
            string userId = _userManagerService.GetUserId(User);
            IEnumerable<Cart> myCart = _dataService.GetQuery().Include(c => c.Hamper).Where(c => c.UserId == userId);
            IEnumerable<Address> address = _addressDataService.GetQuery().Where(a => (a.UserID == userId)&&(a.Favourite==true));

            if (_dataService.GetAll().FirstOrDefault()!=null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var vm = myCart.Select(c => new CartMyCartViewModel
                    {
                        CartId = c.CartId,
                        UserId = c.UserId,
                        HamperId = c.HamperId,
                        HamperName = c.Hamper.HamperName,
                        Qty = c.Qty,
                        Price = c.Hamper.Price,
                        FileName = c.Hamper.FileName,
                        UserName = User.Identity.Name,
                        Addresses = address
                    }).ToList();

                    return View(vm);
                }
                else
                {
                    string Id = HttpContext.Session.GetString(CartSessionKey);
                    if (String.IsNullOrEmpty(Id))
                    {
                        myCart = _dataService.GetAll().Where(c => c.UserId == Id);
                        if (myCart != null)
                        {
                            foreach (var item in myCart)
                            {
                                Cart cart = _dataService.GetSingle(c => c.CartId == item.CartId);
                                _dataService.Delete(cart);
                            }
                        }
                        return RedirectToAction("EmptyCart", "Cart");
                    }
                    else
                    {
                        IEnumerable<Cart> Cart = _dataService.GetQuery().Include(h => h.Hamper).Where(c => c.UserId == Id);

                        var vm = Cart.Select(c => new CartMyCartViewModel
                        {
                            CartId = c.CartId,
                            UserId = c.UserId,
                            HamperId = c.HamperId,
                            HamperName = c.Hamper.HamperName,
                            Qty = c.Qty,
                            Price = c.Hamper.Price,
                            FileName = c.Hamper.FileName,
                            UserName = "Guest"
                        }).ToList();
                        return View(vm);
                    }
                }
            }
            else
            {
                return RedirectToAction("EmptyCart", "Cart");
            }
        }

        [HttpPost]
        public IActionResult myCart(int id, CartMyCartViewModel vm)
        {
            Cart myCart = _dataService.GetSingle(c => c.CartId == id);

            if (vm.Qty != 0)
            {
                myCart.Qty = vm.Qty;

                _dataService.Update(myCart);

                return RedirectToAction("myCart", "Cart");
            }

            _dataService.Delete(myCart);

            return RedirectToAction("myCart", "Cart");
        }

        [HttpGet]
        public IActionResult EmptyCart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Order()
        {
            IEnumerable<Cart> myCart;

            if (User.Identity.IsAuthenticated)
            {
                myCart = _dataService.GetAll().Where(c => c.UserId == _userManagerService.GetUserId(User));
            }
            else
            {
                string Id = HttpContext.Session.GetString(CartSessionKey);
                myCart = _dataService.GetAll().Where(c => c.UserId == Id);
                Profile profile = _profDataService.GetSingle(p=>p.UserID == Id);
                Address address = _addressDataService.GetSingle(a => a.UserID == Id);
                _profDataService.Delete(profile);
                _addressDataService.Delete(address);
            }
            foreach (var item in myCart)
            {
                Cart cart = _dataService.GetSingle(c => c.CartId == item.CartId);
                _dataService.Delete(cart);
            }

            return View();
        }
    }
}