using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Utilities;
using DariaCMS.Common;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared;
using SharedModels;
using SharedModels.Dtos;
using System.Threading;
using Web.Infrastructure;

namespace Web.Pages.DTCMS.ArticleCategories
{
    [Authorize]

    public class IndexModel : GenericPageModel<ArticleCategorySelectDto, ArticleCategory>
    {
        public IndexModel(IRepository<ArticleCategory> repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<IActionResult> OnGetAsync(PageListModel model, string filter, CancellationToken cancellationToken)
        {
            return base.OnGetAsync(model, filter, cancellationToken);
        }
    }
}

