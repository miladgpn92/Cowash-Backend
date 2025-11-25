using Common;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Article
{
    public interface IArticleService
    {
        Task<ResponseModel<ArticleSelectDto>> Get(string slug, CancellationToken cancellationToken);

        Task<ResponseModel<List<ArticleSelectDto>>> List(int page, int pageSize, string query, bool? pin,int? CatId, CancellationToken cancellationToken);

    }
}
