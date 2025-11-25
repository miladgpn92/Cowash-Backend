using AutoMapper;
using Common.Consts;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS;
using SharedModels;
using Shared.Api;
using SharedModels.Dtos;
using System.Data;

namespace Web.Api.Article
{
    [ApiVersion("1")]
    [Authorize(Roles = RoleConsts.Admin)]
    public class ArticleController : SEOCrudController<ArticleDto, ArticleSelectDto, Entities.Article>
    {
        private readonly ISlugService<Entities.Article> _slugService;
        public ArticleController(IRepository<Entities.Article> repository, IMapper mapper , ISlugService<Entities.Article> slugService) : base(repository, mapper)
        {
            _slugService = slugService;
        }

        public override Task<ApiResult<ArticleSelectDto>> Create(ArticleDto dto, CancellationToken cancellationToken)
        {
            ////Check Slug
            dto.Slug = _slugService.CheckSlug(dto.Slug, cancellationToken);
            return base.Create(dto, cancellationToken);
        }

        [AllowAnonymous]
        public override Task<ActionResult<List<ArticleSelectDto>>> List([FromBody] PageListModel model, CancellationToken cancellationToken)
        {
            return base.List(model, cancellationToken);
        }
    }
}
