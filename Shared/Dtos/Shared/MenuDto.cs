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
    public class MenuDto : BaseDto<MenuDto,Menu>
    {
        [Display(Name = "Title", ResourceType = typeof(MainRes))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Title { get; set; }

        [Display(Name = "Slug", ResourceType = typeof(MainRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Slug { get; set; }

        [Display(Name = "MenuContent", ResourceType = typeof(MainRes))]
        public string Content { get; set; }
    }


    public class MenuSelectDto : BaseDto<MenuSelectDto, Menu>
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }
    }

    public class MenuItm
    {
        public MenuItm()
        {
            Children = new List<MenuItm>();
        }
        public int id { get; set; }
        public string text { get; set; }
        public string link { get; set; }
        public int parentId { get; set; }

        public List<MenuItm> Children { get; set; }
    }
}
