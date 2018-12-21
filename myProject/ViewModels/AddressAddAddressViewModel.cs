using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class AddressAddAddressViewModel
    {
        public int AddressID { get; set; }
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
		public string UserId { get; set; }
        public bool Favourite { get; set; }
    }
}
