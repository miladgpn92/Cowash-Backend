using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS.Portfolio;
using Services.Services.CMS.Product;
using SharedModels.Dtos.Shared;

namespace Web.Pages.Template.Components.Components.ProductIndex
{
    public class ProductIndexViewComponent:ViewComponent
    {
        private readonly IProductService _service;

        public ProductIndexViewComponent(IProductService service)
        {
            this._service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ProductSelectDto> model = new();
            var res = await _service.ListAsync(1, 12,null,null, HttpContext.RequestAborted);
            if (res.IsSuccess)
            {
                model = res.Model;
            }

            return View("/Pages/Template/Components/Components/ProductIndex/Index.cshtml", model);
        }
    }
}
