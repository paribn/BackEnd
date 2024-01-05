using Admin.Areas.Admin.Data;
using Admin.Areas.Admin.Models.ColorVM;
using Admin.Entities;
using Admin.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ColorController : BaseController
    {
        private readonly AppDbContext _dbcontext;

        public ColorController(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public IActionResult Index()
        {

            var colors = _dbcontext.Colors.AsNoTracking().ToList();

            var model = new ColorIndexVM
            {
                colors = colors
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Add(ColorAddVM model)
        {
            var color = new Color
            {
                Name = model.Name,
                Code = model.Code
            };

            _dbcontext.Colors.Add(color);
            _dbcontext.SaveChanges();

            return RedirectToAction(Constants.AdminRoutes.ColorIndexAction, Constants.AdminRoutes.ColorController);

        }


        public IActionResult Update(int? id)
        {
            if (id is null) return NotFound();

            var color = _dbcontext.Colors.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (color is null) return NotFound();

            var model = new ColorUpdateVM
            {
                Id = color.Id,
                Name = color.Name,
                Code = color.Code
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ColorUpdateVM model)
        {
            var color = _dbcontext.Colors.FirstOrDefault(x => x.Id == model.Id);
            if (color is null) return NotFound();

            color.Name = model.Name;
            color.Code = model.Code;

            _dbcontext.SaveChanges();

            return RedirectToAction(Constants.AdminRoutes.ColorIndexAction, Constants.AdminRoutes.ColorController);

        }

        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();

            var color = _dbcontext.Colors.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (color is null) return NotFound();

            _dbcontext.Colors.Remove(color);
            _dbcontext.SaveChanges();

            return RedirectToAction(Constants.AdminRoutes.ColorIndexAction, Constants.AdminRoutes.ColorController);

        }
    }
}
