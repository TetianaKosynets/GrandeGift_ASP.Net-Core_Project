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
    public class AddressController : Controller
    {
        private IDataService<Profile> _dataServiceProf;
        private IDataService<Address> _dataServiceAddress;

		private UserManager<IdentityUser> _userManagerService;

		public AddressController(IDataService<Address> serviceAddress, IDataService<Profile> serviceProfile, UserManager<IdentityUser> managerService)
        {
            _dataServiceAddress = serviceAddress;
            _dataServiceProf = serviceProfile;

			_userManagerService = managerService;
        }
        [HttpGet]
        public IActionResult AddAddress()
        {
			return View();
        }

        [HttpPost]
        public IActionResult AddAddress(AddressAddAddressViewModel vm)
        {
            if (ModelState.IsValid)
            {
				string id = _userManagerService.GetUserId(User);

				Address address = new Address
				{
					StreetAddress = vm.StreetAddress,
					City = vm.City,
					State = vm.State,
					PostCode = vm.PostCode,
					UserID = id,
                    Favourite = true
			};

                _dataServiceAddress.Create(address);
                return RedirectToAction("UpdateProfile", "Account");
            }
            //if invalid
            return View(vm);
        }


		public IActionResult Delete(int id)
		{
			Address address = _dataServiceAddress.GetSingle(c => c.AddressID == id);
			_dataServiceAddress.Delete(address);
			return RedirectToAction("UpdateProfile", "Account");
		}
	}
}