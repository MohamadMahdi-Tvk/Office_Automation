using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Office_Automation.ModelLayer;

namespace Office_Automation.Views.ViewModels
{
    public class RoleViewModel
    {
        [Display(Name = "آیدی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public int RoleId { get; set; }



        [Display(Name ="عنوان نقش کاربری")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public string Title { get; set; }



        [Display(Name="نام نقش کاربری")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [RegularExpression("^[آ-ی ]+$",ErrorMessage ="فیلد {0} باید فارسی نوشته شود")]
        public string Name { get; set; }



        public virtual IEnumerable<User> users { get; set; }
    }
}