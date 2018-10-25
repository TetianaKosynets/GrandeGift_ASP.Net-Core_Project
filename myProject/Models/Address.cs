using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
