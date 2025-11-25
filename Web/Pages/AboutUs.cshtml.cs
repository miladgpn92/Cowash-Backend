using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CMS.Setting;
using SharedModels.Dtos;

namespace Web.Pages
{
    public class AboutUsModel : PageModel
    {
        private readonly ISettingService _settingService;

        public AboutUsModel(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        [BindProperty]
        public SettingSelectDto Setting { get; set; }
        public void OnGet()
        {
 

            var res = _settingService.GetSetting();
            if (res.IsSuccess)
            {
                Setting = res.Model;
            }

            SEODto PageSeo = new SEODto()
            {
                SEOTitle = "درباره ما",
                SEODesc = "درباره ما",
               
             
            };

            ViewData["Seo"] = PageSeo;
        }
    }
}
