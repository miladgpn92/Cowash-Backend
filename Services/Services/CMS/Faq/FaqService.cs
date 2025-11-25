using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Utilities;
using DariaCMS.Common;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using SharedModels.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Faq
{
    public class FaqService : IScopedDependency, IFaqService
    {

        private readonly IRepository<Entities.Faq> _mainRepository;
        private readonly IMapper _mapper;

        public FaqService(IRepository<Entities.Faq> mainRepository, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _mapper = mapper;
        }


        public async Task<ResponseModel<List<FaqSelectDto>>> List(int page, int pageSize, string query, bool? pin, CancellationToken cancellationToken)
        {
            // Validate pagination info
            var validatedPageInfo = PaginationValidation.Validate(page, pageSize);

            // Create a Pageres instance
            var paginationInfo = new Pageres
            {
                PageNumber = validatedPageInfo.Page,
                PageSize = validatedPageInfo.PageSize
            };

            // Query 
            var SearchQuery = _mainRepository.TableNoTracking
            .Where(a => a.CmsLanguage == CmsEx.GetCurrentLanguage());

            // Order

            if (pin != null)
            {
                SearchQuery = SearchQuery.OrderByDescending(a => a.IsPin)
                .ThenByDescending(a => a.Id);
            }
            else
            {
                SearchQuery = SearchQuery.OrderByDescending(a => a.Id);
            }

            // Search

            if (!string.IsNullOrWhiteSpace(query))
            {
                SearchQuery = SearchQuery.Where(a => EF.Functions.Like(a.Title, $"%{query}%"));
            }

            // Paginate, project, and retrieve the results
            var result = await SearchQuery
                .Paginate(paginationInfo)
                .ProjectTo<FaqSelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<FaqSelectDto>>(true, result);
        }
    }
}
