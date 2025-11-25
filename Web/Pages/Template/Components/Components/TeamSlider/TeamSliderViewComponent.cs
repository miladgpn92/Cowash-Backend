using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS.Team;
using SharedModels.Dtos.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Pages.Template.Components.ViewComponents
{
    public class TeamSliderViewComponent : ViewComponent
    {
        private readonly ITeamService _teamService;

        public TeamSliderViewComponent(ITeamService teamService)
        {
            this._teamService = teamService;
        }
        public async Task<IViewComponentResult> InvokeAsync()

        {
            List<TeamSelectDto> model = new();
            var res =await _teamService.List(1, 12, null,true, HttpContext.RequestAborted);
            if (res.IsSuccess)
            {
                model = res.Model;
            }
            return View("/Pages/Template/Components/Components/TeamSlider/Index.cshtml",model);
        }
    }
}
 