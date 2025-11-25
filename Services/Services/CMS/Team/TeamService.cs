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

namespace Services.Services.CMS.Team
{
    public class TeamService : IScopedDependency ,ITeamService
    {
        private readonly IRepository<Entities.Team> _mainRepository;
        private readonly IMapper _mapper;

        public TeamService(IRepository<Entities.Team> mainRepository, IMapper mapper)
        {
            _mainRepository = mainRepository;
            _mapper = mapper;
        }



        /// <summary>
        /// Retrieves a paginated list of Teams.
        /// </summary>
        /// <param name="page">The Team number.</param>
        /// <param name="pageSize">The number of items per Team.</param>
        /// <param name="query">The search query.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A response containing a list of Teams.</returns>
        public async Task<ResponseModel<List<TeamSelectDto>>> List(int page, int pageSize, string query, bool? pin, CancellationToken cancellationToken)
        {
            // Validate pagination info
            var validatedPageInfo = PaginationValidation.Validate(page, pageSize);

            // Create a Pageres instance
            var paginationInfo = new Pageres
            {
                PageNumber = validatedPageInfo.Page,
                PageSize = validatedPageInfo.PageSize
            };

            // Query Galleris
            var SearchQuery = _mainRepository.TableNoTracking
                .Where(a => a.CmsLanguage == CmsEx.GetCurrentLanguage());


            // Order
            if (pin != null)
            {
                SearchQuery = SearchQuery.OrderBy(a => a.IsPin)
                .ThenByDescending(a => a.Id);
            }
            else
            {
                SearchQuery = SearchQuery.OrderBy(a => a.Id);
            }

            // Search

            if (!string.IsNullOrWhiteSpace(query))
            {
                SearchQuery = SearchQuery.Where(a => EF.Functions.Like(a.FullName, $"%{query}%"));
            }

            // Paginate, project, and retrieve the results
            var result = await SearchQuery
                .OrderBy(a => a.Id)
                .Paginate(paginationInfo)
                .ProjectTo<TeamSelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<TeamSelectDto>>(true, result);
        }
    }
}
