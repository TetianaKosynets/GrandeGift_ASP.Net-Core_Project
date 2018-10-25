using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace myProject.Controllers
{
    public class HamperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}