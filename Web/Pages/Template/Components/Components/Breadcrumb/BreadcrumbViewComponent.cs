using Microsoft.AspNetCore.Mvc;
using SharedModels.Dtos.Shared;

namespace Web.Pages.Template.Components.Components.Breadcrumb
{
    public class BreadcrumbViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(string Title, List<BreadcrumbItem> model)
        {
            BreadcrumbVCModel breadcrumbVCModel = new BreadcrumbVCModel()
            {
                Title = Title,
                Breadcrumbs = model
            };  

            return View("/Pages/Template/Components/Components/Breadcrumb/Index.cshtml", breadcrumbVCModel);
        }
    }

    public class BreadcrumbVCModel
    {
        public string Title { get; set; }
        public  List<BreadcrumbItem> Breadcrumbs { get; set; } = new();
    }

 
}
public class BreadcrumbItem
{
    public string Title { get; set; }
    public string Link { get; set; }
}
