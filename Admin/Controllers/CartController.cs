using Admin.Areas.Admin.Data;
using Admin.Entities;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Admin.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _dbContext;
        public CartController(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = new CartIndexVM
            {
                Products = await _dbContext.Products.ToListAsync(),

            };
            return View(model);
        }

        public async Task<IActionResult> AddToBasket(int id)
        {
            var foundProduct = _dbContext.Products.FirstOrDefault(x => x.Id == id);

            if (foundProduct is null) return BadRequest();

            List<BasketItem> basketItemsList;
            BasketItem? newBasketItem = null;


            Request.Cookies.TryGetValue("Basket", out string? existingBasket);


            if (!string.IsNullOrWhiteSpace(existingBasket))
            {
                basketItemsList = JsonSerializer.Deserialize<List<BasketItem>>(existingBasket)!;
                newBasketItem = basketItemsList.FirstOrDefault(x => x.ProductId == foundProduct.Id);

            }
            else
            {
                basketItemsList = new();
            }
            if (newBasketItem is null)
            {
                newBasketItem = new BasketItem
                {
                    ProductId = foundProduct.Id,
                    Count = foundProduct.Count,

                };

            }

            basketItemsList.Add(newBasketItem);

            Response.Cookies.Append("Basket", JsonSerializer.Serialize(basketItemsList));

            return RedirectToAction("Index", "Shop");
        }



    }
}
