using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS.GlobalSetting;
using Services.Services.CMS.Menu;
using Services.Services.CMS.Setting;
using SharedModels.Dtos;

namespace Web.Pages.Template.Components.Components.Footer
{
    public class FooterViewComponent : ViewComponent
    {

        private readonly ISettingService _settingService;
   
        private readonly IGlobalSettingService _globalSettingService;
        private readonly IMenuSerivce _menuSerivce;
        public FooterViewComponent(ISettingService settingService, IMenuSerivce menuSerivce, IGlobalSettingService globalSettingService)
        {
            _settingService = settingService;
            _globalSettingService = globalSettingService;
            _menuSerivce = menuSerivce;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            FooterVCModel model=new FooterVCModel();

            var resSetting = _settingService.GetSetting();
            if (resSetting.IsSuccess)
            {
                model.Setting = resSetting.Model;
            }

            var resGlobalSetting = _globalSettingService.GetGlobalSetting();
            if (resGlobalSetting.IsSuccess)
            {
                model.GlobalSetting = resGlobalSetting.Model;
            }

            var resMenu = await _menuSerivce.GetBySlug("footer", HttpContext.RequestAborted);
            if (resMenu.IsSuccess)
            {
                model.Menus = resMenu.Model;
            }



            return View("/Pages/Template/Components/Components/Footer/Index.cshtml",model);
        }

    }

    public class FooterVCModel
    {
        public SettingSelectDto Setting { get; set; } = new();
        public GetGlobalSettingDto GlobalSetting { get; set; } = new();

        public List<MenuItm> Menus { get; set; } = new();

    }
}
