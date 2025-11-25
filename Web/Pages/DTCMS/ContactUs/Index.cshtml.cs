using AutoMapper;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SharedModels.Dtos;
using Shared;
using Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using SharedModels;

namespace Web.Pages.DTCMS.ContactUs
{
    [Authorize]
    public class IndexModel : GenericBasePageModel<ContactUsDto, Entities.ContactUs.ContactUs>
    {
        public IndexModel(IRepository<Entities.ContactUs.ContactUs> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<IActionResult> OnGetAsync(PageListModel model, string filter, CancellationToken cancellationToken)
        {
            return base.OnGetAsync(model, filter, cancellationToken);
        }
    }
}
