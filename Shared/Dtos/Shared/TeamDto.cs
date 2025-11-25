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
    public class TeamDto : BaseDto<TeamDto , Team>
    {
        public TeamDto()
        {
            IsPin = false;
        }
        [Display(Name = "FullName", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string FullName { get; set; }

        [Display(Name = "JobPosition", ResourceType = typeof(MainRes))]
        public string JobPosition { get; set; }

        [Display(Name = "PicUrl", ResourceType = typeof(MainRes))]
        public string PicUrl { get; set; }

        [Display(Name = "IsPin", ResourceType = typeof(MainRes))]
        public bool IsPin { get; set; }
    }

    public class TeamSelectDto : BaseDto<TeamSelectDto, Team>
    {
        public string FullName { get; set; }
        public string JobPosition { get; set; }
        public string PicUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsPin { get; set; }
        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }
    }
}
