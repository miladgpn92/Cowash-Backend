using Common;
using SharedModels.Dtos.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Product
{
    public interface IProductService
    {
        Task<ResponseModel<List<ProductSelectDto>>> ListAsync(int page, int pageSize, string query, int? categoryId, CancellationToken cancellationToken);

        Task<ResponseModel<ProductSelectDto>> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<ResponseModel<ProductSelectDto>> CreateAsync(ProductDto dto, CancellationToken cancellationToken);

        Task<ResponseModel<ProductSelectDto>> UpdateAsync(int id, ProductDto dto, CancellationToken cancellationToken);

        Task<ResponseModel> DeleteAsync(List<int> ids, CancellationToken cancellationToken);
    }
}
