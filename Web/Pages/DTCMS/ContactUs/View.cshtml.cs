using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Services.CMS;
using SharedModels.Dtos;

namespace Web.Pages.DTCMS.ContactUs
{
    [Authorize]
    public class ViewModel : PageModel
    {

        private readonly IRepository<Entities.ContactUs.ContactUs> _repository;
    

        public ViewModel(IRepository<Entities.ContactUs.ContactUs> repository,  IMapper mapper)
        {
            _repository = repository;
            Mapper = mapper;
        }

        [BindProperty]
        public ContactUsSelectDto? Items { get; set; } = new();

      
        public IMapper Mapper { get; }

        public async Task<IActionResult> OnGetAsync(int? id, CancellationToken cancellationToken)
        {
            if (id.HasValue)
            {
                var item = await _repository.TableNoTracking.SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
                if(item == null)
                    return RedirectToPage("./Index");

                if (item.ContactUsState == Entities.ContactUs.ContactUsState.unseen)
                    item.ContactUsState = Entities.ContactUs.ContactUsState.seen;
                _repository.Update(item);

                Items = Mapper.Map<ContactUsSelectDto>(item);
                return Page();
            }
            else
                return RedirectToPage("./Index");
        }
    }
}
