using Admin.Areas.Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ShopController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(int? categoryId, int? brandId, int colorId)
        {
            var categories = await _appDbContext.Categories.AsNoTracking().ToListAsync();
            var brands = await _appDbContext.Brands.AsNoTracking().ToListAsync();
            var colors = await _appDbContext.Colors.AsNoTracking().ToListAsync();

            var products = await _appDbContext.Products
                .Where(x => (categoryId == null ? true : x.CategoryId == categoryId)
                    && (brandId == null ? true : x.BrandId == brandId))
                //&& (colorId == null ? true : x.ColorId == colorId))
                .Include(x => x.ProductImages)
                .Take(6)
                .AsNoTracking()
                .ToListAsync();

            var model = new ShopIndexVM
            {
                Categories = categories,
                Brands = brands,
                Colors = colors,
                Products = products
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            var model = new ShopSearchVM();

            if (string.IsNullOrWhiteSpace(name))
            {
                model.Products = new();
                return ViewComponent("SearchResult", model);
            }

            var products = await _appDbContext.Products
                .AsNoTracking()
                .Where(x => x.Name.ToLower().StartsWith(name.ToLower()))
                .ToListAsync();

            model.Products = products;

            return ViewComponent("SearchResult", model);
        }


    }
}
