using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CMS.Article;
using Services.Services.CMS.DynamicPage;
using SharedModels.Dtos;
using System.Net;

namespace Web.Pages
{
    public class PagesDetailModel : PageModel
    {
        private readonly IDynamicPageService _Service;

        public PagesDetailModel(IDynamicPageService Service)
        {
            _Service = Service;
        }

        [BindProperty]
        public DynamicPageSelectDto Item { get; set; } = new();


        [BindProperty(SupportsGet = true)]
        public string slug { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            if (slug == null)
                return RedirectToPage("./Index");

            var decodedSlug = WebUtility.UrlDecode(slug);

            var res = await _Service.Get(decodedSlug, cancellationToken);

            if (res.IsSuccess)
            {
                SEODto PageSeo = new SEODto()
                {
                    SEOTitle = res.Model.Title,
                    SEODesc = res.Model.Description,
                    Date = res.Model.CreateDate,
                };

                ViewData["Seo"] = PageSeo;
                Item = res.Model;
                return Page();
            }
            else
            {
                return RedirectToPage("./Index");

            }
        }
    }
}
