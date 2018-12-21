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
        public int CategoryId { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public long ContentSize { get; set; }
    }
}
