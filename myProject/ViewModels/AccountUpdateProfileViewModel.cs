using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class AccountUpdateProfileViewModel
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FavouriteAddressId { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
	}
}
