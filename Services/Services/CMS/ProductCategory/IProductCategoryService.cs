using Common;
using SharedModels.Dtos.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.ProductCategory
{
    public interface IProductCategoryService
    {
        Task<ResponseModel<List<ProductCategorySelectDto>>> ListAsync(int page, int pageSize, string query, CancellationToken cancellationToken);

        Task<ResponseModel<ProductCategorySelectDto>> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<ResponseModel<ProductCategorySelectDto>> GetBySlugAsync(string Slug, CancellationToken cancellationToken);

        Task<ResponseModel<ProductCategorySelectDto>> CreateAsync(ProductCategoryDto dto, CancellationToken cancellationToken);

        Task<ResponseModel<ProductCategorySelectDto>> UpdateAsync(int id, ProductCategoryDto dto, CancellationToken cancellationToken);

        Task<ResponseModel> DeleteAsync(List<int> ids, CancellationToken cancellationToken);
    }
}
