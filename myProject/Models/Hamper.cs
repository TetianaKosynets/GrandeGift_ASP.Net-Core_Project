using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.Models
{
    public class Hamper
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
