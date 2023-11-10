using Office_Automation.ModelLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Office_Automation.Views.ViewModels
{
    public class MessageViewModel
    {
        

        [Display(Name = "آیدی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public int MessageId { get; set; }


        

        [Display(Name = "عنوان پیام")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string MessageTitle { get; set; }


        
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string MessageContent { get; set; }

        [Display(Name = "ارسال کننده پیام")]
        public int UserSendMessage { get; set; }


        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}