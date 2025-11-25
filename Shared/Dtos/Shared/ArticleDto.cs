using Common.Consts;
using Entities;
using Microsoft.AspNetCore.Http;
using ResourceLibrary;
using ResourceLibrary.Resources.ErrorMsg;
using SharedModels.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Dtos
{
    public class ArticleDto : BaseWithSeoDto<ArticleDto, Article>
    {
        public ArticleDto()
        {
            IsPin = false;
        }
        [Display(Name = "Title", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Title { get; set; }

       [Display(Name = "Description", ResourceType = typeof(MainRes))]
        public string Description { get; set; }

        [Display(Name = "DescriptionForEditor", ResourceType = typeof(MainRes))]
        public string DescriptionForEditor { get; set; }

        [Display(Name = "PicUrl", ResourceType = typeof(MainRes))]
        public string PicUrl { get; set; }

        [Display(Name = "Slug", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Slug { get; set; }

        [Display(Name = "PublishDate", ResourceType = typeof(MainRes))]
        public DateTime PublishDate { get; set; }

        [Display(Name = "ArticleCategoryId", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public int? ArticleCategoryId { get; set; }


        [Display(Name = "ArticleCategoryId", ResourceType = typeof(MainRes))]
        public string ArticleCategoryTitle { get; set; }

        [Display(Name = "IsPin", ResourceType = typeof(MainRes))]
        public bool IsPin { get; set; }


    }

    public class ArticleSelectDto : BaseWithSeoDto<ArticleSelectDto, Article>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string PicUrl { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string ArticleCategoryTitle { get; set; }
        public string ArticleCategorySlug { get; set; }
        public int ArticleCategoryId { get; set; }
        public string Description { get; set; }
        public string DescriptionForEditor { get; set; }

        public bool IsPin { get; set; }

        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }
    }

}
