using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.ModelLayer
{
    [Table("T_User")]
    public class User : BaseEntity
    {
        [Key]
        [Required]
        public int UserId { get; set; }



        [Required]
        [MaxLength(40)]
        public string Name { get; set; }


        [Required]
        [MaxLength(40)]
        public string Family { get; set; }



        [Required]
        public DateTime BirthDate { get; set; }


        [Required]
        public bool Gender { get; set; }


        [Required]
        public DateTime RegisterDate { get; set; }


        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }


        [Required]
        [MaxLength(10)]
        public string PersonnelID { get; set; }


        [Required]
        public DateTime StartingDate { get; set; }


        [Required]
        [MaxLength(20)]
        public string Password { get; set; }



        [Required]
        [MaxLength(50)]
        public string ProfileImage { get; set; }


        [Required]
        public bool IsActive { get; set; }


        public Role Role { get; set; }
        public int RoleId { get; set; }


        public virtual Department Department { get; set; }
  
        public int DepartmentId { get; set; }

        public virtual IEnumerable<Letter> letter { get; set; }


        public virtual IEnumerable<Message> Messages { get; set; }


    }
}
