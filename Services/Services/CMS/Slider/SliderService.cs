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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Services.CMS.Slider
{
    public class SliderService : ISliderService, IScopedDependency
    {
        private readonly IRepository<Entities.Slider> _repositorySlider;
        private readonly IMapper _mapper;
        public SliderService(IRepository<Entities.Slider> RepositorySlider , IMapper mapper)
        {
            _repositorySlider = RepositorySlider;
            _mapper = mapper;
        }
        public async Task<ResponseModel<List<SliderSelectDto>>> List(int page, int pageSize, CancellationToken cancellationToken)
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
            var SearchQuery = _repositorySlider.TableNoTracking
            .Where(a => a.CmsLanguage == CmsEx.GetCurrentLanguage());

            // Order

            
                SearchQuery = SearchQuery.OrderByDescending(a => a.Id);
           

          

            // Paginate, project, and retrieve the results
            var result = await SearchQuery
                .Paginate(paginationInfo)
                .ProjectTo<SliderSelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<SliderSelectDto>>(true, result);
        }
    }
}
