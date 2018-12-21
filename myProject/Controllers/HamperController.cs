using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myProject.Models;
using myProject.Services;
using myProject.ViewModels;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myProject.Controllers
{
    public class HamperController : Controller
    {
        private IDataService<Hamper> _dataService;
        private IDataService<Category> _catDataService;
        private IDataService<Cart> _cartDataService;

        private MyDbContext _context;

        public HamperController(IDataService<Hamper> dataService,
                                IDataService<Category> catDataService,
                                IDataService<Cart> cartDataService,
                                MyDbContext context)
        {
            _dataService = dataService;
            _catDataService = catDataService;
            _cartDataService = cartDataService;

            _context = context;
        }
        
        [Route("/Hamper/Add/{id}")]
        [HttpGet]
        public IActionResult Add(int id)
        {
            Category cat = _catDataService.GetSingle(c => c.CategoryId == id);

            HamperAddViewModel vm = new HamperAddViewModel
            {
                CategoryId = cat.CategoryId,
                CategoryName = cat.CategoryName
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HamperAddViewModel vm, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload == null)
                {
                    Hamper hamper = new Hamper
                    {
                        CategoryId = vm.CategoryId,
                        HamperName = vm.HamperName,
                        Details = vm.Details,
                        Price = vm.Price
                    };
                _context.TblHamper.Add(hamper);
            }
                else
                {
                    // Create a binary read to read the data from the upload file
                    BinaryReader binaryReader = new BinaryReader(upload.OpenReadStream());
                    // Read the bytes, specifying the length of the content
                    byte[] fileData = binaryReader.ReadBytes((int)upload.Length);
                    // Extract the file name; we don't want the whole path
                    var fileName = Path.GetFileName(upload.FileName);

                    Hamper hamper = new Hamper
                    {
                        CategoryId = vm.CategoryId,
                        HamperName = vm.HamperName,
                        Details = vm.Details,
                        Price = vm.Price,
                        FileName = fileName,
                        ContentSize = upload.Length,
                        ContentType = upload.ContentType,
                        FileContent = fileData
                    };
                //_dataService.Create(hamper);
                _context.TblHamper.Add(hamper);
            }

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Details", "Category");
            }
            //if invalid
            return View(vm);
        }

        [Route("/Hamper/Delete/{id}")]
        public IActionResult Delete(int id)
		{
			Hamper hamper = _dataService.GetSingle(c => c.HamperId == id);

			_dataService.Delete(hamper);

			return RedirectToAction("Details", "Category");
		}

        [Route("/Hamper/Details/{id}")]
        [HttpGet]
        public IActionResult Details(int id)
		{
			Category cat = _catDataService.GetSingle(c => c.CategoryId == id);

            IEnumerable<Hamper> hampers = _dataService.GetQuery().Where(h => h.CategoryId == id).ToList();

			HamperDetailsViewModel vm = new HamperDetailsViewModel
			{
				CategoryId = cat.CategoryId,
				Hampers = hampers,
				CategoryName = cat.CategoryName
			};

			return View(vm);
		}

        [HttpPost]
        public IActionResult Details(int id, int searchMin, int searchMax, HamperDetailsViewModel vm)
        {
            Category cat = _catDataService.GetSingle(c => c.CategoryId == id);

            IEnumerable<Hamper> hampers = _dataService.GetQuery().Where(h => h.CategoryId == id).ToList();

            vm.Hampers = hampers.Where((p => (p.Price >= searchMin) && (p.Price <= searchMax)));

            return View(vm);
        }

        [Route("/Hamper/Edit/{id}")]
        [HttpGet]
		public IActionResult Edit(int id)
		{
			Hamper hamper = _dataService.GetSingle(c => c.HamperId == id);

			HamperEditViewModel vm = new HamperEditViewModel
			{
				HamperId = hamper.HamperId,
				HamperName = hamper.HamperName,
				Details = hamper.Details,
				Price = hamper.Price,
				CategoryId = hamper.CategoryId,
			};

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(HamperEditViewModel vm, IFormFile upload)
		{
			if (ModelState.IsValid)
			{
                Hamper hamper = _context.TblHamper.Where(h => h.HamperId == vm.HamperId).FirstOrDefault();

                if (upload==null)
                {
                    hamper.HamperName = vm.HamperName;
                    hamper.Details = vm.Details;
                    hamper.Price = vm.Price;
                }
                else
                {
                    // Create a binary read to read the data from the upload file
                    BinaryReader binaryReader = new BinaryReader(upload.OpenReadStream());
                    // Read the bytes, specifying the length of the content
                    byte[] fileData = binaryReader.ReadBytes((int)upload.Length);
                    // Extract the file name; we don't want the whole path
                    var fileName = Path.GetFileName(upload.FileName);

                    hamper.HamperName = vm.HamperName;
                    hamper.Details = vm.Details;
                    hamper.Price = vm.Price;
                    hamper.FileName = fileName;
                    hamper.FileContent = fileData;
                    hamper.ContentSize = upload.Length;
                    hamper.ContentType = upload.ContentType;
                }
				await _context.SaveChangesAsync();
				
				return RedirectToAction("Details", "Hamper", new { Id = hamper.CategoryId });
			}
			return View(vm);
		}

        [Route("/Hamper/Id/{id}")]
        [HttpGet]
        public IActionResult Id(int id)
        {
            Hamper hamper = _dataService.GetSingle(c => c.HamperId == id);

            HamperIdViewModel vm = new HamperIdViewModel
            {
                HamperId = hamper.HamperId,
                HamperName = hamper.HamperName,
                Details = hamper.Details,
                Price = hamper.Price,
                FileName = hamper.FileName,
                CategoryId = hamper.CategoryId,
                Qty = 1
            };

            return View(vm);
        }

        [Route("/Hamper/Id/{id}")]
        [HttpPost]
        public IActionResult Id(int id,int Qty, HamperIdViewModel vm)
        {
            
            Hamper hamper = _dataService.GetSingle(c => c.HamperId == id);

            return RedirectToAction("Create", "Cart", new { id = hamper.HamperId, qty = Qty});
        }

        [HttpGet]
        public IEnumerable<Hamper> All()
        {
            IEnumerable<Hamper> h = _dataService.GetAll();
            return h;
        }

        [Route("/Hamper/API/{search}")]
        [HttpGet]
        public IEnumerable<Hamper> Get(string search)
        {
            var h = from m in _context.TblHamper
                    select m;
            if (!String.IsNullOrEmpty(search))
            {
                h = h.Where(s => s.HamperName.Contains(search) || s.Details.Contains(search));
                HamperSearchViewModel vm = new HamperSearchViewModel
                {
                    Hampers = h,
                };
                return h;
            }
            return null;
        }

        [Route("/Hamper/API")]
        [HttpGet]
        public IEnumerable<Hamper> Get()
        {
            var h = from m in _context.TblHamper
                    select m;
                HamperSearchViewModel vm = new HamperSearchViewModel
                {
                    Hampers = h,
                };
                return h;
        }

        [Route("Hamper/search")]
        [HttpGet]
        public async Task<IActionResult> Search(string search)
        {
            var h = from m in _context.TblHamper
                    select m;
            if (!String.IsNullOrEmpty(search))
            {
                h = h.Where(s => s.HamperName.Contains(search) || s.Details.Contains(search));
                IEnumerable<Category> cat = _catDataService.GetAll();
                IEnumerable<SelectListItem> items = _context.TblCategory.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName

                });
                ViewBag.CategoryName = items;

                HamperSearchViewModel vm = new HamperSearchViewModel
                {
                    Hampers = h,
                    SearchHamper = search,
                    Categories = cat
                };
                IEnumerable<Hamper> hamp = vm.Hampers;
                if (h.Select(a=>a.HamperId).FirstOrDefault()==0)
                {
                    return RedirectToAction("NotFound", "Hamper");
                }
                return View(vm);
            }

            return View();
        }

        [Route("Hamper/search")]
        [HttpPost]
        public IActionResult Search(int searchMin, int searchMax, HamperSearchViewModel vm)
        {
            var h = from m in _context.TblHamper
                    select m;

            string search = vm.SearchHamper;

            if (vm.Categories.Select(c => c.CategoryId).FirstOrDefault() != 0)
            {
                vm.Hampers = h.Where(p => p.CategoryId == vm.CategoryId);
            }

            h = h.Where(s => s.HamperName.Contains(search) || s.Details.Contains(search));
            vm.Hampers = h.Where((p => (p.Price >= searchMin) && (p.Price <= searchMax)));

            if (h.Select(a => a.HamperId).FirstOrDefault() == 0)
            {
                return RedirectToAction("NotFound", "Hamper");
            }
            return View(vm);
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }
}