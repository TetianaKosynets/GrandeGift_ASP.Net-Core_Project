using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class HamperDetailsViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
		public string ImageName { get; set; }
        public string searchMin { get; set; }
        public string searchMax { get; set; }
    }
}
