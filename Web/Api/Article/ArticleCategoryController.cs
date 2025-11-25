using AutoMapper;
using Common.Consts;
using Common.Enums;
using Common.Utilities;
using DariaCMS.Common;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS;
using Shared.Api;
using SharedModels;
using SharedModels.Api;
using SharedModels.Dtos;

namespace Web.Api
{
    [ApiVersion("1")]
    [Authorize(Roles =RoleConsts.Admin)]
    public class ArticleCategoryController : SEOCrudController<ArticleCategoryDto, ArticleCategorySelectDto, Entities.ArticleCategory>
    {
  
        private readonly ISlugService<ArticleCategory> _slugService;

        public ArticleCategoryController(IRepository<ArticleCategory> repository, IMapper mapper,ISlugService<ArticleCategory> slugService) : base(repository, mapper)
        {
        
           _slugService = slugService;
        }


        public override Task<ApiResult<ArticleCategorySelectDto>> Create(ArticleCategoryDto dto, CancellationToken cancellationToken)
        {

            ////Check Slug
            dto.Slug = _slugService.CheckSlug(dto.Slug, cancellationToken);
            return base.Create(dto, cancellationToken);
        }
    }
}
