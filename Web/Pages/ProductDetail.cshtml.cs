using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CMS.Article;
using Services.Services.CMS.GlobalSetting;
using Services.Services.CMS.Product;
using SharedModels.Dtos;
using SharedModels.Dtos.Shared;
using System.Net;

namespace Web.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService _articleService;
        private readonly IGlobalSettingService _globalSetting;

        public ProductDetailModel(IProductService articleService, IGlobalSettingService globalSetting)
        {
            _articleService = articleService;
            this._globalSetting = globalSetting;
        }

        [BindProperty]
        public ProductSelectDto Item { get; set; } = new();

 

        [BindProperty(SupportsGet = true)]
        public string slug { get; set; }


        [BindProperty]
        public GetGlobalSettingDto GSetting { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {

            var resGSetting = _globalSetting.GetGlobalSetting();
            if (resGSetting.IsSuccess)
            {
                GSetting = resGSetting.Model;
            }


            if (slug == null)
                return RedirectToPage("./Index");

            var decodedSlug = WebUtility.UrlDecode(slug);

            var res = await _articleService.GetBySlugAsync(decodedSlug, cancellationToken);

            if (res.IsSuccess)
            {
                SEODto PageSeo = new SEODto()
                {
                    SEOTitle = res.Model.Title,
                    SEODesc = res.Model.Description,
                    SEOPic = res.Model.ThumbnailUrl,
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
