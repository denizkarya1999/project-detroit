using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Detroit.Models
{
    //This is a model for Payment database
    [Table("Payment")]
    public class Payment
    {
        [Key] [Required]
        public Guid Id { get; set; }
        public string? PaymentName { get; set; }
        public decimal? PaymentAmount { get; set; }

        //Make connections between the account and payment
        public Guid? UserAccountID { get; set; }
        public userAccount? userAccount { get; set; }

        //Make connections between the group and payment
        public Guid? GroupID { get; set; }
        public Group? Group { get; set; }

        //Make connections between payment and shared
        public ICollection<shared> SharedItems { get; set; }
    }
}
