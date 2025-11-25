using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS.Article;
using SharedModels.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Pages.Template.Components.ViewComponents
{
    public class ArticlesListViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;

        public ArticlesListViewComponent(IArticleService  articleService)
        {
            this._articleService = articleService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List< ArticleSelectDto> model = new();
            var res =await _articleService.List(1, 3, null,true,null, HttpContext.RequestAborted);
            if (res.IsSuccess)
            {
                model = res.Model;
            }
             return View("/Pages/Template/Components/Components/ArticlesList/Index.cshtml" , model);
        }
    }
}
