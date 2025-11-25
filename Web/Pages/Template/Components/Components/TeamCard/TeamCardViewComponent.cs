using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS.GlobalSetting;
using Services.Services.CMS.Setting;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Pages.Template.Components.ViewComponents
{
    public class TeamCardViewComponent : ViewComponent
    {
        private readonly IGlobalSettingService _settingService;

        public TeamCardViewComponent(IGlobalSettingService SettingService)
        {
            _settingService = SettingService;
        }
        public IViewComponentResult Invoke(string? Title, string? Job, string? Pic, double? Delay)

        {

            var settingRes = _settingService.GetGlobalSetting();
            string Instagram = null;
            if (settingRes.IsSuccess)
            {
                Instagram = settingRes.Model.InstagramLink;

            }
            TeamCardVCModel model = new TeamCardVCModel()
            {
                Title = Title,
                ImgUrl = Pic,
                Job = Job,
                Delay = Delay,
                Instagram = Instagram
                
            };
            return View("/Pages/Template/Components/Components/TeamCard/Index.cshtml", model);
        }
    }
}
public class TeamCardVCModel
{
    public TeamCardVCModel()
    {
    }
    public string? Title { get; set; }
    public string? ImgUrl { get; set; }
    public string? Job { get; set; }
    public string? Instagram { get; set; }
    public double? Delay { get; set; }


}