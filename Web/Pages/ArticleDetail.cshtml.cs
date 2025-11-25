using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CMS.Article;
using Services.Services.CMS.GlobalSetting;
using SharedModels.Dtos;
using System.Net;

namespace Web.Pages
{
    public class ArticleDetailModel : PageModel
    {

        private readonly IArticleService _articleService;
        private readonly IGlobalSettingService _globalSetting;

        public ArticleDetailModel(IArticleService articleService , IGlobalSettingService globalSetting)
        {
            _articleService = articleService;
            this._globalSetting = globalSetting;
        }

        [BindProperty]
        public ArticleSelectDto Item { get; set; } = new();


        [BindProperty]
        public List<ArticleSelectDto> RelatedItems { get; set; } = new();

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

            var res = await _articleService.Get(decodedSlug, cancellationToken);

            if (res.IsSuccess)
            {
                SEODto PageSeo = new SEODto()
                {
                    SEOTitle = res.Model.Title,
                    SEODesc = res.Model.Description,
                    SEOPic = res.Model.PicUrl,
                    Date = res.Model.CreateDate,
                };

                ViewData["Seo"] = PageSeo;
                Item = res.Model;

                var ResRelated =await _articleService.List(1, 3, null, null, Item.ArticleCategoryId, cancellationToken);
                if (ResRelated.IsSuccess)
                {
                    RelatedItems = ResRelated.Model;
                }



                return Page();
            }
            else
            {
                return RedirectToPage("./Index");

            }
        }
    }
}
