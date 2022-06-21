using Microsoft.AspNetCore.Mvc;

namespace EMY.Papel.Restaurant.Web.Components.FooresRestaurant
{
    public class NavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("_Navbar"));
        }

    }
}
