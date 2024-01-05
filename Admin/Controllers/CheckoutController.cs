using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
