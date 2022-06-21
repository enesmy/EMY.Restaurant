using Microsoft.AspNetCore.Mvc;

namespace EMY.Papel.Restaurant.Web.Components.FooresRestaurant
{
    public class HeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("_Header"));
        }

    }
}
