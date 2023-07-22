using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Detroit.Models
{
    [Table("Notification")]
    public class Notification
    {
        [Key] [Required]
        public Guid Id { get; set; }
        public string? NotificationName { get; set; }
        public string? Details { get; set; }

        //Make connections between the account and expense
        public Guid? UserAccountID { get; set; }
        public userAccount? userAccount { get; set; }

        //Make connections between the group and expense
        public Guid? GroupID { get; set; }
        public Group? Group { get; set; }

        //Make connections between payment and expense
        public ICollection<shared> SharedItems { get; set; }
    }
}
