using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.ModelLayer
{
    [Table("T_Role")]
    public class Role : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]

        public int RoleId { get; set; }



        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }


        
        [MaxLength(30)]
        [Required]
        public string Title { get; set; }



        public virtual IEnumerable<User> users { get; set; }
    }
}
