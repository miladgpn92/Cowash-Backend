using Common;
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
    public interface IPortfolioService
    {
        Task<ResponseModel<PortfolioSelectDto>> Get(string slug, CancellationToken cancellationToken);

        Task<ResponseModel<List<PortfolioSelectDto>>> List(int page, int pageSize, string query, bool? pin, int? CatId, CancellationToken cancellationToken);
    }
}
