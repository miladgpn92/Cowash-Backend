using Common.Consts;
using Entities;
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
    public class ArticleCategoryDto : BaseWithSeoDto<ArticleCategoryDto, ArticleCategory>
    {
        public ArticleCategoryDto()
        {
            IsPin = false;
        }
        [Display(Name = "Title", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(250, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Title { get; set; }



        [Display(Name = "Slug", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(300, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Slug { get; set; }

        [Display(Name = "IsPin", ResourceType = typeof(MainRes))]
        public bool IsPin { get; set; }

        [Display(Name = "PublishDate", ResourceType = typeof(MainRes))]
        public DateTime? PublishDate { get; set; }


    }

    public class ArticleCategorySelectDto : BaseWithSeoDto<ArticleCategorySelectDto, ArticleCategory>
    {

        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime CreateDate { get; set; }

        public bool IsPin { get; set; }

        public DateTime? PublishDate { get; set; }

        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }
    }
}
