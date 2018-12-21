using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class AccountGuestDetailsViewModel
    {
        [Required(ErrorMessage = "Please input your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please input your Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please input Street")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Please input State")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please input City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please input your Post Code")]
        public string PostCode { get; set; }
    }
}
