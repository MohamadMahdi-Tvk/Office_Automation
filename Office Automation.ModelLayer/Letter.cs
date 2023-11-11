using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.ModelLayer
{
    [Table("T_Letter")]
    public class Letter : BaseEntity
    {
        [Key]
        [Required]
        public int LetterId { get; set; }


        [MaxLength(40)]
        [Required]
        public string Title { get; set; }



        [MaxLength(1000)]
        [Required]
        public string LetterContent { get; set; }


        [Required]
        [MaxLength(30)]
        public string Number { get; set; }


        [Required]
        public DateTime SendDate { get; set; }



        [Required]
        [MaxLength(30)]
        public string Type { get; set; }


        public bool LetterVisit { get; set; }


        public bool LetterSave { get; set; }

        public virtual Department department { get; set; }
        public int DepartmentId { get; set; }


        public int SendDepartmentId { get; set; }
    }
}
