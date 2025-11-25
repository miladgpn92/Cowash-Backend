using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Utilities;
using DariaCMS.Common;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Services.CMS.Article;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.DynamicPage
{
    public class DynamicPageService : IScopedDependency, IDynamicPageService
    {

        private readonly IRepository<Entities.DynamicPage> _pageRepository;
        private readonly IMapper _mapper;

        public DynamicPageService(IRepository<Entities.DynamicPage> pageRepository, IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel> CreateForAI(Entities.DynamicPage model, CancellationToken cancellationToken)
        {
            try
            {
                
            
                await _pageRepository.AddAsync(model, cancellationToken);
                return new ResponseModel(true, "");
            }
            catch (Exception E)
            {
                return new ResponseModel(false, E.InnerException.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves an page by its slug.
        /// </summary>
        /// <param name="slug">The slug of the page.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A response containing the page or an error message.</returns>
        public async Task<ResponseModel<DynamicPageSelectDto>> Get(string slug, CancellationToken cancellationToken)
        {
            var page = await _pageRepository.TableNoTracking
                .Where(a => a.Slug == slug)
                .ProjectTo<DynamicPageSelectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (page.PublishDate != null && page.PublishDate > DateTime.Now)
            {
                page = null;
            }


            return page == null
                ? new ResponseModel<DynamicPageSelectDto>(false, null, "Page not found.")
                : new ResponseModel<DynamicPageSelectDto>(true, page);
        }

        /// <summary>
        /// Retrieves a paginated list of pages.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="query">The search query.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A response containing a list of pages.</returns>
        public async Task<ResponseModel<List<DynamicPageSelectDto>>> List(int page, int pageSize, string query, bool? pin, CancellationToken cancellationToken)
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
            var PagesQuery = _pageRepository.TableNoTracking
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
                .ProjectTo<DynamicPageSelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<DynamicPageSelectDto>>(true, pages);
        }

    }
}
