using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.ModelLayer
{
    [Table("T_Department")]
    public class Department : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DepartmentId { get; set; }


        [Required]
        [MaxLength(40)]
        public string Name { get; set; }



        [MaxLength(700)]
        public string Info { get; set; }


        [Required]
        public bool IsActive { get; set; }

        public virtual IEnumerable<Letter> Letters { get; set; }


    }
}
