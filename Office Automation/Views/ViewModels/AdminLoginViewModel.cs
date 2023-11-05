using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Office_Automation.Views.ViewModels
{
    public class AdminLoginViewModel
    {

        [Display(Name = "شماره پرسنلی")]
        public string PersonnelID { get; set; }



        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Display(Name = "آدرس بازگشتی")]
        public string ReturnUrl { get; set; }

    }
}