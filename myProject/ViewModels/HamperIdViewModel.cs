using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class HamperIdViewModel
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string FileName { get; set; }
        public int Qty { get; set; }
    }
}
