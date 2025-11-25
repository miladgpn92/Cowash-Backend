using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CMS.GlobalSetting;
using Services.Services.CMS.Setting;
using SharedModels.Dtos;

namespace Web.Pages
{
    public class ContactUsModel : PageModel
    {
        private readonly ISettingService _settingService;
        private readonly IGlobalSettingService _globalSettingService;

        public ContactUsModel(ISettingService settingService , IGlobalSettingService GlobalSettingService)
        {
            this._settingService = settingService;
            _globalSettingService = GlobalSettingService;
        }

        [BindProperty]
        public SettingSelectDto Setting { get; set; }


        [BindProperty]
        public GetGlobalSettingDto GlobalSetting { get; set; }

        public void OnGet()
        {

            var res = _settingService.GetSetting();
            if (res.IsSuccess)
            {
                Setting = res.Model;
            }

            var resG = _globalSettingService.GetGlobalSetting();
            if (resG.IsSuccess)
            {
                GlobalSetting = resG.Model;
            }
        }
    }
}
