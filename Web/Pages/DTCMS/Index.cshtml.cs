using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CMS.Article;
using Services.Services.CMS.DynamicPage;
using SharedModels.Dtos;

namespace Web.Pages.DTCMS
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IArticleService _articleService;
        private readonly IDynamicPageService _dynamicPageService;

        public IndexModel(IArticleService articleService, IDynamicPageService dynamicPageService)
        {
            _articleService = articleService;
            _dynamicPageService = dynamicPageService;
        }

        [BindProperty]
        public List<ArticleSelectDto> ArticleList { get; set; } = new List<ArticleSelectDto>();


        [BindProperty]
        public List<DynamicPageSelectDto> DynamicPageList { get; set; } = new List<DynamicPageSelectDto>();
        public async Task OnGetAsync(CancellationToken cancellationToken)
        {

            var articles = await _articleService.List(1,4,null,null,null, cancellationToken);
            ArticleList = articles.Model;


            var pages = await _dynamicPageService.List(1, 4, null,null, cancellationToken);
            DynamicPageList = pages.Model;

        }
    }
}
