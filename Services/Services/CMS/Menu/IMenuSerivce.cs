using Common;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Menu
{
    public interface IMenuSerivce
    {
        Task<ResponseModel<List<MenuItm>>> GetBySlug(string Slug, CancellationToken cancellationToken);
    }
}
