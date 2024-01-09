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
            Request.Cookies.TryGetValue("Basket", out string? existingBasket);

            List<BasketItem> basketItems = new();
            List<Product> products = new();

            if (!string.IsNullOrWhiteSpace(existingBasket))
            {
                basketItems = JsonSerializer.Deserialize<List<BasketItem>>(existingBasket)!;
            }
            foreach (var basketItem in basketItems)
            {
                var foundProduct = _dbContext.Products.Include(x => x.ProductImages).
                    FirstOrDefault(x => x.Id == basketItem.ProductId);
                if (foundProduct != null)
                {
                    products.Add(foundProduct);
                }
            }

            var viewModel = new CartIndexVM()
            {
                Products = products,
                BasketItems = basketItems
            };
            return View(viewModel);
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
                    Count = 1,
                };
                basketItemsList.Add(newBasketItem);
            }
            else
            {
                newBasketItem.Count++;
            }

            Response.Cookies.Append("Basket", JsonSerializer.Serialize(basketItemsList));

            return RedirectToAction("Index", "Shop");
        }


        //public async Task<IActionResult> IncrementProductCount(int? id)
        //{
        //    List<BasketItem> basketItemsList;
        //    BasketItem? newBasketItem = null;

        //    if (id is null) return BadRequest();
        //    var basketProducts = await _dbContext.Products
        //       .FirstOrDefaultAsync(bp => bp.Id == id);

        //    var count = newBasketItem.Count++;


        //    return Ok(count);
        //}


        public async Task<IActionResult> RemoveFromBasket(int id)
        {
            List<BasketItem> basketItemsList;

            Request.Cookies.TryGetValue("Basket", out string? existingBasket);

            if (!string.IsNullOrWhiteSpace(existingBasket))
            {
                basketItemsList = JsonSerializer.Deserialize<List<BasketItem>>(existingBasket)!;
                basketItemsList = basketItemsList.Where(x => x.ProductId != id).ToList();
            }
            else
            {
                return NotFound();
            }
            Response.Cookies.Append("Basket", JsonSerializer.Serialize(basketItemsList));
            return RedirectToAction("Index", "Cart");

        }
    }
}
