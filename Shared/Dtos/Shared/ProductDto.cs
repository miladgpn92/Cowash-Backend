using Entities;
using ResourceLibrary;
using ResourceLibrary.Resources.ErrorMsg;
using SharedModels.Api;
using System;
using System.ComponentModel.DataAnnotations;

namespace SharedModels.Dtos.Shared
{
    public class ProductDto : BaseWithSeoDto<ProductDto, Product>
    {
        [Display(Name = "Title", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Title { get; set; }

        [Display(Name = "Slug", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(300, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Slug { get; set; }

        [Display(Name = "ProductCode", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(150, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string ProductCode { get; set; }

        [Display(Name = "Dimensions", ResourceType = typeof(MainRes))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Dimensions { get; set; }

        [Display(Name = "GalleryFilesUrl", ResourceType = typeof(MainRes))]
        public string GalleryFilesUrl { get; set; }

        [Display(Name = "ThumbnailUrl", ResourceType = typeof(MainRes))]
        public string ThumbnailUrl { get; set; }

        [Display(Name = "Description", ResourceType = typeof(MainRes))]
        public string Description { get; set; }

        [Display(Name = "ProductCategoryId", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public int? ProductCategoryId { get; set; }

        [Display(Name = "PublishDate", ResourceType = typeof(MainRes))]
        public DateTime? PublishDate { get; set; }
    }

    public class ProductSelectDto : BaseWithSeoDto<ProductSelectDto, Product>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ProductCode { get; set; }
        public string Dimensions { get; set; }
        public string GalleryFilesUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? ProductCategoryId { get; set; }
        public string ProductCategoryTitle { get; set; }
        public string ProductCategorySlug { get; set; }
        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }
    }
}
