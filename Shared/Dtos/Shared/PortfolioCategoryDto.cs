using Entities;
using ResourceLibrary.Resources.ErrorMsg;
using ResourceLibrary;
using SharedModels.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Dtos.Shared
{
    public class PortfolioCategoryDto : BaseWithSeoDto<PortfolioCategoryDto, PortfolioCategory>
    {
        public PortfolioCategoryDto()
        {
            IsPin = false;
        }
        [Display(Name = "Title", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Title { get; set; }

        [Display(Name = "Slug", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Slug { get; set; }

        [Display(Name = "PublishDate", ResourceType = typeof(MainRes))]
        public DateTime? PublishDate { get; set; }

        [Display(Name = "PublishDate", ResourceType = typeof(MainRes))]
        public bool IsPin { get; set; }
    }

    public class PortfolioCategorySelectDto : BaseWithSeoDto<PortfolioCategorySelectDto, PortfolioCategory>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPin { get; set; }

        public DateTime? CreateDate { get; set; }

        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }
    }
}
