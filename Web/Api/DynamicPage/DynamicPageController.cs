using AutoMapper;
using Common.Consts;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS;
using Shared.Api;
using SharedModels.Api;
using SharedModels.Dtos;
using System.Data;

namespace Web.Api.DynamicPage
{
    [ApiVersion("1")]
    [Authorize(Roles = RoleConsts.Admin)]
    public class DynamicPageController : SEOCrudController<DynamicPageDto, DynamicPageSelectDto, Entities.DynamicPage>
    {
        private readonly ISlugService<Entities.DynamicPage> _slugService;

        public DynamicPageController(IRepository<Entities.DynamicPage> repository, IMapper mapper,ISlugService<Entities.DynamicPage> slugService) : base(repository, mapper)
        {
            _slugService = slugService;
        }

        public override  Task<ApiResult<DynamicPageSelectDto>> Create(DynamicPageDto dto, CancellationToken cancellationToken)
        {

            ////Check Slug
            dto.Slug = _slugService.CheckSlug(dto.Slug, cancellationToken);
            return base.Create(dto, cancellationToken);
        }
    }

   

}
