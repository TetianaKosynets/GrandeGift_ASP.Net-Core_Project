using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class CartTotalViewModel
    {
        public double Total { get; set; }
        public List<CartMyCartViewModel> Items { get; set; }
    }
}
