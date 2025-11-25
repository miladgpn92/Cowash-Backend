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
    public class SliderDto:BaseDto<SliderDto,Entities.Slider>
    {
        [Display(Name = "Title", ResourceType = typeof(MainRes))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Title { get; set; }

        [Display(Name = "عنوان بالایی" )]
        public string TopTitle { get; set; }


        [Display(Name = "توضیحات")]
        public string Description { get; set; }



        [Display(Name = "PicUrl", ResourceType = typeof(MainRes))]
        public string PicUrl { get; set; }

        [Display(Name = "Link", ResourceType = typeof(MainRes))]
        public string Link { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(MainRes))]
        public bool IsActive { get; set; } = true;
    }

    public class SliderSelectDto : BaseDto<SliderSelectDto, Slider>
    {
        public string Title { get; set; }


        public string TopTitle { get; set; }

        public string Description { get; set; }

        public string PicUrl { get; set; }
        public string Link { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }
    }
}
