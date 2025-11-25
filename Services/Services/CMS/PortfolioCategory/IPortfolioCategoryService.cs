using Common;
using SharedModels.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.PortfolioCategory
{
    public interface IPortfolioCategoryService
    {
        Task<ResponseModel<PortfolioCategorySelectDto>> Get(string slug, CancellationToken cancellationToken);

        Task<ResponseModel<List<PortfolioCategorySelectDto>>> List(int page, int pageSize, string query, bool? pin, CancellationToken cancellationToken);
    }
}
