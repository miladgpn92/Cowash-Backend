using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS.GlobalSetting;
using Services.Services.CMS.Menu;
using Services.Services.CMS.Setting;
using SharedModels.Dtos;

namespace Web.Pages.Template.Components.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IMenuSerivce _menuSerivce;
        private readonly IGlobalSettingService _globalSettingService;

        public HeaderViewComponent(ISettingService settingService , IMenuSerivce menuSerivce , IGlobalSettingService globalSettingService)
        {
          _settingService = settingService;
          _menuSerivce = menuSerivce;
          _globalSettingService = globalSettingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            HeaderVCModel model = new HeaderVCModel();

            var resSetting= _settingService.GetSetting();
            if (resSetting.IsSuccess)
            {
                model.Setting = resSetting.Model;
            }

            var resGlobalSetting = _globalSettingService.GetGlobalSetting();
            if (resGlobalSetting.IsSuccess)
            {
                model.GlobalSetting = resGlobalSetting.Model;
            }


            var resMenu =await _menuSerivce.GetBySlug("menu", HttpContext.RequestAborted);
            if (resMenu.IsSuccess)
            {
                model.Menus = resMenu.Model;
            }



            return View("/Pages/Template/Components/Components/Header/Index.cshtml", model);
        }

    }

    public class HeaderVCModel
    {
        public SettingSelectDto Setting { get; set; } = new();

        public List<MenuItm> Menus { get; set; } = new();

        public GetGlobalSettingDto GlobalSetting { get; set; } = new();

    }
}
