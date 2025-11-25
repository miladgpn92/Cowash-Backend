using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Utilities;
using DariaCMS.Common;
using Data.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using SharedModels;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Article
{
    /// <summary>
    /// Provides services for managing articles in the CMS.
    /// </summary>
    public class ArticleService : IScopedDependency, IArticleService
    {
        private readonly IRepository<Entities.Article> _articleRepository;
        private readonly IMapper _mapper;

        public ArticleService(IRepository<Entities.Article> articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves an article by its slug.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A response containing the article or an error message.</returns>
        public async Task<ResponseModel<ArticleSelectDto>> Get(string slug, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.TableNoTracking
                .Where(a => a.Slug == slug)
                .ProjectTo<ArticleSelectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if(article.PublishDate != null && article.PublishDate > DateTime.Now)
            {
                article = null;
            }

            return article == null
                ? new ResponseModel<ArticleSelectDto>(false, null, "Article not found.")
                : new ResponseModel<ArticleSelectDto>(true, article);
        }

        /// <summary>
        /// Retrieves a paginated list of articles.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="query">The search query.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A response containing a list of articles.</returns>
        public async Task<ResponseModel<List<ArticleSelectDto>>> List(int page, int pageSize, string query, bool? pin, int? CatId, CancellationToken cancellationToken)
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
                .Where(a => a.CmsLanguage == CmsEx.GetCurrentLanguage() && a.PublishDate < DateTime.Now);

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

            if(CatId != null)
            {
                articlesQuery = articlesQuery.Where(a=> a.ArticleCategoryId == CatId);  
            }

            // Paginate, project, and retrieve the results
            var articles = await articlesQuery
                .Paginate(paginationInfo)
                .ProjectTo<ArticleSelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<ArticleSelectDto>>(true, articles);
        }



    }
}
