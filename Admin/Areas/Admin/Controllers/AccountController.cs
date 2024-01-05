using Admin.Areas.Admin.Models;
using Admin.Entities;
using Admin.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private SignInManager<AppUser> _singInManager;
        private UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _singInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult SingIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SingIn(AccountSingInVM model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user is null) return View(model);
            var result = await _singInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                model.ErrorMessage = "Email or password is wrong";
                return View(model);
            }
            return RedirectToAction(Constants.AdminRoutes.HomeIndexAction, Constants.AdminRoutes.HomeController);

        }
    }
}
