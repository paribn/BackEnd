using Admin.Areas.Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _dbContext;
        public HomeController(AppDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var products = _dbContext.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Take(9)
                .ToList();

            var model = new HomeIndexVM
            {
                Products = products
            };

            return View(model);
        }

    }
}