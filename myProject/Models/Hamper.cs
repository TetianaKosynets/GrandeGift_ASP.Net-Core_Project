using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace myProject.Models
{
    public class Hamper
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        //public string ImageName { get; set; }
        //[DisplayName ("Upload File")]
        //public string ImagePath { get; set; }
        public int CategoryId { get; set; }
    }
}
