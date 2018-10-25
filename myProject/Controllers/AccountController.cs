using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myProject.Models;
using myProject.Services;
using myProject.ViewModels;

namespace myProject.Controllers
{
    public class AccountController : Controller
    {
        private IDataService<IdentityUser> _dataService;
        private IDataService<Profile> _dataServiceProf;
        private IDataService<Address> _dataServiceAddress;
        private SignInManager<IdentityUser> _signInManagerService;
        private RoleManager<IdentityRole> _roleManagerService;

        private UserManager<IdentityUser> _userManagerService;

        public AccountController(UserManager<IdentityUser> managerService,
                                    SignInManager<IdentityUser> signinService,
                                    RoleManager<IdentityRole> roleService,
                                    IDataService<Profile> serviceProf,
                                    IDataService<Address> serviceAddress,
                                    IDataService<IdentityUser> service
        {
            _userManagerService = managerService;
            _signInManagerService = signinService;
            _roleManagerService = roleService;

            _dataServiceProf = serviceProf;
            _dataServiceAddress = serviceAddress;

            _dataService = service;
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
        public IActionResult UpdateProfile(string id)
        {
            IdentityUser user = _dataService.GetSingle(p => p.Id == id);
            Profile prof = _dataServiceProf.GetSingle(s => s.UserID == id);
            Address address = _dataServiceAddress.GetSingle(a => a.UserID == id);

            AccountUpdateProfileViewModel vm = new AccountUpdateProfileViewModel
            {
                FirstName = prof.FirstName,
                LastName = prof.LastName,
                StreetAddress = address.StreetAddress

            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AddressAddAddressViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Address address = new Address
                {
                    AddressID = vm.AddressID,
                    StreetAddress = vm.StreetAddress,
                    City = vm.City,
                    State = vm.State,
                    PostCode = vm.PostCode
                };

                _dataServiceAddress.Create(address);
                return RedirectToAction("Index", "Home");
            }
            //if invalid
            return View(vm);
        }
    }
}