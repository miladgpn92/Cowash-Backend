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

namespace Services.Services.CMS.PortfolioCategory
{
    public class PortfolioCategoryService : IScopedDependency, IPortfolioCategoryService
    {

        private readonly IRepository<Entities.PortfolioCategory> _mainRepository;
        private readonly IMapper _mapper;

        public PortfolioCategoryService(IRepository<Entities.PortfolioCategory> MainRepository, IMapper mapper)
        {
            _mainRepository = MainRepository;
            _mapper = mapper;
        }


        public async Task<ResponseModel<PortfolioCategorySelectDto>> Get(string slug, CancellationToken cancellationToken)
        {
            var page = await _mainRepository.TableNoTracking
               .Where(a => a.Slug == slug)
               .ProjectTo<PortfolioCategorySelectDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(cancellationToken);

            if (page.PublishDate != null && page.PublishDate > DateTime.Now)
            {
                page = null;
            }


            return page == null
                ? new ResponseModel<PortfolioCategorySelectDto>(false, null, "Page not found.")
                : new ResponseModel<PortfolioCategorySelectDto>(true, page);
        }

        public async Task<ResponseModel<List<PortfolioCategorySelectDto>>> List(int page, int pageSize, string query, bool? pin, CancellationToken cancellationToken)
        {
            // Validate pagination info
            var validatedPageInfo = PaginationValidation.Validate(page, pageSize);

            // Create a Pageres instance
            var paginationInfo = new Pageres
            {
                PageNumber = validatedPageInfo.Page,
                PageSize = validatedPageInfo.PageSize
            };

            // Query pages
            var PagesQuery = _mainRepository.TableNoTracking
                .Where(a => a.CmsLanguage == CmsEx.GetCurrentLanguage());

            // Order
            if (pin != null)
            {
                PagesQuery = PagesQuery.OrderByDescending(a => a.IsPin)
                .ThenByDescending(a => a.Id);
            }
            else
            {
                PagesQuery = PagesQuery.OrderByDescending(a => a.Id);
            }

            // Search

            if (!string.IsNullOrWhiteSpace(query))
            {
                PagesQuery = PagesQuery.Where(a => EF.Functions.Like(a.Title, $"%{query}%"));
            }

            // Paginate, project, and retrieve the results
            var pages = await PagesQuery
                .Paginate(paginationInfo)
                .ProjectTo<PortfolioCategorySelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<PortfolioCategorySelectDto>>(true, pages);
        }
    }
}
