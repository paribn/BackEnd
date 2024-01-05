using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
