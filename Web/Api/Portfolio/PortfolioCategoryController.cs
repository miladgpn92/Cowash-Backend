using AutoMapper;
using Common.Consts;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS;
using Shared.Api;
using SharedModels.Dtos;
using SharedModels.Dtos.Shared;
using System.Data;

namespace Web.Api.PortfolioCategory
{

    [ApiVersion("1")]
    [Authorize(Roles = RoleConsts.Admin)]
    public class PortfolioCategoryController : SEOCrudController<PortfolioCategoryDto, PortfolioCategorySelectDto, Entities.PortfolioCategory>
    {

        private readonly ISlugService<Entities.PortfolioCategory> _slugService;

        public PortfolioCategoryController(IRepository<Entities.PortfolioCategory> repository, IMapper mapper, ISlugService<Entities.PortfolioCategory> slugService) : base(repository, mapper)
        {

            _slugService = slugService;
        }


        public override Task<ApiResult<PortfolioCategorySelectDto>> Create(PortfolioCategoryDto dto, CancellationToken cancellationToken)
        {

            ////Check Slug
            dto.Slug = _slugService.CheckSlug(dto.Slug, cancellationToken);
            return base.Create(dto, cancellationToken);
        }
    }
}
