using Office_Automation.ModelLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Office_Automation.Views.ViewModels
{
    public class UserViewModel
    {

        [Display(Name = "آیدی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public int UserId { get; set; }



        [Display(Name = "نام")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Name { get; set; }


        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Family { get; set; }



        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public DateTime BirthDate { get; set; }



        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public bool Gender { get; set; }


        [Display(Name = "تاریخ عضویت")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public DateTime RegisterDate { get; set; }



        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [RegularExpression("(09)[0-9]{9}", ErrorMessage = "فرمت شماره موبایل صحیح نیست")]
        public string PhoneNumber { get; set; }



        [Display(Name = "شماره پرسنلی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string PersonnelID { get; set; }



        [Display(Name = "تاریخ استخدام")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [DisplayFormat(DataFormatString = "{0: dddd, dd MMMM yyyy}")]
        public DateTime StartingDate { get; set; }


        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Display(Name = "تصویر پروفایل")]
        public string ProfileImage { get; set; }


        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public bool IsActive { get; set; }


        public Role Role { get; set; }

        [Display(Name = "نقش کاربری")]
        public int RoleId { get; set; }


        public virtual Department Department { get; set; }



        [Display(Name = "دپارتمان")]
        public int DepartmentId { get; set; }

        public virtual IEnumerable<Letter> letter { get; set; }
    }
}