using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required (ErrorMessage = "Email address required")]
        public string Username { get; set; }

        [Required (ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }

        [Compare ("Password", ErrorMessage = "Password is not matched")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please input your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please input your Last Name")]
        public string LastName { get; set; }

        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
