using AutoMapper;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedModels.Dtos;
using SharedModels;
using Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.DTCMS.DynamicPage
{
    [Authorize]
    public class IndexModel : GenericPageModel<DynamicPageSelectDto, Entities.DynamicPage>
    {
        public IndexModel(IRepository<Entities.DynamicPage> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<IActionResult> OnGetAsync(PageListModel model, string filter, CancellationToken cancellationToken)
        {
            return base.OnGetAsync(model, filter, cancellationToken);
        }
    }
}
