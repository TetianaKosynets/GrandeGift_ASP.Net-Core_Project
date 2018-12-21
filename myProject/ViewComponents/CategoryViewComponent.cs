using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myProject.Models;
using myProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myProject.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly MyDbContext db;
        private readonly IDataService<Category> _dataService;

        public CategoryViewComponent(MyDbContext context,
                                     IDataService<Category> dataService)
        {
            db = context;
            _dataService = dataService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        private async Task<List<Category>> GetItemsAsync()
        {
            return await db.TblCategory.Where(i=>i.CategoryName!=null).ToListAsync();
        }
    }
}

