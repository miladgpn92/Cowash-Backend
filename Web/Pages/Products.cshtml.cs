using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services.CMS.Article;
using Services.Services.CMS.ArticleCategory;
using Services.Services.CMS.Product;
using Services.Services.CMS.ProductCategory;
using SharedModels.Dtos;
using SharedModels.Dtos.Shared;
using Web.Pages.Template.Components.Pagination;

namespace Web.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProductService _service;

        public ProductsModel(IProductService service, IProductCategoryService categoryService)
        {
            this._service = service;
            _CategoryService = categoryService;
        }
        [BindProperty]
        public List<ProductSelectDto> Items { get; set; } = new();


        [BindProperty]
        public List<ProductSelectDto> PinItems { get; set; } = new();

        [BindProperty]
        public List<ProductCategorySelectDto> CatItems { get; set; } = new();


        [BindProperty]
        public ProductCategorySelectDto? SelectedCat { get; set; } = new();

        [BindProperty]
        public VCPagination Pagination { get; set; } = new();

        [BindProperty]
        public string Query { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Cat { get; set; }

        public IProductCategoryService _CategoryService { get; }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int pageSize = 12)
        {
            VCPagination vCPagination = new VCPagination()
            {
                Page = page,
                PageSize = pageSize
            };

            SEODto PageSeo = new SEODto()
            {
                SEOTitle = "محصولات",
                SEODesc = "محصولات",
            };

            ViewData["Seo"] = PageSeo;


            if (string.IsNullOrEmpty(Cat))
            {
                var res = await _service.ListAsync(page, pageSize, null, null, cancellationToken);
                if (res.IsSuccess)
                {
                    Items = res.Model;
                    vCPagination.Total = Items.Count;
                }
                Pagination = vCPagination;



                 
            }
            else
            {
                var SingleCatRes = await _CategoryService.GetBySlugAsync(Cat, cancellationToken);
                if (SingleCatRes.IsSuccess)
                {
                    SelectedCat = SingleCatRes.Model;
                    if (SelectedCat != null)
                    {
                        var res = await _service.ListAsync(page, pageSize, null, SelectedCat.Id, cancellationToken);
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




            var CatRes = await _CategoryService.ListAsync(1, 100, null, cancellationToken);
            if (CatRes.IsSuccess)
            {
                CatItems = CatRes.Model;
            }


            return Page();

        }
    }
}
