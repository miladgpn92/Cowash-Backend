using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CMS.DynamicPage;
using SharedModels.Dtos;
using Web.Pages.Template.Components.Pagination;

namespace Web.Pages
{
    public class PagesModel : PageModel
    {
        private readonly IDynamicPageService _service;

        public PagesModel(IDynamicPageService service)
        {
            this._service = service;
        }



        public List<DynamicPageSelectDto> Items { get; set; } = new();

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
                SEOTitle = "صفحه",
                SEODesc = "صفحه",
            };

            ViewData["Seo"] = PageSeo;


            var res = await _service.List(page, pageSize, null,null, cancellationToken);
            if (res.IsSuccess)
            {
                Items = res.Model;
                vCPagination.Total = Items.Count;
            }
            Pagination = vCPagination;
        }
    }
}
