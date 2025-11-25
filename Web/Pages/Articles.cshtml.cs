using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CMS.Article;
using Services.Services.CMS.ArticleCategory;
using SharedModels.Dtos;
using Web.Pages.Template.Components.Pagination;

namespace Web.Pages
{
    public class ArticlesModel : PageModel
    {


        private readonly IArticleService _service;

        public ArticlesModel(IArticleService service , IArticleCategoryService categoryService)
        {
            this._service = service;
            _CategoryService = categoryService;
        }
        [BindProperty]
        public List<ArticleSelectDto> Items { get; set; } = new();


        [BindProperty]
        public List<ArticleSelectDto> PinItems { get; set; } = new();

        [BindProperty]
        public List<ArticleCategorySelectDto> CatItems { get; set; } = new();


        [BindProperty]
        public ArticleCategorySelectDto? SelectedCat { get; set; } = new();

        [BindProperty]
        public VCPagination Pagination { get; set; } = new();

        [BindProperty]
        public string Query { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Cat { get; set; }

        public IArticleCategoryService _CategoryService { get; }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int pageSize = 12)
        {
            VCPagination vCPagination = new VCPagination()
            {
                Page = page,
                PageSize = pageSize
            };

            SEODto PageSeo = new SEODto()
            {
                SEOTitle = "اخبار",
                SEODesc = "اخبار",
            };

            ViewData["Seo"] = PageSeo;


            if (string.IsNullOrEmpty(Cat))
            {
                var res = await _service.List(page, pageSize, null, null,null, cancellationToken);
                if (res.IsSuccess)
                {
                    Items = res.Model;
                    vCPagination.Total = Items.Count;
                }
                Pagination = vCPagination;



                var PinRes = await _service.List(1, 1, null, true,null, cancellationToken);
                if (PinRes.IsSuccess)
                {
                    PinItems = PinRes.Model;
                }
            }
            else
            {
               var SingleCatRes = await _CategoryService.Get(Cat,cancellationToken);
                if (SingleCatRes.IsSuccess)
                {
                    SelectedCat = SingleCatRes.Model;
                    if(SelectedCat != null)
                    {
                        var res = await _service.List(page, pageSize, null, null, SelectedCat.Id, cancellationToken);
                        if (res.IsSuccess)
                        {
                            Items = res.Model;
                            vCPagination.Total = Items.Count;
                        }
                        Pagination = vCPagination;
                    }
             
                }
                else
                {
                    return NotFound();
                }
            }


          

            var CatRes = await _CategoryService.List(1, 100, null, null, cancellationToken);
            if (CatRes.IsSuccess)
            {
                CatItems = CatRes.Model;
            }


            return Page();

        }
    }
}
