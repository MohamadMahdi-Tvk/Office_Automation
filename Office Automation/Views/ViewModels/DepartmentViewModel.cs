using Office_Automation.ModelLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Office_Automation.Views.ViewModels
{
    public class DepartmentViewModel
    {
        [Display(Name = "آیدی")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public int DepartmentId { get; set; }


        [Display(Name = "نام دپارتمان")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        [StringLength(20, ErrorMessage = "حداکثر کارکتر مورد قبول 20 می باشد", MinimumLength = 2)]
        public string Name { get; set; }



        [Display(Name = "اطلاعات تکمیلی دپارتمان")]
        [StringLength(300, ErrorMessage = "حداکثر کارکتر مورد قبول 300 می باشد")]
        public string Info { get; set; }



        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "فیلد {0} اجباری است")]
        public bool IsActive { get; set; }



        public virtual IEnumerable<User> users { get; set; }
    }
}