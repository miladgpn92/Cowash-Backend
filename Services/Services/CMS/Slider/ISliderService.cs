using Common;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Slider
{
    public interface ISliderService
    {
        Task<ResponseModel<List<SliderSelectDto>>> List(int page, int pageSize, CancellationToken cancellationToken);

    }
}
