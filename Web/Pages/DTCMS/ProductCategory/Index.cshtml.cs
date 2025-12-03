using AutoMapper;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedModels;
using SharedModels.Dtos.Shared;
using Web.Infrastructure;

namespace Web.Pages.DTCMS.ProductCategory
{
    [Authorize]

    public class IndexModel : GenericPageModel<ProductCategorySelectDto, Entities.ProductCategory>
    {
        public IndexModel(IRepository<Entities.ProductCategory> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<IActionResult> OnGetAsync(PageListModel model, string filter, CancellationToken cancellationToken)
        {
            return base.OnGetAsync(model, filter, cancellationToken);
        }
    }
}
