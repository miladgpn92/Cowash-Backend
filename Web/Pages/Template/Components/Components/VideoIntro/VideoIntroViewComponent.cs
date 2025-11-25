using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS.Setting;
using SharedModels.Dtos;

namespace Web.Pages.Template.Components.Components.VideoIntro
{
    public class VideoIntroViewComponent:ViewComponent
    {
        private readonly ISettingService _settingService;

        public VideoIntroViewComponent(ISettingService SettingService)
        {
            _settingService = SettingService;
        }


        public async Task<IViewComponentResult> InvokeAsync()

        {
            var settingResponse = _settingService.GetSetting();
            var model = settingResponse.IsSuccess ? settingResponse.Model : new SettingSelectDto();

            return View("/Pages/Template/Components/Components/VideoIntro/Index.cshtml", model);
        }

    }
}
