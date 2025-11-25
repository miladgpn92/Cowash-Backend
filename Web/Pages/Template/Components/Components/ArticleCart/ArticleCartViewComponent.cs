using Microsoft.AspNetCore.Mvc;
using SharedModels.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Pages.Template.Components.ViewComponents
{
    public class ArticleCartViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(ArticleSelectDto model , string? Class)
        {

            var VM = new ArticleCartVCModel()
            {
                Class = Class,
                Article = model
            };

            return View("/Pages/Template/Components/Components/ArticleCart/Index.cshtml", VM);
        }
    }

    public class ArticleCartVCModel
    {
        public string? Class { get; set; }

        public ArticleSelectDto Article { get; set; }
    }
}
 