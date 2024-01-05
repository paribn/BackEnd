using Admin.Areas.Admin.Data;
using Admin.Areas.Admin.Models.BrandVM;
using Admin.Entities;
using Admin.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BrandController : BaseController
    {
        private readonly AppDbContext _dbcontext;

        public BrandController(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IActionResult Index()
        {
            var brands = _dbcontext.Brands.AsNoTracking().ToList();

            var model = new BrandIndexVM
            {
                Brands = brands
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Add(BrandAddVM model)
        {
            var brand = new Brand
            {
                Name = model.Name
            };

            _dbcontext.Brands.Add(brand);
            _dbcontext.SaveChanges();

            return RedirectToAction(Constants.AdminRoutes.BrandIndexAction, Constants.AdminRoutes.BrandController);

        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return NotFound();

            var brand = _dbcontext.Brands.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (brand is null) return NotFound();

            var model = new BrandUpdateVM
            {
                Id = brand.Id,
                Name = brand.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandUpdateVM model)
        {
            var brand = _dbcontext.Brands.FirstOrDefault(x => x.Id == model.Id);
            if (brand is null) return NotFound();

            brand.Name = model.Name;

            _dbcontext.SaveChanges();

            return RedirectToAction(Constants.AdminRoutes.BrandIndexAction, Constants.AdminRoutes.BrandController);

        }

        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();

            var brand = _dbcontext.Brands.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (brand is null) return NotFound();

            _dbcontext.Brands.Remove(brand);
            _dbcontext.SaveChanges();

            return RedirectToAction(Constants.AdminRoutes.BrandIndexAction, Constants.AdminRoutes.BrandController);

        }
    }
}
