using AutoMapper;
using Common.Consts;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using SharedModels.Dtos;
using System.Data;

namespace Web.Api.Slider
{
    [ApiVersion("1")]
    [Authorize(Roles = RoleConsts.Admin)]
    public class SliderController : CrudController<SliderDto, SliderSelectDto, Entities.Slider>
    {
        public SliderController(IRepository<Entities.Slider> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
