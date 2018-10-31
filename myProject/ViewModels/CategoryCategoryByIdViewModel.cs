using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class CategoryCategoryByIdViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
    }
}
