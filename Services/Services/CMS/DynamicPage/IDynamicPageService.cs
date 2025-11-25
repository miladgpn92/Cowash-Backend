using Common;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.DynamicPage
{
    public interface IDynamicPageService
    {
        Task<ResponseModel> CreateForAI( Entities.DynamicPage model, CancellationToken cancellationToken);
        Task<ResponseModel<DynamicPageSelectDto>> Get(string slug, CancellationToken cancellationToken);
        Task<ResponseModel<List<DynamicPageSelectDto>>> List(int page, int pageSize, string query, bool? pin, CancellationToken cancellationToken);
    }
}
