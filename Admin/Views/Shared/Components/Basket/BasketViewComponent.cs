using Admin.Areas.Admin.Data;
using Admin.Entities;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Admin.Views.Shared.Components.Basket
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public BasketViewComponent(AppDbContext context)
        {
            _dbContext = context;
        }
        public IViewComponentResult Invoke()
        {
            Request.Cookies.TryGetValue("Basket", out string? existingBasket);

            List<BasketItem> basketItemsList = new();
            List<Product> productList = new();

            if (!string.IsNullOrWhiteSpace(existingBasket))
            {
                basketItemsList = JsonSerializer.Deserialize<List<BasketItem>>(existingBasket)!;
            }
            foreach (var basketItem in basketItemsList)
            {
                var foundProduct = _dbContext.Products.FirstOrDefault(x => x.Id == basketItem.ProductId);
                if (foundProduct != null)
                {
                    productList.Add(foundProduct);
                }
            }


            var viewModel = new BasketComponentVM
            {
                Products = productList
            };
            return View(viewModel);
        }
    }
}
