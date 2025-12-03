using AutoMapper;
using Common.Consts;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS;
using Shared.Api;
using SharedModels.Dtos.Shared;

namespace Web.Api.Product
{
  

    [ApiVersion("1")]
    [Authorize(Roles = RoleConsts.Admin)]
    public class ProductCategoryController : SEOCrudController<ProductCategoryDto, ProductCategorySelectDto, Entities.ProductCategory>
    {

        private readonly ISlugService<Entities.ProductCategory> _slugService;

        public ProductCategoryController(IRepository<Entities.ProductCategory> repository, IMapper mapper, ISlugService<Entities.ProductCategory> slugService) : base(repository, mapper)
        {

            _slugService = slugService;
        }


        public override Task<ApiResult<ProductCategorySelectDto>> Create(ProductCategoryDto dto, CancellationToken cancellationToken)
        {

            ////Check Slug
            dto.Slug = _slugService.CheckSlug(dto.Slug, cancellationToken);
            return base.Create(dto, cancellationToken);
        }
    }
}
