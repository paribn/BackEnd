using Admin.Areas.Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class ProductController : Controller
    {

        private readonly AppDbContext _dbContext;
        public ProductController(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {

            var products = await _dbContext.Products
                .Include(x => x.ProductImages)
                .Take(1)
                .AsNoTracking()
                .ToListAsync();

            var model = new ProductIndexVM
            {
                products = products
            };
            return View(model);
        }

    }
}
