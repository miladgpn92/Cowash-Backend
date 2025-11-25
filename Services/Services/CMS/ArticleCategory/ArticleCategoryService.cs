using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Utilities;
using DariaCMS.Common;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.ArticleCategory
{
    public class ArticleCategoryService : IScopedDependency, IArticleCategoryService
    {

        private readonly IRepository<Entities.ArticleCategory> _articleRepository;
        private readonly IMapper _mapper;

        public ArticleCategoryService(IRepository<Entities.ArticleCategory> articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<ResponseModel<ArticleCategorySelectDto>> Get(string slug, CancellationToken cancellationToken)
        {
            var articleCat = await _articleRepository.TableNoTracking
               .Where(a => a.Slug == slug)
               .ProjectTo<ArticleCategorySelectDto>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(cancellationToken);
            if (articleCat != null && articleCat.PublishDate != null && articleCat.PublishDate < DateTime.Now)
            {
                articleCat = null;
            }

            return articleCat == null
                ? new ResponseModel<ArticleCategorySelectDto>(false, null, "ArticleCategory not found.")
                : new ResponseModel<ArticleCategorySelectDto>(true, articleCat); throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<ArticleCategorySelectDto>>> List(int page, int pageSize, string query, bool? pin, CancellationToken cancellationToken)
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
            var articlesQuery = _articleRepository.TableNoTracking
                .Where(a => a.CmsLanguage == CmsEx.GetCurrentLanguage() &&
               (a.PublishDate == null || a.PublishDate < DateTime.Now));

            // Order
            if (pin != null)
            {
                articlesQuery = articlesQuery.OrderByDescending(a => a.IsPin)
                .ThenByDescending(a => a.Id);
            }
            else
            {
                articlesQuery = articlesQuery.OrderByDescending(a => a.Id);
            }

            // Search

            if (!string.IsNullOrWhiteSpace(query))
            {
                articlesQuery = articlesQuery.Where(a => EF.Functions.Like(a.Title, $"%{query}%"));
            }

            // Paginate, project, and retrieve the results
            var articles = await articlesQuery
                .Paginate(paginationInfo)
                .ProjectTo<ArticleCategorySelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<ArticleCategorySelectDto>>(true, articles);
        }
    }
}
