using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Services.CMS;
using SharedModels.Dtos;
using SharedModels.Dtos.Shared;

namespace Web.Pages.DTCMS.PortfolioCategory
{
    [Authorize]
    public class UpsertModel : PageModel
    {
        private readonly IRepository<Entities.PortfolioCategory> _repository;
        private readonly ISlugService<Entities.PortfolioCategory> _slugService;

        public UpsertModel(IRepository<Entities.PortfolioCategory> repository, ISlugService<Entities.PortfolioCategory> slugService, IMapper mapper)
        {
            _repository = repository;
            _slugService = slugService;
            Mapper = mapper;
        }

        [BindProperty]
        public PortfolioCategoryDto? Items { get; set; } = new();

        [BindProperty]
        public bool IsEdit { get; set; }
        public IMapper Mapper { get; }

        public async Task OnGetAsync(int? id, CancellationToken cancellationToken)
        {
            if (id.HasValue)
            {
                IsEdit = true;

                Items = await _repository.TableNoTracking.ProjectTo<PortfolioCategoryDto>(Mapper.ConfigurationProvider)
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

                if (model.Slug != Items.Slug)
                {
                    if (Items.Slug != null)
                        Items.Slug = _slugService.CheckSlug(Items.Slug, cancellationToken);
                }

                model = Items?.ToEntity(Mapper, model);
                if (model != null)
                    await _repository.UpdateAsync(model, cancellationToken);

            }
            else
            {
                if (Items?.Slug != null)
                    Items.Slug = _slugService.CheckSlug(Items.Slug, cancellationToken);
                var model = Items?.ToEntity(Mapper);
                if (model != null)
                    await _repository.AddAsync(model, cancellationToken);

            }



            return RedirectToPage("./Index");
        }




    }
}
