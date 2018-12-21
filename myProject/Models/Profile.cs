using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class Profile
    {
        public int ProfileID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
		public ICollection<Address> Addresses { get; set; }
	}
}
