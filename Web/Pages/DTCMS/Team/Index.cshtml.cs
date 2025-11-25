using AutoMapper;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedModels.Dtos.Shared;
using SharedModels;
using Web.Infrastructure;

namespace Web.Pages.DTCMS.Team
{
    [Authorize]
    public class IndexModel : GenericBasePageModel<TeamSelectDto, Entities.Team>
    {
        public IndexModel(IRepository<Entities.Team> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<IActionResult> OnGetAsync(PageListModel model, string filter, CancellationToken cancellationToken)
        {
            return base.OnGetAsync(model, filter, cancellationToken);
        }
    }
}
