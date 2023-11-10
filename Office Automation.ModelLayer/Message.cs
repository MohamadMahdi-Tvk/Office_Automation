using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.ModelLayer
{
    [Table("T_Message")]
    public class Message : BaseEntity
    {
        [Key]
        [Required]
        public int MessageId { get; set; }


        [Required]
        [MaxLength(40)]
        public string MessageTitle { get; set; }


        [Required]
        [MaxLength(700)]
        public string MessageContent { get; set; }


        
        public int UserSendMessage { get; set; }


        public virtual User User { get; set; }
        public int UserId { get; set; } 
    }
}
