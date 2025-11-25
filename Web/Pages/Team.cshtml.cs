using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Services.Services.CMS.Team;
using SharedModels.Dtos;
using SharedModels.Dtos.Shared;
using Web.Pages.Template.Components.Pagination;

namespace Web.Pages
{
    public class TeamModel : PageModel
    {
        private readonly ITeamService _service;

        public TeamModel(ITeamService service)
        {
            this._service = service;
        }
        [BindProperty]
        public List<TeamSelectDto> Items { get; set; } = new();

        [BindProperty]
        public VCPagination Pagination { get; set; } = new();

        [BindProperty]
        public string Query { get; set; }

        public async Task OnGetAsync(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int pageSize = 12)
        {
            VCPagination vCPagination = new VCPagination()
            {
                Page = page,
                PageSize = pageSize
            };

            SEODto PageSeo = new SEODto()
            {
                SEOTitle = "تیم",
                SEODesc = "تیم",
            };

            ViewData["Seo"] = PageSeo;


            var res = await _service.List(page, pageSize, null,true, cancellationToken);
            if (res.IsSuccess)
            {
                Items = res.Model;
                vCPagination.Total = Items.Count;
            }
            Pagination = vCPagination;
        }
    }
}
