using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.Detroit.Models
{
    //This is a model for expense database
    [Table("Expense")]
    public class Expense
    {
        [Key] [Required]
        public Guid Id { get; set; }
        public string? ExpenseName { get; set; }
        public decimal? ExpenseAmount { get; set; }

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
