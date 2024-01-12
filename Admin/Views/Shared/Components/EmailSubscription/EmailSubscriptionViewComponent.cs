using Admin.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Views.Shared.Components.EmailSubscription
{
    public class EmailSubscriptionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(EmailEntity emailEntity)
        {
            return await Task.FromResult(View(emailEntity));
        }

    }
}
