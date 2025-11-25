using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS.Portfolio;
using SharedModels.Dtos.Shared;

namespace Web.Pages.Template.Components.Components.PortfolioSlider
{
    public class PortfolioSliderViewComponent : ViewComponent
    {
        private readonly IPortfolioService _service;

        public PortfolioSliderViewComponent(IPortfolioService service)
        {
            this._service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<PortfolioSelectDto> model = new();
            var res = await _service.List(1, 12, null, true,null, HttpContext.RequestAborted);
            if (res.IsSuccess)
            {
                model = res.Model;
            }

            return View("/Pages/Template/Components/Components/PortfolioSlider/Index.cshtml", model);
        }
    }
}
