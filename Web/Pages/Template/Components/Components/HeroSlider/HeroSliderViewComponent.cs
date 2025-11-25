using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS.Faq;
using Services.Services.CMS.Slider;
using SharedModels.Dtos;
using SharedModels.Dtos.Shared;

namespace Web.Pages.Template.Components.Components.HeroSlider
{
    public class HeroSliderViewComponent:ViewComponent
    {
        private readonly ISliderService _service;

        public HeroSliderViewComponent(ISliderService service)
        {
            this._service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()

        {


            List<SliderSelectDto> model = new();
            var res = await _service.List(1, 12, HttpContext.RequestAborted);
            if (res.IsSuccess)
            {
                model = res.Model;
            }

            return View("/Pages/Template/Components/Components/HeroSlider/Index.cshtml", model);
        }
    }
}
