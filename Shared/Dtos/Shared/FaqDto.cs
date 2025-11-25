
using ResourceLibrary.Resources.ErrorMsg;
using ResourceLibrary;
using SharedModels.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace SharedModels.Dtos.Shared
{
    public class FaqDto : BaseDto<FaqDto , Faq>
    {
        public FaqDto()
        {
            IsPin = false;
        }
        [Display(Name = "Title", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(MainRes))]
        public string Description { get; set; }


        [Display(Name = "CTAText", ResourceType = typeof(MainRes))]
        public string CTAText { get; set; }



        [Display(Name = "CTALink", ResourceType = typeof(MainRes))]
        public string CTALink { get; set; }

        [Display(Name = "IsPin", ResourceType = typeof(MainRes))]
        public bool IsPin { get; set; }

    }

    public class FaqSelectDto : BaseDto<FaqSelectDto, Faq>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string CTAText { get; set; }

        public string CTALink { get; set; }

        public bool IsPin { get; set; }
        public DateTime? CreateDate { get; set; }


        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }
    }
}
