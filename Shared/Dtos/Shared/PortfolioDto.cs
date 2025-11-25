using ResourceLibrary.Resources.ErrorMsg;
using ResourceLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Api;
using Entities;

namespace SharedModels.Dtos.Shared
{
    public class PortfolioDto : BaseWithSeoDto<PortfolioDto, Portfolio>
    {
        public PortfolioDto()
        {
            IsPin = false;
        }

        [Display(Name = "ThumbPicUrl", ResourceType = typeof(MainRes))]
        public string ThumbPicUrl { get; set; }

        [Display(Name = "Title", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Title { get; set; }

        [Display(Name = "DateOfWork", ResourceType = typeof(MainRes))]
        public string DateOfWork { get; set; }


        [Display(Name = "Description", ResourceType = typeof(MainRes))]
        public string Description { get; set; }

        [Display(Name = "ChallengePicUrl", ResourceType = typeof(MainRes))]
        public string ChallengePicUrl { get; set; }

        [Display(Name = "ChallengeDescription", ResourceType = typeof(MainRes))]
        public string ChallengeDescription { get; set; }

        [Display(Name = "SolutionPicUrl", ResourceType = typeof(MainRes))]
        public string SolutionPicUrl { get; set; }

        [Display(Name = "SolutionDescription", ResourceType = typeof(MainRes))]
        public string SolutionDescription { get; set; }

        [Display(Name = "GalleryFilesUrl", ResourceType = typeof(MainRes))]
        public string GalleryFilesUrl { get; set; }

        [Display(Name = "Slug", ResourceType = typeof(MainRes))]
        public string Slug { get; set; }

        [Display(Name = "IsPin", ResourceType = typeof(MainRes))]
        public bool IsPin { get; set; }

        [Display(Name = "PortfolioCategoryTitle", ResourceType = typeof(MainRes))]
        public int? PortfolioCategoryId { get; set; }

        [Display(Name = "PortfolioCategoryTitle", ResourceType = typeof(MainRes))]
        public string PortfolioCategoryTitle { get; set; }

        [Display(Name = "PublishDate", ResourceType = typeof(MainRes))]
        public DateTime? PublishDate { get; set; }

    }

    public class PortfolioSelectDto : BaseWithSeoDto<PortfolioSelectDto, Portfolio>
    {
        public string ThumbPicUrl { get; set; }

        public string Title { get; set; }

        public string DateOfWork { get; set; }

        public string Description { get; set; }

        public string ChallengePicUrl { get; set; }

        public string ChallengeDescription { get; set; }

        public string SolutionPicUrl { get; set; }

        public string SolutionDescription { get; set; }

        public string GalleryFilesUrl { get; set; }

        public string Slug { get; set; }

        public bool IsPin { get; set; }

        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }


        public DateTime? CreateDate { get; set; }

        public int? PortfolioCategoryId { get; set; }
        public string PortfolioCategoryTitle { get; set; }

        public DateTime? PublishDate { get; set; }

    }
}
