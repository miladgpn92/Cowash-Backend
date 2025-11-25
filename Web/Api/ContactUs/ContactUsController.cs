using AutoMapper;
using Common.Consts;
using Data.Repositories;
using Entities.ContactUs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Api;
using SharedModels.Dtos;
using System.Data;

namespace Web.Api.ContactUs
{
    [ApiVersion("1")]
    [Authorize(Roles = RoleConsts.Admin)]
    public class ContactUsController : CrudController<ContactUsDto, ContactUsSelectDto, Entities.ContactUs.ContactUs>
    {
        public ContactUsController(IRepository<Entities.ContactUs.ContactUs> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        [NonAction]
        public override Task<ApiResult<ContactUsSelectDto>> Update(int id, ContactUsDto dto, CancellationToken cancellationToken)
        {
            return base.Update(id, dto, cancellationToken);
        }
    }
}
