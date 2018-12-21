using myProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewModels
{
    public class CartMyCartViewModel
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public int Qty { get; set; }
        public int HamperId { get; set; }
        public string  HamperName { get; set; }
        public string FileName { get; set; }
        public double Price { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public double Total { get; set; }
    }
}
