using Office_Automation.ModelLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Office_Automation.Views.ViewModels
{
    public class LetterViewModel
    {
        [Display(Name = "آیدی")]
        [Required(ErrorMessage ="فیلد {0} اجباری است")]
        public int LetterId { get; set; }



        [Display(Name = "عنوان نامه")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Title { get; set; }



        [Display(Name = "متن نامه")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string LetterContent { get; set; }



        [Display(Name = "شماره نامه")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Number { get; set; }



        [Display(Name = "تاریخ ارسال نامه")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public DateTime SendDate { get; set; }



        [Display(Name ="نوع نامه")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Type { get; set; }


        [Display(Name = "بازدید نامه")]
        public bool LetterVisit { get; set; }



        public virtual Department department { get; set; }
        public int DepartmentId { get; set; }

        [Display(Name ="دپارتمان ارسال کننده")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public int SendDepartmentId { get; set; }
    }
}