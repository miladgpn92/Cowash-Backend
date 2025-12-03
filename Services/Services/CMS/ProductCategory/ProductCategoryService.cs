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

namespace Services.Services.CMS.ProductCategory
{
    public class ProductCategoryService : IProductCategoryService, IScopedDependency
    {
        private readonly IRepository<Entities.ProductCategory> _repository;
        private readonly IMapper _mapper;
        private readonly ISlugService<Entities.ProductCategory> _slugService;

        public ProductCategoryService(IRepository<Entities.ProductCategory> repository, IMapper mapper, ISlugService<Entities.ProductCategory> slugService)
        {
            _repository = repository;
            _mapper = mapper;
            _slugService = slugService;
        }

        public async Task<ResponseModel<ProductCategorySelectDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _repository.TableNoTracking
                .ProjectTo<ProductCategorySelectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

            if (category == null)
            {
                return new ResponseModel<ProductCategorySelectDto>(false, null, "دسته‌بندی محصول پیدا نشد.");
            }

            return new ResponseModel<ProductCategorySelectDto>(true, category);
        }

        public async Task<ResponseModel<List<ProductCategorySelectDto>>> ListAsync(int page, int pageSize, string query, CancellationToken cancellationToken)
        {
            var validatedPageInfo = PaginationValidation.Validate(page, pageSize);

            var paginationInfo = new Pageres
            {
                PageNumber = validatedPageInfo.Page,
                PageSize = validatedPageInfo.PageSize
            };

            IQueryable<Entities.ProductCategory> searchQuery = _repository.TableNoTracking
                .Where(a => a.CmsLanguage == CmsEx.GetCurrentLanguage());

            if (!string.IsNullOrWhiteSpace(query))
            {
                searchQuery = searchQuery.Where(a => EF.Functions.Like(a.Title, $"%{query}%"));
            }

            searchQuery = searchQuery.OrderByDescending(a => a.Id);

            var items = await searchQuery
                .Paginate(paginationInfo)
                .ProjectTo<ProductCategorySelectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ResponseModel<List<ProductCategorySelectDto>>(true, items);
        }

        public async Task<ResponseModel<ProductCategorySelectDto>> CreateAsync(ProductCategoryDto dto, CancellationToken cancellationToken)
        {
            try
            {
                dto.Slug = _slugService.CheckSlug(dto.Slug, cancellationToken);

                var entity = dto.ToEntity(_mapper);
                await _repository.AddAsync(entity, cancellationToken);

                var resultDto = await _repository.TableNoTracking
                    .ProjectTo<ProductCategorySelectDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(a => a.Id == entity.Id, cancellationToken);

                return new ResponseModel<ProductCategorySelectDto>(true, resultDto, "دسته‌بندی محصول با موفقیت ثبت شد.");
            }
            catch (Exception)
            {
                return new ResponseModel<ProductCategorySelectDto>(false, null, "خطا در ثبت دسته‌بندی محصول.");
            }
        }

        public async Task<ResponseModel<ProductCategorySelectDto>> UpdateAsync(int id, ProductCategoryDto dto, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(cancellationToken, id);

            if (entity == null)
            {
                return new ResponseModel<ProductCategorySelectDto>(false, null, "دسته‌بندی محصول پیدا نشد.");
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
                .ProjectTo<ProductCategorySelectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == entity.Id, cancellationToken);

            return new ResponseModel<ProductCategorySelectDto>(true, resultDto, "دسته‌بندی محصول با موفقیت ویرایش شد.");
        }

        public async Task<ResponseModel> DeleteAsync(List<int> ids, CancellationToken cancellationToken)
        {
            if (ids == null || ids.Count == 0)
            {
                return new ResponseModel(false, "شناسه‌ای برای حذف ارسال نشده است.");
            }

            foreach (var id in ids)
            {
                var entity = await _repository.GetByIdAsync(cancellationToken, id);

                if (entity == null)
                {
                    return new ResponseModel(false, "دسته‌بندی محصول مورد نظر یافت نشد.");
                }

                await _repository.DeleteAsync(entity, cancellationToken);
            }

            return new ResponseModel(true, "حذف دسته‌بندی محصول انجام شد.");
        }

        public async Task<ResponseModel<ProductCategorySelectDto>> GetBySlugAsync(string Slug, CancellationToken cancellationToken)
        {
            var category = await _repository.TableNoTracking
                .ProjectTo<ProductCategorySelectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Slug == Slug, cancellationToken);

            if (category == null)
            {
                return new ResponseModel<ProductCategorySelectDto>(false, null, "دسته‌بندی محصول پیدا نشد.");
            }

            return new ResponseModel<ProductCategorySelectDto>(true, category);
        }
    }
}
