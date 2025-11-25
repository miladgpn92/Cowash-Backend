using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Utilities;
using DariaCMS.Common;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Services.CMS;
using SharedModels.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.CMS.Product
{
    public class ProductService : IProductService, IScopedDependency
    {
        private readonly IRepository<Entities.Product> _repository;
        private readonly IMapper _mapper;
        private readonly ISlugService<Entities.Product> _slugService;

        public ProductService(IRepository<Entities.Product> repository, IMapper mapper, ISlugService<Entities.Product> slugService)
        {
            _repository = repository;
            _mapper = mapper;
            _slugService = slugService;
        }

        public async Task<ResponseModel<List<ProductSelectDto>>> ListAsync(int page, int pageSize, string query, int? categoryId, CancellationToken cancellationToken)
        {
            var validatedPageInfo = PaginationValidation.Validate(page, pageSize);

            var paginationInfo = new Pageres
            {
                PageNumber = validatedPageInfo.Page,
                PageSize = validatedPageInfo.PageSize
            };

            var searchQuery = _repository.TableNoTracking
                .Where(a => a.CmsLanguage == CmsEx.GetCurrentLanguage());

            if (!string.IsNullOrWhiteSpace(query))
            {
                searchQuery = searchQuery.Where(a =>
                    EF.Functions.Like(a.Title, $"%{query}%") ||
                    EF.Functions.Like(a.ProductCode, $"%{query}%"));
            }

            if (categoryId.HasValue)
            {
                searchQuery = searchQuery.Where(a => a.ProductCategoryId == categoryId.Value);
            }

            searchQuery = searchQuery.OrderByDescending(a => a.Id);

            var items = await searchQuery
                .Paginate(paginationInfo)
                .ProjectTo<ProductSelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<ProductSelectDto>>(true, items);
        }

        public async Task<ResponseModel<ProductSelectDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _repository.TableNoTracking
                .ProjectTo<ProductSelectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

            if (product == null)
            {
                return new ResponseModel<ProductSelectDto>(false, null, "محصول یافت نشد.");
            }

            return new ResponseModel<ProductSelectDto>(true, product);
        }

        public async Task<ResponseModel<ProductSelectDto>> CreateAsync(ProductDto dto, CancellationToken cancellationToken)
        {
            try
            {
                dto.Slug = _slugService.CheckSlug(dto.Slug, cancellationToken);

                var entity = dto.ToEntity(_mapper);
                await _repository.AddAsync(entity, cancellationToken);

                var resultDto = await _repository.TableNoTracking
                    .ProjectTo<ProductSelectDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(a => a.Id == entity.Id, cancellationToken);

                return new ResponseModel<ProductSelectDto>(true, resultDto, "محصول با موفقیت ایجاد شد.");
            }
            catch (Exception)
            {
                return new ResponseModel<ProductSelectDto>(false, null, "خطا در ایجاد محصول.");
            }
        }

        public async Task<ResponseModel<ProductSelectDto>> UpdateAsync(int id, ProductDto dto, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(cancellationToken, id);

            if (entity == null)
            {
                return new ResponseModel<ProductSelectDto>(false, null, "محصول یافت نشد.");
            }

            if (!string.Equals(entity.Slug, dto.Slug, StringComparison.OrdinalIgnoreCase))
            {
                var slugExists = await _repository.TableNoTracking
                    .AnyAsync(a => a.Slug == dto.Slug && a.Id != id, cancellationToken);

                dto.Slug = slugExists ? _slugService.CheckSlug(dto.Slug, cancellationToken) : dto.Slug;
            }

            entity = dto.ToEntity(_mapper, entity);
            await _repository.UpdateAsync(entity, cancellationToken);

            var resultDto = await _repository.TableNoTracking
                .ProjectTo<ProductSelectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == entity.Id, cancellationToken);

            return new ResponseModel<ProductSelectDto>(true, resultDto, "محصول با موفقیت به‌روز شد.");
        }

        public async Task<ResponseModel> DeleteAsync(List<int> ids, CancellationToken cancellationToken)
        {
            if (ids == null || ids.Count == 0)
            {
                return new ResponseModel(false, "لیست شناسه‌ها معتبر نیست.");
            }

            foreach (var id in ids)
            {
                var entity = await _repository.GetByIdAsync(cancellationToken, id);

                if (entity == null)
                {
                    return new ResponseModel(false, "محصولی با این شناسه پیدا نشد.");
                }

                await _repository.DeleteAsync(entity, cancellationToken);
            }

            return new ResponseModel(true, "محصولات انتخاب‌شده با موفقیت حذف شدند.");
        }
    }
}
