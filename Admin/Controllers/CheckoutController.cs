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


            if (!string.IsNullOrWhiteSpace(existingBasket))
            {
                basketItems = JsonSerializer.Deserialize<List<BasketItem>>(existingBasket)!;
            }
            foreach (var basketItem in basketItems)
            {
                var foundProduct = _dbContext?.cartCheckouts.Include(x => x.Id).
                    FirstOrDefault(x => x.Id == basketItem.ProductId);

            }

            var viewModel = new CheckoutIndexVM()
            {
                BasketItems = basketItems
            };
            return View(viewModel);

        }



    }
}




