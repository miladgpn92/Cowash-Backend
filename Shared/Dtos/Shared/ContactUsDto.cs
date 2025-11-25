using Common.Consts;
using Common.Enums;
using Entities;
using Entities.ContactUs;
using ResourceLibrary.Resources.ContactUs;
using ResourceLibrary.Resources.ErrorMsg;
using SharedModels.Api;
using SharedModels.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Dtos
{
    public class ContactUsDto: BaseDto<ContactUsDto, ContactUs>
    {
        [Display(Name = "FullName", ResourceType = typeof(ContactUsRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        [MaxLength(70, ErrorMessageResourceName = "MaxLenMsg", ErrorMessageResourceType = typeof(ErrorMsg))]

        public string FullName { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(ContactUsRes))]
        [ValidateIranPhonenumber(ErrorMessageResourceName = "MobileNumberErr", ErrorMessageResourceType = typeof(ErrorMsg))]
        [Required(ErrorMessage = ErrMsg.RequierdMsg)]
        public string PhoneNumber { get; set; }

        [Display(Name = "MessageText", ResourceType = typeof(ContactUsRes))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "RequierdMsg", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string MessageText { get; set; }

        [Display(Name = "ContactUsState", ResourceType = typeof(ContactUsRes))]
        public ContactUsState ContactUsState { get; set; }
    }

    public class ContactUsSelectDto : BaseDto<ContactUsSelectDto, ContactUs>
    {
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string MessageText { get; set; }

        public ContactUsState ContactUsState { get; set; }

        public new CmsLanguage CmsLanguage { get; set; }

        public DateTime CreateDate { get; set; }

        public int? CreatorUserId { get; set; }

        public string ApplicationUserName { get; set; }
        public string ApplicationUserFamily { get; set; }
    }
}
