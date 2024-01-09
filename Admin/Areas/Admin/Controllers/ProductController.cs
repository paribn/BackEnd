using Admin.Areas.Admin.Data;
using Admin.Areas.Admin.Models.ProductVM;
using Admin.Entities;
using Admin.General;
using areas.Services;
using EntityFrameworkProject.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Admin.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {

        private readonly AppDbContext _dbContext;
        private readonly FileService _fileService;

        public ProductController(AppDbContext dbContext, FileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            //int page = 1;
            //int defaultTake = 3;

            //decimal productCount = _dbContext.Products.Count();

            //int pageCount = (int)Math.Ceiling(productCount / defaultTake);

            //var product = _dbContext.Products.OrderByDescending(x => x.Id)
            //    .Skip((page - 1) * defaultTake)
            //    .Take(defaultTake)
            //    .ToList();

            var products = _dbContext.Products.
            Include(x => x.ProductImages)
           .Include(x => x.Category)
           .Include(x => x.Brand)
           .Include(x => x.Color)
           .ToList();

            var model = new ProductIndexVM
            {
                Products = products,
                //PageCount = pageCount,
                //CurrentPage = page,
            };

            return View(model);
        }

        public async Task<IActionResult> Add()
        {
            var model = new ProductAddVM();

            var productTypes = await _dbContext.Categories.ToListAsync();
            var brandTypes = await _dbContext.Brands.ToListAsync();
            var colorTypes = await _dbContext.Colors.ToListAsync();


            model.Category = productTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Brand = brandTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            model.Color = colorTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(ProductAddVM model)
        {
            var productTypes = await _dbContext.Categories.ToListAsync();
            var brandTypes = await _dbContext.Brands.ToListAsync();
            var colorTypes = await _dbContext.Colors.ToListAsync();
            model.Category = productTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            model.Brand = brandTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            model.Color = colorTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()

            }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            };

            foreach (var photo in model.Photos)
            {
                if (!photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photos", "Please choose correct image type");
                    return View(model);
                }

                if (!photo.CheckFileSize(5000))
                {
                    ModelState.AddModelError("Photos", "Please choose correct image size");
                    return View(model);
                }
            }

            List<ProductImage> productImages = new();

            foreach (var file in model.Photos)
            {
                productImages.Add(new()
                {
                    ImageName = _fileService.UploadFile(file)
                });
            }

            productImages.FirstOrDefault().IsMain = true;

            Product product = new()
            {
                Name = model.Name,
                Price = (decimal)model.Price,
                Count = model.Count,
                Description = model.Description,
                CategoryId = model.ProductCategoryId,
                BrandId = model.ProductBrandId,
                ColorId = model.ProductColorId,
                ProductImages = productImages
            };

            _dbContext.Products.Add(product);

            _dbContext.SaveChanges();

            return RedirectToAction(Constants.AdminRoutes.ProductIndexAction, Constants.AdminRoutes.ProductController);

        }

        public IActionResult Delete(int? id)
        {
            var product = _dbContext.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            if (product.ProductImages != null)
            {
                foreach (var file in product.ProductImages)
                {
                    _fileService.DeleteFile(file.ImageName);
                }
            }

            _dbContext.Products.Remove(product);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = _dbContext.Products
                .Include(x => x.ProductImages)
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Color)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            var productTypes = await _dbContext.Categories.ToListAsync();
            var brandTypes = await _dbContext.Brands.ToListAsync();
            var colorCategory = await _dbContext.Colors.ToListAsync();


            var model = new ProductUpdateVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Count = product.Count,
                Description = product.Description,
                ProductImages = product.ProductImages,
                Category = productTypes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                Brand = brandTypes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                Color = colorCategory.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),

                ProductCategoryId = product.Category.Id,
                ProductBrandId = product.Brand.Id,
                ProductColorId = product.Color.Id,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateVM model)
        {
            //if (!ModelState.IsValid) return View(model);

            var product = _dbContext.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == model.Id);
            if (product is null) return NotFound();

            if (model.Photos != null)
            {
                if (product.ProductImages != null)
                {
                    foreach (var file in product.ProductImages)
                    {
                        _fileService.DeleteFile(file.ImageName);
                    }
                }

                foreach (var photo in model.Photos)
                {
                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photos", "Please choose correct image type");
                        return View(model);
                    }

                    if (!photo.CheckFileSize(5000))
                    {
                        ModelState.AddModelError("Photos", "Please choose correct image size");
                        return View(model);
                    }
                }

                List<ProductImage> productImages = new();

                foreach (var file in model.Photos)
                {
                    productImages.Add(new()
                    {
                        ImageName = _fileService.UploadFile(file)
                    });
                }

                productImages.FirstOrDefault().IsMain = true;

                product.ProductImages = productImages;
            }

            product.Name = model.Name;
            product.Price = (decimal)model.Price;
            product.Description = model.Description;
            product.Count = model.Count;
            product.BrandId = model.ProductBrandId;
            product.ColorId = model.ProductColorId;
            product.CategoryId = model.ProductCategoryId;

            _dbContext.Products.Update(product);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
