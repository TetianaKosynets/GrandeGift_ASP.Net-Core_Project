using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class HamperAddViewModel
    {
        public string HamperName { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        //public string ImageName { get; set; }
        //public string ImagePath { get; set; }
        public int CategoryId { get; set; }
    }
}
