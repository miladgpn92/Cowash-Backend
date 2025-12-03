using Microsoft.AspNetCore.Mvc;

namespace Web.Pages.Template.Components.Components.PageHeader
{
    public class PageHeaderViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(string title, string? subtitle = null, string? picUrl = null)
        {
            if (string.IsNullOrWhiteSpace(picUrl))
            {
                picUrl = "/Template/img/slider/1.png";
            }

            var model = new PageHeaderViewModel
            {
                Title = title,
                Subtitle = subtitle,
                PicUrl=picUrl
            };
            return View("/Pages/Template/Components/Components/PageHeader/Index.cshtml", model);
        }
    }
    public class PageHeaderViewModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }

        public string PicUrl { get; set; }
    }
}
