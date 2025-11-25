using Common;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.ArticleCategory
{
    public interface IArticleCategoryService
    {
        Task<ResponseModel<ArticleCategorySelectDto>> Get(string slug, CancellationToken cancellationToken);

        Task<ResponseModel<List<ArticleCategorySelectDto>>> List(int page, int pageSize, string query, bool? pin, CancellationToken cancellationToken);
    }
}
