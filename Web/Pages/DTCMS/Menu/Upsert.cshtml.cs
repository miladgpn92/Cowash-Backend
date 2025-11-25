using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SharedModels.Dtos;

namespace Web.Pages.DTCMS.Menu
{
    [Authorize]
    public class UpsertModel : PageModel
    {
        private readonly IRepository<Entities.Menu> _repository;


        public UpsertModel(IRepository<Entities.Menu> repository, IMapper mapper)
        {
            _repository = repository;
            Mapper = mapper;
        }

        [BindProperty]
        public MenuDto? Items { get; set; } = new();

        [BindProperty]
        public bool IsEdit { get; set; }
        public IMapper Mapper { get; }

        public async Task OnGetAsync(int? id, CancellationToken cancellationToken)
        {
            if (id.HasValue)
            {
                IsEdit = true;

                Items = await _repository.TableNoTracking.ProjectTo<MenuDto>(Mapper.ConfigurationProvider)
                  .SingleOrDefaultAsync(p => p.Id.Equals(id), cancellationToken);
            }
            else
                IsEdit = false;
        }


        public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }



            if (IsEdit)
            {
                var model = await _repository.GetByIdAsync(cancellationToken, Items?.Id);

                model = Items?.ToEntity(Mapper, model);

                if (model != null)
                    await _repository.UpdateAsync(model, cancellationToken);


            }
            else
            {
                var model = Items?.ToEntity(Mapper);
                if (model != null)
                    await _repository.AddAsync(model, cancellationToken);

            }



            return RedirectToPage("./Index");
        }


    }
}
