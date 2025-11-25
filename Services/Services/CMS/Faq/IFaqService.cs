using Common;
using SharedModels.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Faq
{
    public interface IFaqService
    {
        Task<ResponseModel<List<FaqSelectDto>>> List(int page, int pageSize, string query, bool? pin, CancellationToken cancellationToken);
    }
}
