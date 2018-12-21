using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace myProject.ViewModels
{
    public class HamperSearchViewModel
    {
        public int HamperId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SearchHamper { get; set; }
        public string searchMin { get; set; }
        public string searchMax { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }

}
