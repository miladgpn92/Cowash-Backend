using AutoMapper;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedModels.Dtos;
using SharedModels;
using Web.Infrastructure;
using SharedModels.Dtos.Shared;

namespace Web.Pages.DTCMS.Portfolio
{
    [Authorize]
    public class IndexModel : GenericPageModel<PortfolioSelectDto, Entities.Portfolio>
    {
        public IndexModel(IRepository<Entities.Portfolio> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<IActionResult> OnGetAsync(PageListModel model, string filter, CancellationToken cancellationToken)
        {
            return base.OnGetAsync(model, filter, cancellationToken);
        }
    }
}
