using Common;
using SharedModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.ContactUs
{
    public interface IContactUsService
    {
        Task<ResponseModel> Create(ContactUsDto model, CancellationToken cancellationToken);
    }
}
