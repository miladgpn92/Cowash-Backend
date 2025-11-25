using AutoMapper;
using Common.Consts;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.CMS;
using Shared.Api;
using SharedModels.Dtos;
using System.Data;

namespace Web.Api.Menu
{
    [ApiVersion("1")]
    [Authorize(Roles = RoleConsts.Admin)]
    public class MenuController : CrudController<MenuDto, MenuSelectDto, Entities.Menu>
    {
        private readonly ISlugService<Entities.Menu> _slugService;
        public MenuController(IRepository<Entities.Menu> repository, IMapper mapper , ISlugService<Entities.Menu> slugService) : base(repository, mapper)
        {
            _slugService = slugService;
        }

        public override Task<ApiResult<MenuSelectDto>> Create(MenuDto dto, CancellationToken cancellationToken)
        {
            ////Check Slug
            dto.Slug = _slugService.CheckSlug(dto.Slug, cancellationToken);
            return base.Create(dto, cancellationToken);
        }
    }
}
