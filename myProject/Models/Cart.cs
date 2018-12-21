using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int HamperId { get; set; }
        public string UserId { get; set; }
        public int Qty { get; set; }
        public Hamper Hamper { get; set; }
    }
}
