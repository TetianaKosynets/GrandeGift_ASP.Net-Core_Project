using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }

    }
}
