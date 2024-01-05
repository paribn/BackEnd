using Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Views.Shared.Components.SearchResult
{
    public class SearchResultViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ShopSearchVM model)
        {
            return View(model);
        }
    }
}

