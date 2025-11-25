using Common;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using ResourceLibrary.Panel.Common.Global;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Menu
{
    public class MenuService : IScopedDependency, IMenuSerivce
    {
        private readonly IRepository<Entities.Menu> _repository;

        public MenuService(IRepository<Entities.Menu> repository, IStringLocalizer<ResourceLibrary.Panel.Common.Global.Global> localizer)
        {
            _repository = repository;
            _Localizer = localizer;
        }

        public IStringLocalizer<Global> _Localizer { get; }

        public async Task<ResponseModel<List<MenuItm>>> GetBySlug(string Slug, CancellationToken cancellationToken)
        {
            var res = await _repository.TableNoTracking.Where(a => a.Slug == Slug).FirstOrDefaultAsync();
            if (res == null || res.Content == null)
            {
                return new ResponseModel<List<MenuItm>>(false, null, _Localizer["NothingFound"]);
            }
            else
            {
                try
                {
                    List<MenuItm> MenuItems = JsonConvert.DeserializeObject<List<MenuItm>>(res.Content);
                    return new ResponseModel<List<MenuItm>>(true, MenuItems, "");
                }
                catch
                {

                    return new ResponseModel<List<MenuItm>>(false, null, _Localizer["NothingFound"]);
                }
            }
        }
    }
}
