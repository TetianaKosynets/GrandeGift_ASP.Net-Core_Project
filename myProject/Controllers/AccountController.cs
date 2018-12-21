using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myProject.Models;
using myProject.Services;
using myProject.ViewModels;

namespace myProject.Controllers
{
    public class AccountController : Controller
    {
        private IDataService<Profile> _dataServiceProf;
        private IDataService<Address> _dataServiceAddress;
        private SignInManager<IdentityUser> _signInManagerService;
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Cart> _cartDataService;
        private IDataService<Hamper> _hamperDataService;

        private UserManager<IdentityUser> _userManagerService;

        private MyDbContext _context;

        public AccountController(UserManager<IdentityUser> managerService,
                                    SignInManager<IdentityUser> signinService,
                                    RoleManager<IdentityRole> roleService,
                                    IDataService<Profile> serviceProf,
                                    IDataService<Address> serviceAddress,
                                    IDataService<Cart> cartDataService,
                                    IDataService<Hamper> hamperDataService,
                                    MyDbContext context)
        {
            _userManagerService = managerService;
            _signInManagerService = signinService;
            _roleManagerService = roleService;

            _dataServiceProf = serviceProf;
            _dataServiceAddress = serviceAddress;
            _cartDataService = cartDataService;
            _hamperDataService = hamperDataService;

            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = vm.Username
                };

                Profile prof = new Profile
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    UserID = user.Id
                };

                Address addressDetails = new Address
                {
                    UserID = user.Id,
                    StreetAddress = vm.StreetAddress,
                    City = vm.City,
                    State = vm.State,
                    PostCode = vm.PostCode
                };

                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    _dataServiceProf.Create(prof);
                    _dataServiceAddress.Create(addressDetails);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            //if invalid
            return View(vm);
        }

        [HttpGet]
        public IActionResult Login(string retrurnUrl = "")
        {
            AccountLoginViewModel vm = new AccountLoginViewModel
            {
                ReturnUrl = retrurnUrl
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var results = await _signInManagerService.PasswordSignInAsync(vm.UserName, vm.Password, vm.RememberMe, false);
                if (results.Succeeded)
                {
                    if (!string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Email or Password incorect");
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UpdateProfile()
        {
            //Current UserName
            string name = User.Identity.Name;
            //Current UserId
            string id = _userManagerService.GetUserId(User);

            Profile prof = _dataServiceProf.GetSingle(s => s.UserID == id);
            IEnumerable<Address> address = _dataServiceAddress.GetQuery().Where(a => a.UserID == id);



			AccountUpdateProfileViewModel vm = new AccountUpdateProfileViewModel
			{
				UserId = id,
				Username = name,
				FirstName = prof.FirstName,
				LastName = prof.LastName,
				Addresses = address
            };

            vm.FavouriteAddressId = address.Where(a => a.Favourite == true).Select(a => a.AddressID).FirstOrDefault();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AccountUpdateProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string userId = _userManagerService.GetUserId(User);

                Profile prof = _dataServiceProf.GetSingle(s => s.UserID == userId);

                prof.FirstName = vm.FirstName;
                prof.LastName = vm.LastName;

                Address a = _context.TblAddress.Where(s => s.Favourite == true).FirstOrDefault();
                a.Favourite = false;

                Address add = _context.TblAddress.Where(s => s.AddressID == vm.FavouriteAddressId).FirstOrDefault();
                add.Favourite = true;

                await _context.SaveChangesAsync();
                _dataServiceProf.Update(prof);
                    return RedirectToAction("Index", "Home");
            }
            //if invalid
            return View(vm);
        }

        [HttpGet]
        public IActionResult GuestDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GuestDetails(AccountGuestDetailsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Profile prof = new Profile
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    UserID = "guest" + HttpContext.Session.Id
            };

                Address addressDetails = new Address
                {
                    UserID = "guest" + HttpContext.Session.Id,
                    StreetAddress = vm.StreetAddress,
                    City = vm.City,
                    State = vm.State,
                    PostCode = vm.PostCode
                };

                    _dataServiceProf.Create(prof);
                    _dataServiceAddress.Create(addressDetails);
                    return RedirectToAction("Order", "Cart");
                }
            return View(vm);
        }
    }
}