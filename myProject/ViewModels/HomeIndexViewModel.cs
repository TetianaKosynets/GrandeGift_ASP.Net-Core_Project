using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Hamper> Hampers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
