using Microsoft.AspNetCore.Mvc;
using SharedModels.Dtos;
using SharedModels.Dtos.Shared;
using Web.Pages.Template.Components.ViewComponents;

namespace Web.Pages.Template.Components.Components.ProductCart
{
    public class ProductCartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(ProductSelectDto model)
        {

          

            return View("/Pages/Template/Components/Components/ProductCart/Index.cshtml", model);
        }
    }
}
