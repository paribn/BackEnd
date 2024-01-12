using Admin.Areas.Admin.Data;
using Admin.Entities;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Admin.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _dbContext;
        public CheckoutController(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index(CheckoutIndexVM model)
        {
            Request.Cookies.TryGetValue("Basket", out string? existingBasket);

            List<BasketItem> basketItems = new();

            var product = await _dbContext.Products.ToListAsync();
            var checkout = await _dbContext.cartCheckouts.ToListAsync();

            if (!string.IsNullOrWhiteSpace(existingBasket))
            {
                basketItems = JsonSerializer.Deserialize<List<BasketItem>>(existingBasket) ?? new List<BasketItem>();
            }
            foreach (var basketItem in basketItems)
            {
                var foundProduct = _dbContext?.cartCheckouts
              .FirstOrDefault(x => x.Id == basketItem.ProductId);

            }

            var viewModel = new CheckoutIndexVM()
            {
                BasketItems = basketItems,
                Products = product,
                Checkouts = checkout

            };
            return View(viewModel);

        }



    }
}




