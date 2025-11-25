using AutoMapper;
using Common;
using Data.Repositories;
using Entities.ContactUs;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.ContactUs
{
    public class ContactUsService : IScopedDependency, IContactUsService
    {
        private readonly IRepository<Entities.ContactUs.ContactUs> _repository;

        public ContactUsService(IRepository<Entities.ContactUs.ContactUs> repository, IMapper mapper)
        {
            _repository = repository;
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

        public async Task<ResponseModel> Create(ContactUsDto model, CancellationToken cancellationToken)
        {
            var ContactUs = Mapper.Map<Entities.ContactUs.ContactUs>(model);
            ContactUs.ContactUsState = ContactUsState.unseen;
            ContactUs.CreatorUserId = 1;
            await _repository.AddAsync(ContactUs, cancellationToken);
            return new ResponseModel(true);
        }
    }
}
