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
    public class AddressController : Controller
    {
        private IDataService<Profile> _dataServiceProf;
        private IDataService<Address> _dataServiceAddress;

        public AddressController(IDataService<Address> serviceAddress, IDataService<Profile> serviceProfile)
        {
            _dataServiceAddress = serviceAddress;
            _dataServiceProf = serviceProfile;
        }
        [HttpGet]
        public IActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressAddAddressViewModel vm)
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