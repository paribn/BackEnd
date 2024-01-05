using Admin.Areas.Admin.Data;
using Admin.Areas.Admin.Models.CategoryVM;
using Admin.Entities;
using Admin.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly AppDbContext _dbcontext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IActionResult Index()
        {
            var category = _dbcontext.Categories.AsNoTracking().ToList();

            var model = new CategoryIndexVM
            {
                categories = category
            };
            return View(model);
        }

        public IActionResult Add()
        {
            var model = new CategoryAddVM();



            return View(model);
        }


        [HttpPost]
        public IActionResult Add(CategoryAddVM model)
        {
            if (!ModelState.IsValid) return View(model);


            var category = new Category
            {
                Name = model.Name

            };

            _dbcontext.Categories.Add(category);

            _dbcontext.SaveChanges();

            return RedirectToAction(Constants.AdminRoutes.CategoryIndexAction, Constants.AdminRoutes.CategoryController);

        }


        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();

            var category = _dbcontext.Categories.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (category is null) return NotFound();

            _dbcontext.Categories.Remove(category);
            _dbcontext.SaveChanges();

            return RedirectToAction(Constants.AdminRoutes.CategoryIndexAction, Constants.AdminRoutes.CategoryController);

        }
        public IActionResult Update(int id)
        {
            var category = _dbcontext.Categories
                .FirstOrDefault(x => x.Id == id);

            if (category is null) return NotFound();


            var model = new CategoryUpdateVM
            {
                Id = category.Id,
                Name = category.Name,

            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Update(CategoryUpdateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = _dbcontext.Categories.FirstOrDefault(x => x.Id == model.Id);
            if (category is null) return NotFound();


            category.Name = model.Name;

            _dbcontext.Categories.Update(category);

            _dbcontext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


    }
}
