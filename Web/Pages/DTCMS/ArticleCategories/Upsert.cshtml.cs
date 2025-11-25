using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using Services.Services.CMS;
using Shared;
using SharedModels.Dtos;
using System.Threading;

namespace Web.Pages.DTCMS.ArticleCategories
{
    [Authorize]
    public class UpsertModel : PageModel
    {
        private readonly IRepository<ArticleCategory> _repository;
        private readonly ISlugService<ArticleCategory> _slugService;

        public UpsertModel(IRepository<Entities.ArticleCategory> repository, ISlugService<ArticleCategory> slugService, IMapper mapper)
        {
            _repository = repository;
            _slugService = slugService;
            Mapper = mapper;
        }

        [BindProperty]
        public ArticleCategoryDto? Items { get; set; } = new();

        [BindProperty]
        public bool IsEdit { get; set; }
        public IMapper Mapper { get; }

        public async Task OnGetAsync(int? id, CancellationToken cancellationToken)
        {
            if (id.HasValue)
            {
                IsEdit = true;

                Items = await _repository.TableNoTracking.ProjectTo<ArticleCategoryDto>(Mapper.ConfigurationProvider)
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
                if(model != null)
                  await _repository.UpdateAsync(model, cancellationToken);

            }
            else
            {
                if (Items?.Slug != null)
                    Items.Slug = _slugService.CheckSlug(Items.Slug, cancellationToken);
                var model = Items?.ToEntity(Mapper);
                if(model != null)
                  await _repository.AddAsync(model, cancellationToken);
 
            }



            return RedirectToPage("./Index");
        }


       

    }
}
