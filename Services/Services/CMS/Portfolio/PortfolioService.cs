using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Utilities;
using DariaCMS.Common;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using SharedModels.Dtos;
using SharedModels.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Portfolio
{
    public class PortfolioService : IScopedDependency, IPortfolioService
    {

        private readonly IRepository<Entities.Portfolio> _mainRepository;
        private readonly IMapper _mapper;

        public PortfolioService(IRepository<Entities.Portfolio> mainRepository, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<PortfolioSelectDto>> Get(string slug, CancellationToken cancellationToken)
        {
            var portfolio = await _mainRepository.TableNoTracking
               .Where(a => a.Slug == slug)
               .ProjectTo<PortfolioSelectDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(cancellationToken);
            if (portfolio.PublishDate != null && portfolio.PublishDate > DateTime.Now)
            {
                portfolio = null;
            }

            return portfolio == null
                ? new ResponseModel<PortfolioSelectDto>(false, null, "portfolio not found.")
                : new ResponseModel<PortfolioSelectDto>(true, portfolio);
        }

        public async Task<ResponseModel<List<PortfolioSelectDto>>> List(int page, int pageSize, string query, bool? pin, int? CatId, CancellationToken cancellationToken)
        {
            // Validate pagination info
            var validatedPageInfo = PaginationValidation.Validate(page, pageSize);

            // Create a Pageres instance
            var paginationInfo = new Pageres
            {
                PageNumber = validatedPageInfo.Page,
                PageSize = validatedPageInfo.PageSize
            };

            // Query articles
            var MainQuery = _mainRepository.TableNoTracking
                .Where(a => a.CmsLanguage == CmsEx.GetCurrentLanguage() && a.PublishDate < DateTime.Now);

            // Order
            if (pin != null)
            {
                MainQuery = MainQuery.OrderByDescending(a => a.IsPin)
                .ThenByDescending(a => a.Id);
            }
            else
            {
                MainQuery = MainQuery.OrderByDescending(a => a.Id);
            }

            // Search

            if (!string.IsNullOrWhiteSpace(query))
            {
                MainQuery = MainQuery.Where(a => EF.Functions.Like(a.Title, $"%{query}%"));
            }

            if (CatId != null)
            {
                MainQuery = MainQuery.Where(a => a.PortfolioCategoryId == CatId);
            }

            // Paginate, project, and retrieve the results
            var resData = await MainQuery
                .Paginate(paginationInfo)
                .ProjectTo<PortfolioSelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<PortfolioSelectDto>>(true, resData);
        }
    }
}
