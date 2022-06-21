using Microsoft.AspNetCore.Mvc;

namespace EMY.Papel.Restaurant.Web.Components.FooresRestaurant
{
    public class CssFilesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("_CssFiles"));
        }

    }
}
